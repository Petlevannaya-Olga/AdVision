using System.Linq.Expressions;
using AdVision.Application;
using AdVision.Application.Discounts.GetAllDiscountsQuery;
using AdVision.Application.Positions.GetAllPositionsQuery;
using AdVision.Application.Venues.GetDistinctQuery;
using AdVision.Application.Venues.GetVenuesQuery;
using AdVision.Application.VenueTypes.GetAllVenueTypesQuery;
using AdVision.Contracts;
using AdVision.Domain.Venues;
using AdVision.Domain.VenueTypes;
using AdVision.Presentation.Helpers;
using AdVision.Presentation.Notifications;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Abstractions;
using Shared.Extensions;

namespace AdVision.Presentation;

public partial class MainForm : Form
{
    private const string LoadVenuesErrorTitle = "Ошибка загрузки площадок";
    private const string LoadVenueTypesErrorTitle = "Ошибка загрузки типов площадок";
    private const string LoadPositionsErrorTitle = "Ошибка загрузки позиций";
    private const string LoadRegionsErrorTitle = "Ошибка загрузки регионов";
    private const string LoadDistrictsErrorTitle = "Ошибка загрузки районов";
    private const string LoadCitiesErrorTitle = "Ошибка загрузки городов";
    private const string UnknownErrorTitle = "Непредвиденная ошибка";
    private const string DefaultLoadErrorMessage = "Не удалось загрузить данные";
    private const string LoadDiscountsErrorTitle = "Ошибка загрузки скидок";

    private const int PageSize = 10;
    private const int DirectoryPageSize = 10;

    private readonly CancellationTokenSource _cts = new();
    private readonly INotificationService _notificationService;
    private readonly IQueryHandler<IReadOnlyList<string>, GetDistinctQuery> _getDistinctQueryHandler;
    private readonly IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> _venueTypesQueryHandler;
    private readonly IQueryHandler<IReadOnlyList<PositionDto>, GetAllPositionsQuery> _positionsQueryHandler;
    private readonly IQueryHandler<IReadOnlyList<DiscountDto>, GetAllDiscountsQuery> _discountsQueryHandler;
    private readonly IQueryHandler<PagedResult<VenueDto>, GetVenuesQuery> _venuesQueryHandler;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MainForm> _logger;

    private readonly DirectoryListHelper _venueTypesDirectory;
    private readonly DirectoryListHelper _positionsDirectory;
    private readonly DirectoryListHelper _discountsDirectory;

    private bool _isLoading;

    // Площадки
    private int _page = 1;
    private int _totalCount;

    private bool _directoriesTabInitialized;
    private DirectoryType _currentDirectoryType = DirectoryType.None;
    private VenueTypesFilterUserControl? _venueTypesFilterControl;
    private PositionsFilterUserControl? _positionsFilterControl;
    private DiscountsFilterUserControl? _discountsFilterControl;

    private int TotalPages => _totalCount == 0
        ? 0
        : (int)Math.Ceiling((double)_totalCount / PageSize);

