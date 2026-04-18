using AdVision.Application;
using AdVision.Application.Contracts.GetContractsQuery;
using AdVision.Application.Customers.GetAllCustomersQuery;
using AdVision.Application.Discounts.GetAllDiscountsQuery;
using AdVision.Application.Employees.GetAllEmployeesQuery;
using AdVision.Application.Positions.GetAllPositionsQuery;
using AdVision.Application.Venues.GetDistinctQuery;
using AdVision.Application.Venues.GetVenuesQuery;
using AdVision.Application.VenueTypes.GetAllVenueTypesQuery;
using AdVision.Contracts;
using AdVision.Presentation.Helpers;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation;

public partial class MainForm : Form
{
    // Ошибки
    private const string LoadVenuesErrorTitle = "Ошибка загрузки площадок";
    private const string LoadVenueTypesErrorTitle = "Ошибка загрузки типов площадок";
    private const string LoadPositionsErrorTitle = "Ошибка загрузки позиций";
    private const string LoadDiscountsErrorTitle = "Ошибка загрузки скидок";
    private const string LoadRegionsErrorTitle = "Ошибка загрузки регионов";
    private const string LoadDistrictsErrorTitle = "Ошибка загрузки районов";
    private const string LoadCitiesErrorTitle = "Ошибка загрузки городов";
    private const string UnknownErrorTitle = "Непредвиденная ошибка";
    private const string DefaultLoadErrorMessage = "Не удалось загрузить данные";
    private const string LoadEmployeesErrorTitle = "Ошибка загрузки сотрудников";
    private const string LoadCustomersErrorTitle = "Ошибка загрузки заказчиков";
    private const string LoadContractsErrorTitle = "Ошибка загрузки договоров";

    // Количество записей на странице
    private const int PageSize = 10;
    private const int DirectoryPageSize = 10;
    private const int ContractsPageSize = 10;

    private readonly CancellationTokenSource _cts = new();
    private readonly INotificationService _notificationService;
    private readonly IQueryHandler<IReadOnlyList<string>, GetDistinctQuery> _getDistinctQueryHandler;
    private readonly IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> _venueTypesQueryHandler;
    private readonly IQueryHandler<IReadOnlyList<PositionDto>, GetAllPositionsQuery> _positionsQueryHandler;
    private readonly IQueryHandler<IReadOnlyList<DiscountDto>, GetAllDiscountsQuery> _discountsQueryHandler;
    private readonly IQueryHandler<IReadOnlyList<EmployeeDto>, GetAllEmployeesQuery> _employeesQueryHandler;
    private readonly IQueryHandler<PagedResult<VenueDto>, GetVenuesQuery> _venuesQueryHandler;
    private readonly IQueryHandler<IReadOnlyList<CustomerDto>, GetAllCustomersQuery> _customersQueryHandler;
    private readonly IQueryHandler<PagedResult<ContractDto>, GetContractsQuery> _contractsQueryHandler;
    private readonly IContractRepository _contractRepository;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MainForm> _logger;

    private readonly DirectoryListHelper _venueTypesDirectory;
    private readonly DirectoryListHelper _positionsDirectory;
    private readonly DirectoryListHelper _discountsDirectory;
    private readonly DirectoryListHelper _employeesDirectory;
    private readonly DirectoryListHelper _customersDirectory;

    private bool _isLoading;

    // Площадки
    private int _page = 1;
    private int _totalCount;

    // Справочники
    private bool _directoriesTabInitialized;
    private DirectoryType _currentDirectoryType = DirectoryType.None;
    private VenueTypesFilterUserControl? _venueTypesFilterControl;
    private PositionsFilterUserControl? _positionsFilterControl;
    private DiscountsFilterUserControl? _discountsFilterControl;
    private EmployeesFilterUserControl? _employeesFilterControl;
    private CustomersFilterUserControl? _customersFilterControl;

    // Контракты
    private int _contractsPage = 1;
    private int _contractsTotalCount;
    private bool _contractsTabInitialized;

    private int TotalPages => _totalCount == 0
        ? 0
        : (int)Math.Ceiling((double)_totalCount / PageSize);

    private int ContractsTotalPages => _contractsTotalCount == 0
        ? 0
        : (int)Math.Ceiling((double)_contractsTotalCount / ContractsPageSize);

