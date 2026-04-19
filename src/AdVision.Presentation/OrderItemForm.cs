using AdVision.Application;
using AdVision.Application.Venues.GetAvailableVenuesQuery;
using AdVision.Application.Venues.GetDistinctQuery;
using AdVision.Application.VenueTypes.GetAllVenueTypesQuery;
using AdVision.Contracts;
using AdVision.Domain.Venues;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation;

public partial class OrderItemForm : Form
{
    private const int PageSize = 20;

    private readonly IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> _venueTypesQueryHandler;
    private readonly IQueryHandler<PagedResult<AvailableVenueDto>, GetAvailableVenuesQuery> _availableVenuesQueryHandler;
    private readonly IQueryHandler<IReadOnlyList<string>, GetDistinctQuery> _getDistinctQueryHandler;
    private readonly ILogger<OrderItemForm> _logger;

    private bool _isLoading;
    private int _page = 1;
    private int _totalPages;

    public SelectedPositionDto? SelectedPosition { get; private set; }

    public OrderItemForm(
        IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> venueTypesQueryHandler,
        IQueryHandler<PagedResult<AvailableVenueDto>, GetAvailableVenuesQuery> availableVenuesQueryHandler,
        IQueryHandler<IReadOnlyList<string>, GetDistinctQuery> getDistinctQueryHandler,
        ILogger<OrderItemForm> logger)
    {
        _venueTypesQueryHandler = venueTypesQueryHandler;
        _availableVenuesQueryHandler = availableVenuesQueryHandler;
        _getDistinctQueryHandler = getDistinctQueryHandler;
        _logger = logger;

        InitializeComponent();

        ConfigureVenuesGrid();

        btnAdd.Click += BtnAdd_Click;
        btnApply.Click += BtnApply_Click;
        btnReset.Click += BtnReset_Click;
        btnPrevPage.Click += BtnPrevPage_Click;
        btnNextPage.Click += BtnNextPage_Click;

        dgvVenues.CellDoubleClick += DgvVenues_CellDoubleClick;
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        _isLoading = true;
        try
        {
            dtpBookingFrom.Value = DateTime.Today;
            dtpBookingTo.Value = DateTime.Today;

            await LoadVenueFiltersAsync();
        }
        finally
        {
            _isLoading = false;
        }

        await LoadVenuesAsync();
    }

