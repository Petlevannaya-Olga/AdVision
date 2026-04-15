using System.Linq.Expressions;
using AdVision.Application.Venues.GetDistinctQuery;
using AdVision.Application.Venues.GetVenuesQuery;
using AdVision.Application.VenueTypes.GetAllVenueTypesQuery;
using AdVision.Contracts;
using AdVision.Domain.Venues;
using AdVision.Domain.VenueTypes;
using AdVision.Infrastructure;
using AdVision.Presentation.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.Extensions;

namespace AdVision.Presentation;

public partial class MainForm : Form
{
    private const string LoadVenuesErrorTitle = "Ошибка загрузки площадок";
    private const string LoadVenueTypesErrorTitle = "Ошибка загрузки типов площадок";
    private const string LoadRegionsErrorTitle = "Ошибка загрузки регионов";
    private const string LoadDistrictsErrorTitle = "Ошибка загрузки районов";
    private const string LoadCitiesErrorTitle = "Ошибка загрузки городов";
    private const string UnknownErrorTitle = "Непредвиденная ошибка";
    private const string DefaultLoadErrorMessage = "Не удалось загрузить данные";

    private const int PageSize = 10;
    private const int DirectoryPageSize = 10;

    private readonly CancellationTokenSource _cts = new();
    private readonly INotificationService _notificationService;
    private readonly IQueryHandler<IReadOnlyList<string>, GetDistinctQuery> _getDistinctQueryHandler;
    private readonly IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> _venueTypesQueryHandler;
    private readonly IQueryHandler<PagedResult<VenueDto>, GetVenuesQuery> _venuesQueryHandler;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MainForm> _logger;

    private bool _isLoading;

    // Площадки
    private int _page = 1;
    private int _totalCount;

    // Справочники -> Типы площадок
    private int _venueTypesPage = 1;
    private int _venueTypesTotalCount;
    private bool _directoriesTabInitialized;
    private DirectoryType _currentDirectoryType = DirectoryType.None;
    private VenueTypesFilterUserControl? _venueTypesFilterControl;

    private int TotalPages => _totalCount == 0
        ? 0
        : (int)Math.Ceiling((double)_totalCount / PageSize);

    private int VenueTypesTotalPages => _venueTypesTotalCount == 0
        ? 0
        : (int)Math.Ceiling((double)_venueTypesTotalCount / DirectoryPageSize);

    public MainForm(
        INotificationService notificationService,
        IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> venueTypesQueryHandler,
        IQueryHandler<IReadOnlyList<string>, GetDistinctQuery> getDistinctQueryHandler,
        IQueryHandler<PagedResult<VenueDto>, GetVenuesQuery> venuesQueryHandler,
        IServiceProvider serviceProvider,
        ILogger<MainForm> logger)
    {
        _notificationService = notificationService;
        _venueTypesQueryHandler = venueTypesQueryHandler;
        _getDistinctQueryHandler = getDistinctQueryHandler;
        _venuesQueryHandler = venuesQueryHandler;
        _serviceProvider = serviceProvider;
        _logger = logger;

        InitializeComponent();

        ConfigureVenuesGrid();
        UpdatePagingState();
        UpdateVenueTypesPagingState();

        venuesDataGridView.CellDoubleClick += VenuesDataGridView_CellDoubleClick;
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        try
        {
            await InitializeAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Инициализация главной формы отменена");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка инициализации главной формы");
            _notificationService.ShowError(UnknownErrorTitle, ex.Message);
        }
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
        UpdateVenueTypesPagingState();

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
            UpdateVenueTypesPagingState();
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