    public MainForm(
        INotificationService notificationService,
        IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> venueTypesQueryHandler,
        IQueryHandler<IReadOnlyList<PositionDto>, GetAllPositionsQuery> positionsQueryHandler,
        IQueryHandler<IReadOnlyList<string>, GetDistinctQuery> getDistinctQueryHandler,
        IQueryHandler<PagedResult<VenueDto>, GetVenuesQuery> venuesQueryHandler,
        IQueryHandler<IReadOnlyList<DiscountDto>, GetAllDiscountsQuery> discountsQueryHandler,
        IServiceProvider serviceProvider,
        ILogger<MainForm> logger)
    {
        _notificationService = notificationService;
        _venueTypesQueryHandler = venueTypesQueryHandler;
        _positionsQueryHandler = positionsQueryHandler;
        _getDistinctQueryHandler = getDistinctQueryHandler;
        _venuesQueryHandler = venuesQueryHandler;
        _discountsQueryHandler = discountsQueryHandler;
        _serviceProvider = serviceProvider;
        _logger = logger;

        _venueTypesDirectory = new DirectoryListHelper(DirectoryPageSize);
        _positionsDirectory = new DirectoryListHelper(DirectoryPageSize);
        _discountsDirectory = new DirectoryListHelper(DirectoryPageSize);

        InitializeComponent();

        ConfigureVenuesGrid();
        UpdatePagingState();
        UpdateDirectoryPagingState();

        venuesDataGridView.CellDoubleClick += VenuesDataGridView_CellDoubleClick;
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
        UpdatePagingState();
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
            UpdatePagingState();
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

    #region Площадки

    private void ConfigureVenuesGrid()
    {
        venuesDataGridView.AutoGenerateColumns = false;
        venuesDataGridView.Columns.Clear();

        AddVenueTextColumn(nameof(VenueDto.Name), "Название", "colName");
        AddVenueTextColumn(nameof(VenueDto.Type), "Тип", "colType");
        AddVenueTextColumn(nameof(VenueDto.Region), "Регион", "colRegion");
        AddVenueTextColumn(nameof(VenueDto.District), "Район", "colDistrict");
        AddVenueTextColumn(nameof(VenueDto.City), "Город", "colCity");
        AddVenueTextColumn(nameof(VenueDto.Street), "Улица", "colStreet");
        AddVenueTextColumn(nameof(VenueDto.House), "Дом", "colHouse");
        AddVenueTextColumn(nameof(VenueDto.Latitude), "Широта", "colLatitude");
        AddVenueTextColumn(nameof(VenueDto.Longitude), "Долгота", "colLongitude");
        AddVenueTextColumn(nameof(VenueDto.Width), "Ширина", "colWidth");
        AddVenueTextColumn(nameof(VenueDto.Height), "Высота", "colHeight");
        AddVenueTextColumn(nameof(VenueDto.Rating), "Рейтинг", "colRating");
    }

    private void AddVenueTextColumn(string dataPropertyName, string headerText, string columnName)
    {
        venuesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = dataPropertyName,
            HeaderText = headerText,
            Name = columnName
        });
    }

    private async Task LoadVenueFiltersAsync()
    {
        await LoadVenueTypesToComboboxAsync();
        await LoadRegionsAsync();
        await LoadDistrictsAsync();
        await LoadCitiesAsync();

        ResetVenueFilters();
        UpdateVenueResetButtonState();
    }

    private async Task LoadVenueTypesToComboboxAsync()
    {
        cbVenueTypes.DataSource = null;

        var result = await _venueTypesQueryHandler.Handle(
            new GetAllVenueTypesQuery(),
            _cts.Token);

        if (result.IsFailure)
        {
            ShowLoadError(LoadVenueTypesErrorTitle, result.Error);
            return;
        }

        var venueTypes = result.Value
            .OrderBy(x => x.Name)
            .ToList();

        cbVenueTypes.DataSource = venueTypes;
        cbVenueTypes.DisplayMember = nameof(VenueTypeDto.Name);
        cbVenueTypes.ValueMember = nameof(VenueTypeDto.Id);
        cbVenueTypes.SelectedIndex = -1;
    }

    private Task LoadRegionsAsync()
    {
        return LoadDistinctAsync(
            cbRegions,
            new GetDistinctQuery(v => v.Address.Region),
            LoadRegionsErrorTitle);
    }

    private Task LoadDistrictsAsync()
    {
        return LoadDistinctAsync(
            cbDistricts,
            new GetDistinctQuery(v => v.Address.District),
            LoadDistrictsErrorTitle);
    }

    private Task LoadCitiesAsync()
    {
        return LoadDistinctAsync(
            cbCities,
            new GetDistinctQuery(v => v.Address.City),
            LoadCitiesErrorTitle);
    }

    private async Task LoadDistinctAsync(
        ComboBox comboBox,
        GetDistinctQuery query,
        string errorTitle)
    {
        comboBox.DataSource = null;

        var result = await _getDistinctQueryHandler.Handle(query, _cts.Token);

        if (result.IsFailure)
        {
            ShowLoadError(errorTitle, result.Error);
            return;
        }

        comboBox.DataSource = result.Value;
        comboBox.SelectedIndex = -1;
    }

    private async Task LoadVenuesAsync()
    {
        var filter = BuildVenueFilter();
        var orderBy = BuildVenueSort();
        var descending = chkDescending.Checked;

        var result = await _venuesQueryHandler.Handle(
            new GetVenuesQuery(_page, PageSize, filter, orderBy, descending),
            _cts.Token);

        if (result.IsFailure)
        {
            ShowLoadError(LoadVenuesErrorTitle, result.Error);
            return;
        }

        var paged = result.Value;
        _totalCount = paged.TotalCount;

        venuesDataGridView.DataSource = new BindingSource
        {
            DataSource = paged.Items
        };

        UpdatePagingState();
    }

    private Expression<Func<Venue, bool>> BuildVenueFilter()
    {
        Expression<Func<Venue, bool>> filter = v => true;

        var selectedRegion = cbRegions.SelectedItem as string;
        var selectedDistrict = cbDistricts.SelectedItem as string;
        var selectedCity = cbCities.SelectedItem as string;
        var selectedName = txtName.Text.Trim();
        var selectedStreet = txtStreet.Text.Trim();

        if (cbVenueTypes.SelectedItem is VenueTypeDto selectedVenueType)
        {
            var venueTypeId = new VenueTypeId(selectedVenueType.Id);
            filter = filter.And(v => v.VenueTypeId == venueTypeId);
        }

        if (!string.IsNullOrWhiteSpace(selectedRegion))
        {
            filter = filter.And(v => v.Address.Region == selectedRegion);
        }

        if (!string.IsNullOrWhiteSpace(selectedDistrict))
        {
            filter = filter.And(v => v.Address.District == selectedDistrict);
        }

        if (!string.IsNullOrWhiteSpace(selectedCity))
        {
            filter = filter.And(v => v.Address.City == selectedCity);
        }

        if (!string.IsNullOrWhiteSpace(selectedName))
        {
            var pattern = $"%{selectedName}%";
            filter = filter.And(v => EF.Functions.Like(v.Name.Value, pattern));
        }

        if (!string.IsNullOrWhiteSpace(selectedStreet))
        {
            var pattern = $"%{selectedStreet}%";
            filter = filter.And(v => EF.Functions.Like(v.Address.Street, pattern));
        }

        var ratingFrom = nudRatingFrom.Value;
        var ratingTo = nudRatingTo.Value;

        if (ratingFrom > nudRatingFrom.Minimum)
        {
            filter = filter.And(v => v.Rating.Value >= (double)ratingFrom);
        }

        if (ratingTo < nudRatingTo.Maximum)
        {
            filter = filter.And(v => v.Rating.Value <= (double)ratingTo);
        }

        return filter;
    }

    private Expression<Func<Venue, object>> BuildVenueSort()
    {
        return cbSortOrder.SelectedItem?.ToString() switch
        {
            "Название" => v => v.Name.Value,
            "Тип" => v => v.Type,
            "Регион" => v => v.Address.Region,
            "Район" => v => v.Address.District,
            "Город" => v => v.Address.City,
            "Улица" => v => v.Address.Street,
            "Широта" => v => v.Address.Latitude,
            "Долгота" => v => v.Address.Longitude,
            "Ширина" => v => v.Size.Width,
            "Высота" => v => v.Size.Height,
            _ => v => v.Name.Value
        };
    }

    private void UpdatePagingState()
    {
        btnPrev.Enabled = !_isLoading && _page > 1;
        btnNext.Enabled = !_isLoading && _page < TotalPages;

        label11.Text = _totalCount == 0
            ? "Количество записей: 0"
            : $"Количество записей: {_totalCount}, стр. {_page} из {TotalPages}";
    }

    private void ResetVenueFilters()
    {
        cbVenueTypes.SelectedIndex = -1;
        cbRegions.SelectedIndex = -1;
        cbDistricts.SelectedIndex = -1;
        cbCities.SelectedIndex = -1;

        txtName.Clear();
        txtStreet.Clear();

        nudRatingFrom.Value = nudRatingFrom.Minimum;
        nudRatingTo.Value = nudRatingTo.Maximum;

        cbSortOrder.SelectedIndex = 0;
        chkDescending.Checked = false;
    }

    private void UpdateVenueResetButtonState()
    {
        btnReset.Enabled =
            cbVenueTypes.SelectedIndex >= 0 ||
            cbRegions.SelectedIndex >= 0 ||
            cbDistricts.SelectedIndex >= 0 ||
            cbCities.SelectedIndex >= 0 ||
            !string.IsNullOrWhiteSpace(txtName.Text) ||
            !string.IsNullOrWhiteSpace(txtStreet.Text) ||
            chkDescending.Checked ||
            nudRatingFrom.Value > nudRatingFrom.Minimum ||
            nudRatingTo.Value < nudRatingTo.Maximum ||
            cbSortOrder.SelectedIndex > 0;
    }

    private async void BtnApply_Click(object sender, EventArgs e)
    {
        if (_isLoading)
        {
            return;
        }

        await RunUiActionAsync(
            async () =>
            {
                _page = 1;
                await LoadVenuesAsync();
                UpdateVenueResetButtonState();
            },
            "Применение фильтров отменено",
            "Ошибка применения фильтров");
    }

    private async void BtnReset_Click(object sender, EventArgs e)
    {
        if (_isLoading || tabControl1.SelectedTab == tabPage2)
        {
            return;
        }

        await RunUiActionAsync(
            async () =>
            {
                ResetVenueFilters();
                _page = 1;
                await LoadVenuesAsync();
                UpdateVenueResetButtonState();
            },
            "Сброс фильтров отменен",
            "Ошибка сброса фильтров");
    }

    private async void BtnPrev_Click(object sender, EventArgs e)
    {
        if (_isLoading || _page <= 1)
        {
            return;
        }

        await RunUiActionAsync(
            async () =>
            {
                _page--;
                await LoadVenuesAsync();
            },
            "Переход на предыдущую страницу отменен",
            "Ошибка загрузки предыдущей страницы");
    }

    private async void BtnNext_Click(object sender, EventArgs e)
    {
        if (_isLoading || _page >= TotalPages)
        {
            return;
        }

        await RunUiActionAsync(
            async () =>
            {
                _page++;
                await LoadVenuesAsync();
            },
            "Переход на следующую страницу отменен",
            "Ошибка загрузки следующей страницы");
    }

    private void BtnCreate_Click(object sender, EventArgs e)
    {
        var form = _serviceProvider.GetRequiredService<VenueForm>();
        form.VenueCreated += OnVenueCreated;
        form.ShowDialog();
        form.VenueCreated -= OnVenueCreated;
    }

    private async void OnVenueCreated()
    {
        await RunUiActionAsync(
            async () =>
            {
                _page = 1;
                await RefreshDataAsync();
            },
            "Обновление данных после создания площадки отменено",
            "Ошибка обновления данных после создания площадки");
    }

    private void VenuesDataGridView_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        var row = venuesDataGridView.Rows[e.RowIndex];

        if (row.DataBoundItem is not VenueDto venue)
        {
            _logger.LogWarning("Не удалось получить VenueDto из строки DataGridView");
            return;
        }

        OpenTariffForm(venue);
    }

    private void OpenTariffForm(VenueDto venue)
    {
        var form = _serviceProvider.GetRequiredService<TariffForm>();
        form.LoadVenue(venue);
        form.ShowDialog();
    }

    private void TxtName_TextChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void CbVenueTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void CbRegions_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void CbDistricts_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void CbCities_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void TxtStreet_TextChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void NudRatingFrom_ValueChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void NudRatingTo_ValueChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void CbSortOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void ChkDescending_CheckedChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    #endregion

    #region Справочники

    private void ConfigureVenueTypesGrid()
    {
        dgvDirectories.AutoGenerateColumns = false;
        dgvDirectories.Columns.Clear();

        dgvDirectories.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(VenueTypeDto.Name),
            HeaderText = @"Название",
            Name = "colName",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
    }

    private void ConfigurePositionsGrid()
    {
        dgvDirectories.AutoGenerateColumns = false;
        dgvDirectories.Columns.Clear();

        dgvDirectories.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(PositionDto.Name),
            HeaderText = @"Название",
            Name = "colName",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
    }

    private async Task LoadDirectoryToGridAsync<T>(
        Func<CancellationToken, Task<Result<IReadOnlyList<T>, Errors>>> loader,
        string errorTitle,
        string? nameFilter,
        Func<T, string> nameSelector,
        DirectoryListHelper directoryHelper)
    {
        var result = await loader(_cts.Token);

        if (result.IsFailure)
        {
            ShowLoadError(errorTitle, result.Error);
            return;
        }

        var pagedItems = DirectoryItemsHelper.PreparePage(
            result.Value,
            nameFilter,
            nameSelector,
            directoryHelper);

        dgvDirectories.DataSource = new BindingSource
        {
            DataSource = pagedItems
        };

        UpdateDirectoryPagingState();
        UpdateDirectoryResetButtonState();
    }

    private Task LoadVenueTypesToGridAsync()
    {
        return LoadDirectoryToGridAsync<VenueTypeDto>(
            ct => _venueTypesQueryHandler.Handle(new GetAllVenueTypesQuery(), ct),
            LoadVenueTypesErrorTitle,
            _venueTypesFilterControl?.NameFilter,
            x => x.Name,
            _venueTypesDirectory);
    }

    private Task LoadPositionsToGridAsync()
    {
        return LoadDirectoryToGridAsync<PositionDto>(
            ct => _positionsQueryHandler.Handle(new GetAllPositionsQuery(), ct),
            LoadPositionsErrorTitle,
            _positionsFilterControl?.NameFilter,
            x => x.Name,
            _positionsDirectory);
    }

    private async Task LoadCurrentDirectoryAsync()
    {
        switch (_currentDirectoryType)
        {
            case DirectoryType.VenueTypes:
                await LoadVenueTypesToGridAsync();
                break;

            case DirectoryType.Positions:
                await LoadPositionsToGridAsync();
                break;

            case DirectoryType.Discounts:
                await LoadDiscountsToGridAsync();
                break;
        }
    }

    private TControl LoadDirectoryFilterControl<TControl>(
        Action<TControl> subscribe,
        Action<TControl> configure)
        where TControl : Control
    {
        pnlFilters.Controls.Clear();

        var control = _serviceProvider.GetRequiredService<TControl>();
        pnlFilters.Height = control.Height;
        control.Dock = DockStyle.Fill;

        subscribe(control);
        configure(control);

        pnlFilters.Controls.Add(control);
        pnlFilters.Height = control.Height;

        return control;
    }

    private void LoadVenueTypesFilterControl()
    {
        _venueTypesFilterControl = LoadDirectoryFilterControl<VenueTypesFilterUserControl>(
            control =>
            {
                control.ApplyClicked += OnVenueTypesFilterApplyClicked;
                control.ResetClicked += OnVenueTypesFilterResetClicked;
                control.FiltersChanged += OnVenueTypesFiltersChanged;
            },
            control => control.SetResetEnabled(false));
    }

    private void LoadPositionsFilterControl()
    {
        _positionsFilterControl = LoadDirectoryFilterControl<PositionsFilterUserControl>(
            control =>
            {
                control.ApplyClicked += OnPositionsFilterApplyClicked;
                control.ResetClicked += OnPositionsFilterResetClicked;
                control.FiltersChanged += OnPositionsFiltersChanged;
            },
            control => control.SetResetEnabled(false));
    }

    private bool CanGoToPreviousDirectoryPage()
    {
        return _currentDirectoryType switch
        {
            DirectoryType.VenueTypes => _venueTypesDirectory.CanGoPrevious(),
            DirectoryType.Positions => _positionsDirectory.CanGoPrevious(),
            DirectoryType.Discounts => _discountsDirectory.CanGoPrevious(),
            _ => false
        };
    }

    private bool CanGoToNextDirectoryPage()
    {
        return _currentDirectoryType switch
        {
            DirectoryType.VenueTypes => _venueTypesDirectory.CanGoNext(),
            DirectoryType.Positions => _positionsDirectory.CanGoNext(),
            DirectoryType.Discounts => _discountsDirectory.CanGoNext(),
            _ => false
        };
    }

    private void GoToPreviousDirectoryPage()
    {
        switch (_currentDirectoryType)
        {
            case DirectoryType.VenueTypes:
                _venueTypesDirectory.GoPrevious();
                break;

            case DirectoryType.Positions:
                _positionsDirectory.GoPrevious();
                break;

            case DirectoryType.Discounts:
                _discountsDirectory.GoPrevious();
                break;
        }
    }

    private void GoToNextDirectoryPage()
    {
        switch (_currentDirectoryType)
        {
            case DirectoryType.VenueTypes:
                _venueTypesDirectory.GoNext();
                break;

            case DirectoryType.Positions:
                _positionsDirectory.GoNext();
                break;

            case DirectoryType.Discounts:
                _discountsDirectory.GoNext();
                break;
        }
    }

    private int GetCurrentDirectoryPage()
    {
        return _currentDirectoryType switch
        {
            DirectoryType.VenueTypes => _venueTypesDirectory.Page,
            DirectoryType.Positions => _positionsDirectory.Page,
            DirectoryType.Discounts => _discountsDirectory.Page,
            _ => 1
        };
    }

    private int GetCurrentDirectoryTotalPages()
    {
        return _currentDirectoryType switch
        {
            DirectoryType.VenueTypes => _venueTypesDirectory.TotalPages,
            DirectoryType.Positions => _positionsDirectory.TotalPages,
            DirectoryType.Discounts => _discountsDirectory.TotalPages,
            _ => 0
        };
    }

    private int GetCurrentDirectoryTotalCount()
    {
        return _currentDirectoryType switch
        {
            DirectoryType.VenueTypes => _venueTypesDirectory.TotalCount,
            DirectoryType.Positions => _positionsDirectory.TotalCount,
            DirectoryType.Discounts => _discountsDirectory.TotalCount,
            _ => 0
        };
    }

    private void UpdateDirectoryPagingState()
    {
        var currentPage = GetCurrentDirectoryPage();
        var totalPages = GetCurrentDirectoryTotalPages();
        var totalCount = GetCurrentDirectoryTotalCount();

        btnPrevPage.Enabled = !_isLoading && currentPage > 1;
        btnNextPage.Enabled = !_isLoading && currentPage < totalPages;

        label12.Text = totalCount == 0
            ? "Количество записей: 0"
            : $"Количество записей: {totalCount}, стр. {currentPage} из {totalPages}";
    }

    private async void BtnVenueTypes_Click(object sender, EventArgs e)
    {
        await RunUiActionAsync(
            () => OpenDirectoryAsync(DirectoryType.VenueTypes),
            "Загрузка типов площадок отменена",
            "Ошибка загрузки типов площадок");
    }

    private async void BtnPositions_Click(object sender, EventArgs e)
    {
        await RunUiActionAsync(
            () => OpenDirectoryAsync(DirectoryType.Positions),
            "Загрузка позиций отменена",
            "Ошибка загрузки позиций");
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
    }

    private async Task OpenDirectoryAsync(DirectoryType directoryType)
    {
        _currentDirectoryType = directoryType;
        UpdateDirectoryButtonsState();

        switch (directoryType)
        {
            case DirectoryType.VenueTypes:
                LoadVenueTypesFilterControl();
                ConfigureVenueTypesGrid();
                await LoadVenueTypesToGridAsync();
                break;

            case DirectoryType.Positions:
                LoadPositionsFilterControl();
                ConfigurePositionsGrid();
                await LoadPositionsToGridAsync();
                break;

            case DirectoryType.Discounts:
                LoadDiscountsFilterControl();
                ConfigureDiscountsGrid();
                await LoadDiscountsToGridAsync();
                break;
        }
    }

    private async void OnVenueTypesFilterApplyClicked()
    {
        await RunUiActionAsync(
            async () =>
            {
                _venueTypesDirectory.ResetPage();
                await LoadVenueTypesToGridAsync();
            },
            "Применение фильтра типов площадок отменено",
            "Ошибка применения фильтра типов площадок");
    }

    private async void OnVenueTypesFilterResetClicked()
    {
        await RunUiActionAsync(
            async () =>
            {
                _venueTypesFilterControl?.ResetFilters();
                _venueTypesDirectory.ResetPage();
                await LoadVenueTypesToGridAsync();
            },
            "Сброс фильтра типов площадок отменен",
            "Ошибка сброса фильтра типов площадок");
    }

    private async void OnPositionsFilterApplyClicked()
    {
        await RunUiActionAsync(
            async () =>
            {
                _positionsDirectory.ResetPage();
                await LoadPositionsToGridAsync();
            },
            "Применение фильтра позиций отменено",
            "Ошибка применения фильтра позиций");
    }

    private async void OnPositionsFilterResetClicked()
    {
        await RunUiActionAsync(
            async () =>
            {
                _positionsFilterControl?.ResetFilters();
                _positionsDirectory.ResetPage();
                await LoadPositionsToGridAsync();
            },
            "Сброс фильтра позиций отменен",
            "Ошибка сброса фильтра позиций");
    }

    private async void OnVenueTypeCreated(string _)
    {
        await RunUiActionAsync(
            async () =>
            {
                _venueTypesFilterControl?.ResetFilters();
                _venueTypesDirectory.ResetPage();
                await LoadVenueTypesToGridAsync();
                await LoadVenueTypesToComboboxAsync();
                UpdateDirectoryResetButtonState();
            },
            "Обновление списка типов площадок отменено",
            "Ошибка обновления списка типов площадок");
    }

    private async void OnPositionCreated(string _)
    {
        await RunUiActionAsync(
            async () =>
            {
                _positionsFilterControl?.ResetFilters();
                _positionsDirectory.ResetPage();
                await LoadPositionsToGridAsync();
                UpdateDirectoryResetButtonState();
            },
            "Обновление списка позиций отменено",
            "Ошибка обновления списка позиций");
    }

    private async void BtnPrevPage_Click(object sender, EventArgs e)
    {
        if (_isLoading || tabControl1.SelectedTab != tabPage2 || !CanGoToPreviousDirectoryPage())
        {
            return;
        }

        await RunUiActionAsync(
            async () =>
            {
                GoToPreviousDirectoryPage();
                await LoadCurrentDirectoryAsync();
            },
            "Переход на предыдущую страницу справочника отменен",
            "Ошибка загрузки предыдущей страницы справочника");
    }

    private async void BtnNextPage_Click(object sender, EventArgs e)
    {
        if (_isLoading || tabControl1.SelectedTab != tabPage2 || !CanGoToNextDirectoryPage())
        {
            return;
        }

        await RunUiActionAsync(
            async () =>
            {
                GoToNextDirectoryPage();
                await LoadCurrentDirectoryAsync();
            },
            "Переход на следующую страницу справочника отменен",
            "Ошибка загрузки следующей страницы справочника");
    }

    private void BtnAdd_Click(object sender, EventArgs e)
    {
        switch (_currentDirectoryType)
        {
            case DirectoryType.VenueTypes:
                OpenVenueTypeForm();
                break;

            case DirectoryType.Positions:
                OpenPositionForm();
                break;

            case DirectoryType.Discounts:
                OpenDiscountForm();
                break;
        }
    }

    private void OpenVenueTypeForm()
    {
        var form = _serviceProvider.GetRequiredService<VenueTypeForm>();
        form.VenueTypeCreated += OnVenueTypeCreated;
        form.ShowDialog();
        form.VenueTypeCreated -= OnVenueTypeCreated;
    }

    private void OpenPositionForm()
    {
        var form = _serviceProvider.GetRequiredService<PositionForm>();
        form.PositionCreated += OnPositionCreated;
        form.ShowDialog();
        form.PositionCreated -= OnPositionCreated;
    }

    private bool HasActiveVenueTypeFilters()
    {
        return !string.IsNullOrWhiteSpace(_venueTypesFilterControl?.NameFilter);
    }

    private bool HasActivePositionFilters()
    {
        return !string.IsNullOrWhiteSpace(_positionsFilterControl?.NameFilter);
    }

    private void UpdateDirectoryResetButtonState()
    {
        switch (_currentDirectoryType)
        {
            case DirectoryType.VenueTypes:
                _venueTypesFilterControl?.SetResetEnabled(HasActiveVenueTypeFilters());
                break;

            case DirectoryType.Positions:
                _positionsFilterControl?.SetResetEnabled(HasActivePositionFilters());
                break;

            case DirectoryType.Discounts:
                _discountsFilterControl?.SetResetEnabled(HasActiveDiscountFilters());
                break;
        }
    }

    private void OnVenueTypesFiltersChanged()
    {
        UpdateDirectoryResetButtonState();
    }

    private void OnPositionsFiltersChanged()
    {
        UpdateDirectoryResetButtonState();
    }

    private void UpdateDirectoryButtonsState()
    {
        btnVenueTypes.BackColor = SystemColors.Control;
        btnPositions.BackColor = SystemColors.Control;
        btnEmployees.BackColor = SystemColors.Control;
        btnCustomers.BackColor = SystemColors.Control;
        btnDiscounts.BackColor = SystemColors.Control;

        btnVenueTypes.Enabled = true;
        btnPositions.Enabled = true;
        btnEmployees.Enabled = true;
        btnCustomers.Enabled = true;
        btnDiscounts.Enabled = true;

        switch (_currentDirectoryType)
        {
            case DirectoryType.VenueTypes:
                btnVenueTypes.BackColor = Color.LightBlue;
                btnVenueTypes.Enabled = false;
                break;

            case DirectoryType.Positions:
                btnPositions.BackColor = Color.LightBlue;
                btnPositions.Enabled = false;
                break;

            case DirectoryType.Employees:
                btnEmployees.BackColor = Color.LightBlue;
                btnEmployees.Enabled = false;
                break;

            case DirectoryType.Customers:
                btnCustomers.BackColor = Color.LightBlue;
                btnCustomers.Enabled = false;
                break;

            case DirectoryType.Discounts:
                btnDiscounts.BackColor = Color.LightBlue;
                btnDiscounts.Enabled = false;
                break;

            case DirectoryType.None:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #endregion

    #region Скидки

    private async Task LoadDiscountsToGridAsync()
    {
        var result = await _discountsQueryHandler.Handle(
            new GetAllDiscountsQuery(),
            _cts.Token);

        if (result.IsFailure)
        {
            ShowLoadError(LoadDiscountsErrorTitle, result.Error);
            return;
        }

        IEnumerable<DiscountDto> discounts = result.Value;

        var nameFilter = _discountsFilterControl?.NameFilter;
        if (!string.IsNullOrWhiteSpace(nameFilter))
        {
            discounts = discounts.Where(x =>
                x.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase));
        }

        var percentFrom = _discountsFilterControl?.PercentFrom;
        if (percentFrom.HasValue)
        {
            discounts = discounts.Where(x => (decimal)x.Percent >= percentFrom.Value);
        }

        var percentTo = _discountsFilterControl?.PercentTo;
        if (percentTo.HasValue)
        {
            discounts = discounts.Where(x => (decimal)x.Percent <= percentTo.Value);
        }

        var minTotalFrom = _discountsFilterControl?.MinTotalFrom;
        if (minTotalFrom.HasValue)
        {
            discounts = discounts.Where(x => x.MinTotal >= minTotalFrom.Value);
        }

        var minTotalTo = _discountsFilterControl?.MinTotalTo;
        if (minTotalTo.HasValue)
        {
            discounts = discounts.Where(x => x.MinTotal <= minTotalTo.Value);
        }

        var filteredItems = discounts
            .OrderBy(x => x.Name)
            .ToList();

        var pagedItems = _discountsDirectory.ApplyPaging(filteredItems);

        dgvDirectories.DataSource = new BindingSource
        {
            DataSource = pagedItems
        };

        UpdateDirectoryPagingState();
        UpdateDirectoryResetButtonState();
    }

    private bool HasActiveDiscountFilters()
    {
        return !string.IsNullOrWhiteSpace(_discountsFilterControl?.NameFilter) ||
               _discountsFilterControl?.PercentFrom is not null ||
               _discountsFilterControl?.PercentTo is not null ||
               _discountsFilterControl?.MinTotalFrom is not null ||
               _discountsFilterControl?.MinTotalTo is not null;
    }

    private void ConfigureDiscountsGrid()
    {
        dgvDirectories.AutoGenerateColumns = false;
        dgvDirectories.Columns.Clear();

        dgvDirectories.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(DiscountDto.Name),
            HeaderText = @"Название",
            Name = "colName",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        dgvDirectories.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(DiscountDto.Percent),
            HeaderText = @"Процент",
            Name = "colPercent"
        });

        dgvDirectories.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(DiscountDto.MinTotal),
            HeaderText = @"Мин. сумма",
            Name = "colMinTotal"
        });
    }

    private void LoadDiscountsFilterControl()
    {
        _discountsFilterControl = LoadDirectoryFilterControl<DiscountsFilterUserControl>(
            control =>
            {
                control.ApplyClicked += OnDiscountsFilterApplyClicked;
                control.ResetClicked += OnDiscountsFilterResetClicked;
                control.FiltersChanged += OnDiscountsFiltersChanged;
            },
            control => control.SetResetEnabled(false));
    }

    private async void BtnDiscounts_Click(object sender, EventArgs e)
    {
        await RunUiActionAsync(
            () => OpenDirectoryAsync(DirectoryType.Discounts),
            "Загрузка скидок отменена",
            "Ошибка загрузки скидок");
    }

    private async void OnDiscountsFilterApplyClicked()
    {
        await RunUiActionAsync(
            async () =>
            {
                _discountsDirectory.ResetPage();
                await LoadDiscountsToGridAsync();
            },
            "Применение фильтра скидок отменено",
            "Ошибка применения фильтра скидок");
    }

    private async void OnDiscountsFilterResetClicked()
    {
        await RunUiActionAsync(
            async () =>
            {
                _discountsFilterControl?.ResetFilters();
                _discountsDirectory.ResetPage();
                await LoadDiscountsToGridAsync();
            },
            "Сброс фильтра скидок отменен",
            "Ошибка сброса фильтра скидок");
    }

    private void OnDiscountsFiltersChanged()
    {
        UpdateDirectoryResetButtonState();
    }
    
    private void OpenDiscountForm()
    {
        var form = _serviceProvider.GetRequiredService<DiscountForm>();
        form.DiscountCreated += OnDiscountCreated;
        form.ShowDialog();
        form.DiscountCreated -= OnDiscountCreated;
    }
    
    private async void OnDiscountCreated(string _)
    {
        await RunUiActionAsync(
            async () =>
            {
                _discountsFilterControl?.ResetFilters();
                _discountsDirectory.ResetPage();
                await LoadDiscountsToGridAsync();
                UpdateDirectoryResetButtonState();
            },
            "Обновление списка скидок отменено",
            "Ошибка обновления списка скидок");
    }
    
    
    #endregion

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
}