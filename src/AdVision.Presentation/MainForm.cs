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

    private readonly CancellationTokenSource _cts = new();
    private readonly INotificationService _notificationService;
    private readonly IQueryHandler<IReadOnlyList<string>, GetDistinctQuery> _getDistinctQueryHandler;
    private readonly IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> _venueTypesQueryHandler;
    private readonly IQueryHandler<PagedResult<VenueDto>, GetVenuesQuery> _venuesQueryHandler;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MainForm> _logger;

    private bool _isLoading;
    private int _page = 1;
    private int _totalCount;

    private int TotalPages => _totalCount == 0
        ? 0
        : (int)Math.Ceiling((double)_totalCount / PageSize);

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

        try
        {
            await LoadFiltersAsync();
            await LoadVenuesAsync();
        }
        finally
        {
            _isLoading = false;
            UseWaitCursor = false;
            UpdatePagingState();
        }
    }

    private void ConfigureVenuesGrid()
    {
        venuesDataGridView.AutoGenerateColumns = false;
        venuesDataGridView.Columns.Clear();

        AddTextColumn(nameof(VenueDto.Name), "Название", "colName");
        AddTextColumn(nameof(VenueDto.Type), "Тип", "colType");
        AddTextColumn(nameof(VenueDto.Region), "Регион", "colRegion");
        AddTextColumn(nameof(VenueDto.District), "Район", "colDistrict");
        AddTextColumn(nameof(VenueDto.City), "Город", "colCity");
        AddTextColumn(nameof(VenueDto.Street), "Улица", "colStreet");
        AddTextColumn(nameof(VenueDto.House), "Дом", "colHouse");
        AddTextColumn(nameof(VenueDto.Latitude), "Широта", "colLatitude");
        AddTextColumn(nameof(VenueDto.Longitude), "Долгота", "colLongitude");
        AddTextColumn(nameof(VenueDto.Width), "Ширина", "colWidth");
        AddTextColumn(nameof(VenueDto.Height), "Высота", "colHeight");
        AddTextColumn(nameof(VenueDto.Rating), "Рейтинг", "colRating");
    }

    private void AddTextColumn(string dataPropertyName, string headerText, string columnName)
    {
        venuesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = dataPropertyName,
            HeaderText = headerText,
            Name = columnName
        });
    }

    private bool IsSortDescending()
    {
        return chkDescending.Checked;
    }

    private async Task LoadVenuesAsync()
    {
        var filter = BuildFilter();
        var orderBy = BuildSort();
        var descending = IsSortDescending();

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

    private void UpdatePagingState()
    {
        btnPrev.Enabled = !_isLoading && _page > 1;
        btnNext.Enabled = !_isLoading && _page < TotalPages;
        label11.Text = $@"Количество записей: {_totalCount}, стр. {_page} из {TotalPages}";
    }

    private async Task LoadFiltersAsync()
    {
        await LoadVenueTypesAsync();
        await LoadRegionsAsync();
        await LoadDistrictsAsync();
        await LoadCitiesAsync();
        UpdateResetButtonState();
        ResetFilters();
    }

    private async Task LoadVenueTypesAsync()
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

    private void ShowLoadError(string title, IEnumerable<object>? errors)
    {
        var message = string.Join(Environment.NewLine, errors ?? []);
        var normalizedMessage = string.IsNullOrWhiteSpace(message)
            ? DefaultLoadErrorMessage
            : message;

        _logger.LogError("{Title}: {Error}", title, normalizedMessage);
        _notificationService.ShowError(title, normalizedMessage);
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

    private Expression<Func<Venue, bool>> BuildFilter()
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

            filter = filter.And(v =>
                EF.Functions.Like(v.Name.Value, pattern));
        }

        if (!string.IsNullOrWhiteSpace(selectedStreet))
        {
            var pattern = $"%{selectedStreet}%";

            filter = filter.And(v =>
                EF.Functions.Like(v.Address.Street, pattern));
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

    private Expression<Func<Venue, object>> BuildSort()
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

        UpdateResetButtonState();
    }

    private async void BtnNext_Click(object sender, EventArgs e)
    {
        if (_page >= TotalPages || _isLoading)
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

    private async void BtnPrev_Click(object sender, EventArgs e)
    {
        if (_page <= 1 || _isLoading)
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

    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        if (!_cts.IsCancellationRequested)
        {
            _cts.Cancel();
        }

        _cts.Dispose();
    }

    private async void BtnReset_Click(object sender, EventArgs e)
    {
        if (_isLoading)
        {
            return;
        }

        try
        {
            ResetFilters();
            _page = 1; // важно!
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

        UpdateResetButtonState();
    }

    private void ResetFilters()
    {
        cbVenueTypes.SelectedIndex = -1;
        cbRegions.SelectedIndex = -1;
        cbDistricts.SelectedIndex = -1;
        cbCities.SelectedIndex = -1;
        nudRatingFrom.Value = 1;
        nudRatingTo.Value = 10;
        cbSortOrder.SelectedIndex = 0;
        chkDescending.Checked = false;
    }

    private void UpdateResetButtonState()
    {
        btnReset.Enabled =
            cbVenueTypes.SelectedIndex >= 0 ||
            cbRegions.SelectedIndex >= 0 ||
            cbDistricts.SelectedIndex >= 0 ||
            cbCities.SelectedIndex >= 0 ||
            chkDescending.Checked ||
            nudRatingTo.Value > 1 ||
            nudRatingFrom.Value < 10 ||
            cbSortOrder.SelectedIndex >= 0;
    }
    
    private void VenuesDataGridView_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return; // клик по заголовку
        }

        var row = venuesDataGridView.Rows[e.RowIndex];

        if (row.DataBoundItem is not VenueDto venue)
        {
            _logger.LogWarning("Не удалось получить VenueDto из строки DataGridView");
            return;
        }

        OpenVenueForm(venue);
    }
    
    private void OpenVenueForm(VenueDto venue)
    {
        // var form = _serviceProvider.GetRequiredService<VenueForm>();
        //
        // // 👉 если форма поддерживает редактирование
        // form.LoadVenue(venue);
        //
        // form.VenueCreated += OnVenueCreated;
        //
        // form.ShowDialog();
        //
        // form.VenueCreated -= OnVenueCreated;

        MessageBox.Show("asdad");
    }
}