    private void ConfigureVenuesGrid()
    {
        dgvVenues.AutoGenerateColumns = false;
        dgvVenues.Columns.Clear();
        dgvVenues.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvVenues.MultiSelect = false;
        dgvVenues.ReadOnly = true;

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueDto.VenueName),
            HeaderText = @"Название",
            Name = "colName",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueDto.VenueTypeName),
            HeaderText = @"Тип",
            Name = "colType"
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueDto.Region),
            HeaderText = @"Регион",
            Name = "colRegion"
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueDto.District),
            HeaderText = @"Район",
            Name = "colDistrict"
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueDto.City),
            HeaderText = @"Город",
            Name = "colCity"
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueDto.Street),
            HeaderText = @"Улица",
            Name = "colStreet"
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueDto.Rating),
            HeaderText = @"Рейтинг",
            Name = "colRating"
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueDto.Price),
            HeaderText = @"Цена",
            Name = "colPrice"
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueDto.TariffStartDate),
            HeaderText = @"Тариф с",
            Name = "colTariffStart"
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueDto.TariffEndDate),
            HeaderText = @"Тариф до",
            Name = "colTariffEnd"
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueDto.FreeDaysCount),
            HeaderText = @"Свободных дней",
            Name = "colFreeDays"
        });

        dgvVenues.Columns.Add(new DataGridViewCheckBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueDto.HasPartialAvailability),
            HeaderText = @"Частично доступна",
            Name = "colPartial"
        });
    }

    private async Task LoadVenueFiltersAsync()
    {
        await LoadVenueTypesAsync();
        await LoadRegionsAsync();
        await LoadDistrictsAsync();
        await LoadCitiesAsync();

        ResetVenueFilters();
        UpdateResetButtonState();
    }

    private async Task LoadVenueTypesAsync()
    {
        cbVenueTypes.DataSource = null;

        var result = await _venueTypesQueryHandler.Handle(
            new GetAllVenueTypesQuery(),
            CancellationToken.None);

        if (result.IsFailure)
        {
            ShowError("Ошибка загрузки типов площадок", result.Error);
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
            "Ошибка загрузки регионов");
    }

    private Task LoadDistrictsAsync()
    {
        return LoadDistinctAsync(
            cbDistricts,
            new GetDistinctQuery(v => v.Address.District),
            "Ошибка загрузки районов");
    }

    private Task LoadCitiesAsync()
    {
        return LoadDistinctAsync(
            cbCities,
            new GetDistinctQuery(v => v.Address.City),
            "Ошибка загрузки городов");
    }

    private async Task LoadDistinctAsync(
        ComboBox comboBox,
        GetDistinctQuery query,
        string errorTitle)
    {
        comboBox.DataSource = null;

        var result = await _getDistinctQueryHandler.Handle(query, CancellationToken.None);

        if (result.IsFailure)
        {
            ShowError(errorTitle, result.Error);
            return;
        }

        comboBox.DataSource = result.Value;
        comboBox.SelectedIndex = -1;
    }

    private async Task LoadVenuesAsync()
    {
        if (_isLoading)
        {
            return;
        }

        var dateFrom = DateOnly.FromDateTime(dtpDateFrom.Value);
        var dateTo = DateOnly.FromDateTime(dtpDateTo.Value);

        if (dateFrom > dateTo)
        {
            MessageBox.Show(
                "Дата начала фильтра не может быть больше даты окончания",
                "Фильтр площадок",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        var result = await _availableVenuesQueryHandler.Handle(
            BuildVenuesQuery(dateFrom, dateTo),
            CancellationToken.None);

        if (result.IsFailure)
        {
            ShowError("Ошибка загрузки площадок", result.Error);
            return;
        }

        var paged = result.Value;

        if (paged.TotalPages > 0 && paged.Page > paged.TotalPages)
        {
            _page = paged.TotalPages;

            result = await _availableVenuesQueryHandler.Handle(
                BuildVenuesQuery(dateFrom, dateTo),
                CancellationToken.None);

            if (result.IsFailure)
            {
                ShowError("Ошибка загрузки площадок", result.Error);
                return;
            }

            paged = result.Value;
        }

        if (paged.TotalPages == 0)
        {
            _page = 1;
            _totalPages = 0;
        }
        else
        {
            _page = paged.Page;
            _totalPages = paged.TotalPages;
        }

        dgvVenues.DataSource = new BindingSource
        {
            DataSource = paged.Items
        };

        UpdatePagingState(paged);
    }

    private GetAvailableVenuesQuery BuildVenuesQuery(DateOnly dateFrom, DateOnly dateTo)
    {
        return new GetAvailableVenuesQuery(
            _page,
            PageSize,
            string.IsNullOrWhiteSpace(txtName.Text) ? null : txtName.Text.Trim(),
            cbVenueTypes.SelectedValue is Guid venueTypeId ? venueTypeId : null,
            cbRegions.SelectedItem as string,
            cbDistricts.SelectedItem as string,
            cbCities.SelectedItem as string,
            string.IsNullOrWhiteSpace(txtStreet.Text) ? null : txtStreet.Text.Trim(),
            (int)nudRatingFrom.Value,
            (int)nudRatingTo.Value,
            nudPriceFrom.Value > 0 ? nudPriceFrom.Value : null,
            nudPriceTo.Value > 0 ? nudPriceTo.Value : null,
            dateFrom,
            dateTo);
    }

    private void ResetVenueFilters()
    {
        cbVenueTypes.SelectedIndex = -1;
        cbRegions.SelectedIndex = -1;
        cbDistricts.SelectedIndex = -1;
        cbCities.SelectedIndex = -1;

        txtName.Clear();
        txtStreet.Clear();

        nudRatingFrom.Value = 1;
        nudRatingTo.Value = 10;
        nudPriceFrom.Value = 0;
        nudPriceTo.Value = 0;

        dtpDateFrom.Value = DateTime.Today;
        dtpDateTo.Value = DateTime.Today;
    }

    private void UpdateResetButtonState()
    {
        btnReset.Enabled =
            cbVenueTypes.SelectedIndex >= 0 ||
            cbRegions.SelectedIndex >= 0 ||
            cbDistricts.SelectedIndex >= 0 ||
            cbCities.SelectedIndex >= 0 ||
            !string.IsNullOrWhiteSpace(txtName.Text) ||
            !string.IsNullOrWhiteSpace(txtStreet.Text) ||
            nudRatingFrom.Value > 1 ||
            nudRatingTo.Value < 10 ||
            nudPriceFrom.Value > 0 ||
            nudPriceTo.Value > 0 ||
            DateOnly.FromDateTime(dtpDateFrom.Value) != DateOnly.FromDateTime(DateTime.Today) ||
            DateOnly.FromDateTime(dtpDateTo.Value) != DateOnly.FromDateTime(DateTime.Today);
    }

    private void UpdatePagingState(PagedResult<AvailableVenueDto> paged)
    {
        btnPrevPage.Enabled = !_isLoading && paged.TotalPages > 0 && paged.Page > 1;
        btnNextPage.Enabled = !_isLoading && paged.TotalPages > 0 && paged.Page < paged.TotalPages;

        lblPageInfo.Text = paged.TotalPages == 0
            ? "Нет данных"
            : $"Страница {paged.Page} из {paged.TotalPages}";
    }

    private async void BtnApply_Click(object? sender, EventArgs e)
    {
        if (_isLoading)
        {
            return;
        }

        _page = 1;
        await LoadVenuesAsync();
        UpdateResetButtonState();
    }

    private async void BtnReset_Click(object? sender, EventArgs e)
    {
        if (_isLoading)
        {
            return;
        }

        ResetVenueFilters();
        _page = 1;
        await LoadVenuesAsync();
        UpdateResetButtonState();
    }

    private async void BtnPrevPage_Click(object? sender, EventArgs e)
    {
        if (_isLoading || _page <= 1)
        {
            return;
        }

        _page--;
        await LoadVenuesAsync();
    }

    private async void BtnNextPage_Click(object? sender, EventArgs e)
    {
        if (_isLoading || _page >= _totalPages)
        {
            return;
        }

        _page++;
        await LoadVenuesAsync();
    }

    private async void BtnAdd_Click(object? sender, EventArgs e)
    {
        await AddSelectedVenueAsync();
    }

    private async void DgvVenues_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        await AddSelectedVenueAsync();
    }

    private async Task AddSelectedVenueAsync()
    {
        if (dgvVenues.CurrentRow?.DataBoundItem is not AvailableVenueDto venue)
        {
            MessageBox.Show(
                "Выберите площадку",
                "Добавление позиции",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        var bookingFrom = DateOnly.FromDateTime(dtpBookingFrom.Value);
        var bookingTo = DateOnly.FromDateTime(dtpBookingTo.Value);

        if (bookingFrom > bookingTo)
        {
            MessageBox.Show(
                "Дата начала бронирования не может быть больше даты окончания",
                "Добавление позиции",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        var isAvailable = await IsVenueAvailableForBookingAsync(
            venue.TariffId,
            bookingFrom,
            bookingTo);

        if (!isAvailable)
        {
            MessageBox.Show(
                "Площадка недоступна на указанные даты бронирования",
                "Добавление позиции",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        SelectedPosition = new SelectedPositionDto(
            venue.TariffId,
            venue.VenueId,
            venue.VenueName,
            venue.Price,
            bookingFrom,
            bookingTo);

        DialogResult = DialogResult.OK;
        Close();
    }

    private void TxtName_TextChanged(object sender, EventArgs e)
    {
        UpdateResetButtonState();
    }

    private void TxtStreet_TextChanged(object sender, EventArgs e)
    {
        UpdateResetButtonState();
    }

    private void CbVenueTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateResetButtonState();
    }

    private void CbRegions_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateResetButtonState();
    }

    private void CbDistricts_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateResetButtonState();
    }

    private void CbCities_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateResetButtonState();
    }

    private void NudRatingFrom_ValueChanged(object sender, EventArgs e)
    {
        UpdateResetButtonState();
    }

    private void NudRatingTo_ValueChanged(object sender, EventArgs e)
    {
        UpdateResetButtonState();
    }

    private void NudPriceFrom_ValueChanged(object sender, EventArgs e)
    {
        UpdateResetButtonState();
    }

    private void NudPriceTo_ValueChanged(object sender, EventArgs e)
    {
        UpdateResetButtonState();
    }

    private void DtpDateFrom_ValueChanged(object sender, EventArgs e)
    {
        UpdateResetButtonState();
    }

    private void DtpDateTo_ValueChanged(object sender, EventArgs e)
    {
        UpdateResetButtonState();
    }

    private void ShowError(string title, IEnumerable<object> errors)
    {
        var message = string.Join(Environment.NewLine, errors);

        _logger.LogError("{Title}: {Message}", title, message);

        MessageBox.Show(
            string.IsNullOrWhiteSpace(message) ? "Не удалось загрузить данные" : message,
            title,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    private async Task<bool> IsVenueAvailableForBookingAsync(
        Guid tariffId,
        DateOnly bookingFrom,
        DateOnly bookingTo)
    {
        var result = await _availableVenuesQueryHandler.Handle(
            new GetAvailableVenuesQuery(
                1,
                int.MaxValue,
                string.IsNullOrWhiteSpace(txtName.Text) ? null : txtName.Text.Trim(),
                cbVenueTypes.SelectedValue is Guid venueTypeId ? venueTypeId : null,
                cbRegions.SelectedItem as string,
                cbDistricts.SelectedItem as string,
                cbCities.SelectedItem as string,
                string.IsNullOrWhiteSpace(txtStreet.Text) ? null : txtStreet.Text.Trim(),
                (int)nudRatingFrom.Value,
                (int)nudRatingTo.Value,
                nudPriceFrom.Value > 0 ? nudPriceFrom.Value : null,
                nudPriceTo.Value > 0 ? nudPriceTo.Value : null,
                bookingFrom,
                bookingTo),
            CancellationToken.None);

        if (result.IsFailure)
        {
            ShowError("Ошибка проверки доступности площадки", result.Error);
            return false;
        }

        return result.Value.Items.Any(x => x.TariffId == tariffId);
    }
}