        try
        {
            _page = 1;
            await LoadVenuesAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Применение фильтров отменено");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка применения фильтров");
            _notificationService.ShowError(UnknownErrorTitle, ex.Message);
        }

        UpdateVenueResetButtonState();
    }

    private async void BtnReset_Click(object sender, EventArgs e)
    {
        if (_isLoading || tabControl1.SelectedTab == tabPage2)
        {
            return;
        }

        try
        {
            ResetVenueFilters();
            _page = 1;
            await LoadVenuesAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Сброс фильтров отменен");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка сброса фильтров");
            _notificationService.ShowError(UnknownErrorTitle, ex.Message);
        }

        UpdateVenueResetButtonState();
    }

    private async void BtnPrev_Click(object sender, EventArgs e)
    {
        if (_isLoading || _page <= 1)
        {
            return;
        }

        try
        {
            _page--;
            await LoadVenuesAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Переход на предыдущую страницу отменен");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка загрузки предыдущей страницы");
            _notificationService.ShowError(UnknownErrorTitle, ex.Message);
        }
    }

    private async void BtnNext_Click(object sender, EventArgs e)
    {
        if (_isLoading || _page >= TotalPages)
        {
            return;
        }

        try
        {
            _page++;
            await LoadVenuesAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Переход на следующую страницу отменен");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка загрузки следующей страницы");
            _notificationService.ShowError(UnknownErrorTitle, ex.Message);
        }
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
        try
        {
            _page = 1;
            await RefreshDataAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Обновление данных после создания площадки отменено");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка обновления данных после создания площадки");
            _notificationService.ShowError(UnknownErrorTitle, ex.Message);
        }
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
            HeaderText = "Название",
            Name = "colName",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
    }

    private async Task LoadVenueTypesToGridAsync()
    {
        var result = await _venueTypesQueryHandler.Handle(
            new GetAllVenueTypesQuery(),
            _cts.Token);

        if (result.IsFailure)
        {
            ShowLoadError(LoadVenueTypesErrorTitle, result.Error);
            return;
        }

        IEnumerable<VenueTypeDto> venueTypes = result.Value;

        var nameFilter = _venueTypesFilterControl?.NameFilter;
        if (!string.IsNullOrWhiteSpace(nameFilter))
        {
            venueTypes = venueTypes.Where(x =>
                x.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase));
        }

        var filteredItems = venueTypes
            .OrderBy(x => x.Name)
            .ToList();

        _venueTypesTotalCount = filteredItems.Count;

        if (_venueTypesPage > VenueTypesTotalPages && VenueTypesTotalPages > 0)
        {
            _venueTypesPage = VenueTypesTotalPages;
        }

        if (_venueTypesPage <= 0)
        {
            _venueTypesPage = 1;
        }

        var pagedItems = filteredItems
            .Skip((_venueTypesPage - 1) * DirectoryPageSize)
            .Take(DirectoryPageSize)
            .ToList();

        dgvDirectories.DataSource = new BindingSource
        {
            DataSource = pagedItems
        };

        UpdateVenueTypesPagingState();
        UpdateVenueTypesResetButtonState();
    }

    private void UpdateVenueTypesPagingState()
    {
        btnPrevPage.Enabled = !_isLoading && _venueTypesPage > 1;
        btnNextPage.Enabled = !_isLoading && _venueTypesPage < VenueTypesTotalPages;

        label12.Text = _venueTypesTotalCount == 0
            ? "Количество записей: 0"
            : $"Количество записей: {_venueTypesTotalCount}, стр. {_venueTypesPage} из {VenueTypesTotalPages}";
    }

    private async void BtnVenueTypes_Click(object sender, EventArgs e)
    {
        _currentDirectoryType = DirectoryType.VenueTypes;
        await OpenVenueTypesAsync(true);
    }

    private async void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tabControl1.SelectedTab == tabPage2 && !_directoriesTabInitialized)
        {
            _directoriesTabInitialized = true;
            await OpenVenueTypesAsync();
        }
    }

    private async Task OpenVenueTypesAsync(bool highlightButton = false)
    {
        _currentDirectoryType = DirectoryType.VenueTypes;

        try
        {
            LoadVenueTypesFilterControl();
            ConfigureVenueTypesGrid();
            await LoadVenueTypesToGridAsync();

            if (highlightButton)
            {
                btnVenueTypes.BackColor = Color.LightBlue;
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Загрузка типов площадок отменена");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка загрузки типов площадок");
            _notificationService.ShowError(UnknownErrorTitle, ex.Message);
        }
    }

    private void LoadVenueTypesFilterControl()
    {
        pnlFilters.Controls.Clear();

        _venueTypesFilterControl = _serviceProvider.GetRequiredService<VenueTypesFilterUserControl>();
        pnlFilters.Height = _venueTypesFilterControl.Height;
        _venueTypesFilterControl.Dock = DockStyle.Fill;

        _venueTypesFilterControl.ApplyClicked += OnVenueTypesFilterApplyClicked;
        _venueTypesFilterControl.ResetClicked += OnVenueTypesFilterResetClicked;
        _venueTypesFilterControl.FiltersChanged += OnVenueTypesFiltersChanged;
        _venueTypesFilterControl.SetResetEnabled(false);

        pnlFilters.Controls.Add(_venueTypesFilterControl);
        pnlFilters.Height = _venueTypesFilterControl.Height;
    }

    private async void OnVenueTypesFilterApplyClicked()
    {
        try
        {
            _venueTypesPage = 1;
            await LoadVenueTypesToGridAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Применение фильтра типов площадок отменено");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка применения фильтра типов площадок");
            _notificationService.ShowError(UnknownErrorTitle, ex.Message);
        }
    }

    private async void OnVenueTypesFilterResetClicked()
    {
        try
        {
            _venueTypesFilterControl?.ResetFilters();
            _venueTypesPage = 1;
            await LoadVenueTypesToGridAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Сброс фильтра типов площадок отменен");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка сброса фильтра типов площадок");
            _notificationService.ShowError(UnknownErrorTitle, ex.Message);
        }
    }

    private async void OnVenueTypeCreated(string _)
    {
        try
        {
            _venueTypesPage = 1;
            await LoadVenueTypesToGridAsync();
            await LoadVenueTypesToComboboxAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка обновления списка типов площадок");
            _notificationService.ShowError("Ошибка", ex.Message);
        }
    }

    private async void BtnPrevPage_Click(object sender, EventArgs e)
    {
        if (_isLoading || _venueTypesPage <= 1)
        {
            return;
        }

        try
        {
            if (tabControl1.SelectedTab == tabPage2 &&
                _currentDirectoryType == DirectoryType.VenueTypes)
            {
                _venueTypesPage--;
                await LoadVenueTypesToGridAsync();
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Переход на предыдущую страницу справочника отменен");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка загрузки предыдущей страницы справочника");
            _notificationService.ShowError(UnknownErrorTitle, ex.Message);
        }
    }

    private async void BtnNextPage_Click(object sender, EventArgs e)
    {
        if (_isLoading || _venueTypesPage >= VenueTypesTotalPages)
        {
            return;
        }

        try
        {
            if (tabControl1.SelectedTab == tabPage2 &&
                _currentDirectoryType == DirectoryType.VenueTypes)
            {
                _venueTypesPage++;
                await LoadVenueTypesToGridAsync();
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Переход на следующую страницу справочника отменен");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка загрузки следующей страницы справочника");
            _notificationService.ShowError(UnknownErrorTitle, ex.Message);
        }
    }

    private void BtnAdd_Click(object sender, EventArgs e)
    {
        switch (_currentDirectoryType)
        {
            case DirectoryType.VenueTypes:
                OpenVenueTypeForm();
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

    private bool HasActiveVenueTypeFilters()
    {
        return !string.IsNullOrWhiteSpace(_venueTypesFilterControl?.NameFilter);
    }

    private void UpdateVenueTypesResetButtonState()
    {
        _venueTypesFilterControl?.SetResetEnabled(HasActiveVenueTypeFilters());
    }

    private void OnVenueTypesFiltersChanged()
    {
        UpdateVenueTypesResetButtonState();
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