    public MainForm(
        INotificationService notificationService,
        IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> venueTypesQueryHandler,
        IQueryHandler<IReadOnlyList<PositionDto>, GetAllPositionsQuery> positionsQueryHandler,
        IQueryHandler<IReadOnlyList<string>, GetDistinctQuery> getDistinctQueryHandler,
        IQueryHandler<PagedResult<VenueDto>, GetVenuesQuery> venuesQueryHandler,
        IQueryHandler<IReadOnlyList<DiscountDto>, GetAllDiscountsQuery> discountsQueryHandler,
        IQueryHandler<IReadOnlyList<EmployeeDto>, GetAllEmployeesQuery> getAllEmployeesQueryHandler,
        IQueryHandler<IReadOnlyList<CustomerDto>, GetAllCustomersQuery> customersQueryHandler,
        IQueryHandler<PagedResult<ContractDto>, GetContractsQuery> contractsQueryHandler,
        IContractRepository contractRepository,
        IServiceProvider serviceProvider,
        ILogger<MainForm> logger)
    {
        _notificationService = notificationService;
        _venueTypesQueryHandler = venueTypesQueryHandler;
        _positionsQueryHandler = positionsQueryHandler;
        _getDistinctQueryHandler = getDistinctQueryHandler;
        _venuesQueryHandler = venuesQueryHandler;
        _discountsQueryHandler = discountsQueryHandler;
        _employeesQueryHandler = getAllEmployeesQueryHandler;
        _customersQueryHandler = customersQueryHandler;
        _contractsQueryHandler = contractsQueryHandler;
        _contractRepository = contractRepository;
        _serviceProvider = serviceProvider;
        _logger = logger;

        _venueTypesDirectory = new DirectoryListHelper(DirectoryPageSize);
        _positionsDirectory = new DirectoryListHelper(DirectoryPageSize);
        _discountsDirectory = new DirectoryListHelper(DirectoryPageSize);
        _employeesDirectory = new DirectoryListHelper(DirectoryPageSize);
        _customersDirectory = new DirectoryListHelper(DirectoryPageSize);

        InitializeComponent();

        SubscribePagingControls();
        ConfigureVenuesGrid();
        UpdateVenuesPagingState();
        UpdateDirectoryPagingState();

        venuesDataGridView.CellDoubleClick += VenuesDataGridView_CellDoubleClick;
    }

    private void SubscribePagingControls()
    {
        venuesPagingUserControl.PrevClicked += GoToPreviousVenuePage;
        venuesPagingUserControl.NextClicked += GoToNextVenuePage;
        venuesPagingUserControl.AddClicked += CreateVenue;

        directoriesPagingUserControl.PrevClicked += GoToPreviousDirectoryPageUi;
        directoriesPagingUserControl.NextClicked += GoToNextDirectoryPageUi;
        directoriesPagingUserControl.AddClicked += AddDirectoryItem;

        contractsPagingUserControl.PrevClicked += GoToPreviousContractsPage;
        contractsPagingUserControl.NextClicked += GoToNextContractsPage;
        contractsPagingUserControl.AddClicked += AddContract;
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        await RunUiActionAsync(
            InitializeAsync,
            "Инициализация главной формы отменена",
            "Ошибка инициализации главной формы");
    }

    private async Task InitializeAsync()
    {
        await RefreshDataAsync();
    }

    private async Task RefreshDataAsync()
    {
        if (_isLoading)
        {
            return;
        }

        _isLoading = true;
        UseWaitCursor = true;
        UpdateVenuesPagingState();
        UpdateDirectoryPagingState();

        try
        {
            await LoadVenueFiltersAsync();
            await LoadVenuesAsync();
        }
        finally
        {
            _isLoading = false;
            UseWaitCursor = false;
            UpdateVenuesPagingState();
            UpdateDirectoryPagingState();
        }
    }

    private async Task RunUiActionAsync(
        Func<Task> action,
        string cancelLogMessage,
        string errorLogMessage)
    {
        try
        {
            await action();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation(cancelLogMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, errorLogMessage);
            _notificationService.ShowError(UnknownErrorTitle, ex.Message);
        }
    }

    private void ShowLoadError(string title, IEnumerable<object>? errors)
    {
        var message = string.Join(Environment.NewLine, errors ?? []);
        var normalizedMessage = string.IsNullOrWhiteSpace(message)
            ? DefaultLoadErrorMessage
            : message;

        _logger.LogError("{Title}: {Error}", title, normalizedMessage);
        _notificationService.ShowError(title, normalizedMessage);
    }

    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        if (!_cts.IsCancellationRequested)
        {
            _cts.Cancel();
        }

        _cts.Dispose();
    }

    private async void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tabControl1.SelectedTab == tabPage2 && !_directoriesTabInitialized)
        {
            _directoriesTabInitialized = true;

            await RunUiActionAsync(
                () => OpenDirectoryAsync(DirectoryType.VenueTypes),
                "Загрузка типов площадок отменена",
                "Ошибка загрузки типов площадок");
        }

        if (tabControl1.SelectedTab == tabPage3 && !_contractsTabInitialized)
        {
            _contractsTabInitialized = true;

            await RunUiActionAsync(
                async () =>
                {
                    await ResetContractsFiltersAsync();
                    await InitializeContractsDateFiltersFromDbAsync();
                    await LoadContractsFiltersAsync();
                    LoadContractStatuses();
                    LoadContractSorting();
                    ConfigureContractsGrid();
                    await LoadContractsAsync();
                    UpdateContractsResetButtonState();
                },
                "Загрузка договоров отменена",
                "Ошибка загрузки договоров");
        }
    }
}