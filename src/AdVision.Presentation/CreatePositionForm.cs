using AdVision.Application.Venues.GetAvailableVenuesForPositionQuery;
using AdVision.Application.VenueTypes.GetAllVenueTypesQuery;
using AdVision.Contracts;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation;

public partial class CreatePositionForm : Form
{
    private readonly IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> _venueTypesQueryHandler;
    private readonly IQueryHandler<IReadOnlyList<AvailableVenueForPositionDto>, GetAvailableVenuesForPositionQuery> _availableVenuesQueryHandler;
    private readonly ILogger<CreatePositionForm> _logger;

    public SelectedPositionDto? SelectedPosition { get; private set; }

    public CreatePositionForm(
        IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> venueTypesQueryHandler,
        IQueryHandler<IReadOnlyList<AvailableVenueForPositionDto>, GetAvailableVenuesForPositionQuery> availableVenuesQueryHandler,
        ILogger<CreatePositionForm> logger)
    {
        _venueTypesQueryHandler = venueTypesQueryHandler;
        _availableVenuesQueryHandler = availableVenuesQueryHandler;
        _logger = logger;

        InitializeComponent();

        ConfigureVenuesGrid();
        btnAdd.Click += BtnAdd_Click;
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        await LoadVenueTypesAsync();
        await LoadRegionsAsync();
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
            DataPropertyName = nameof(AvailableVenueForPositionDto.VenueName),
            HeaderText = @"Название",
            Name = "colName",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueForPositionDto.VenueTypeName),
            HeaderText = @"Тип",
            Name = "colType"
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueForPositionDto.Region),
            HeaderText = @"Регион",
            Name = "colRegion"
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueForPositionDto.City),
            HeaderText = @"Город",
            Name = "colCity"
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueForPositionDto.Street),
            HeaderText = @"Улица",
            Name = "colStreet"
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueForPositionDto.Rating),
            HeaderText = @"Рейтинг",
            Name = "colRating"
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueForPositionDto.Price),
            HeaderText = @"Цена",
            Name = "colPrice"
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueForPositionDto.TariffStartDate),
            HeaderText = @"Тариф с",
            Name = "colTariffStart"
        });

        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueForPositionDto.TariffEndDate),
            HeaderText = @"Тариф до",
            Name = "colTariffEnd"
        });
        
        dgvVenues.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueForPositionDto.FreeDaysCount),
            HeaderText = @"Свободных дней",
            Name = "colFreeDays"
        });

        dgvVenues.Columns.Add(new DataGridViewCheckBoxColumn
        {
            DataPropertyName = nameof(AvailableVenueForPositionDto.HasPartialAvailability),
            HeaderText = @"Частично доступна",
            Name = "colPartial"
        });
    }

    private async Task LoadVenueTypesAsync()
    {
        var result = await _venueTypesQueryHandler.Handle(
            new GetAllVenueTypesQuery(),
            CancellationToken.None);

        if (result.IsFailure)
        {
            ShowError("Ошибка загрузки типов площадок", result.Error);
            return;
        }

        cbVenueTypes.DataSource = result.Value.ToList();
        cbVenueTypes.DisplayMember = nameof(VenueTypeDto.Name);
        cbVenueTypes.ValueMember = nameof(VenueTypeDto.Id);
        cbVenueTypes.SelectedIndex = -1;
    }

    private async Task LoadVenuesAsync()
    {
        DateOnly? dateFrom = DateOnly.FromDateTime(dtpDateFrom.Value);
        DateOnly? dateTo = DateOnly.FromDateTime(dtpDateTo.Value);

        if (dateFrom > dateTo)
        {
            ShowError("Ошибка фильтра", new[] { "Дата начала не может быть больше даты окончания" });
            return;
        }

        var result = await _availableVenuesQueryHandler.Handle(
            new GetAvailableVenuesForPositionQuery(
                string.IsNullOrWhiteSpace(txtName.Text) ? null : txtName.Text.Trim(),
                cbVenueTypes.SelectedValue is Guid venueTypeId ? venueTypeId : null,
                cbRegions.SelectedItem?.ToString(),
                cbDistricts.SelectedItem?.ToString(),
                cbCities.SelectedItem?.ToString(),
                string.IsNullOrWhiteSpace(txtStreet.Text) ? null : txtStreet.Text.Trim(),
                (int)nudRatingFrom.Value,
                (int)nudRatingTo.Value,
                nudPriceFrom.Value > 0 ? nudPriceFrom.Value : null,
                nudPriceTo.Value > 0 ? nudPriceTo.Value : null,
                dateFrom,
                dateTo),
            CancellationToken.None);

        if (result.IsFailure)
        {
            ShowError("Ошибка загрузки площадок", result.Error);
            return;
        }

        dgvVenues.DataSource = new BindingSource
        {
            DataSource = result.Value
        };
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        if (dgvVenues.CurrentRow?.DataBoundItem is not AvailableVenueForPositionDto venue)
        {
            MessageBox.Show(
                "Выберите площадку",
                "Добавление позиции",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        var startDate = DateOnly.FromDateTime(dtpDateFrom.Value);
        var endDate = DateOnly.FromDateTime(dtpDateTo.Value);

        if (startDate > endDate)
        {
            MessageBox.Show(
                "Дата начала не может быть больше даты окончания",
                "Добавление позиции",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        SelectedPosition = new SelectedPositionDto(
            venue.TariffId,
            venue.VenueName,
            venue.Price,
            startDate,
            endDate);

        DialogResult = DialogResult.OK;
        Close();
    }

    private async void BtnApply_Click(object sender, EventArgs e)
    {
        await LoadVenuesAsync();
    }

    private async void BtnReset_Click(object sender, EventArgs e)
    {
        txtName.Clear();
        txtStreet.Clear();

        cbVenueTypes.SelectedIndex = -1;
        cbRegions.SelectedIndex = -1;
        cbDistricts.DataSource = null;
        cbCities.DataSource = null;

        nudRatingFrom.Value = 1;
        nudRatingTo.Value = 10;
        nudPriceFrom.Value = 0;
        nudPriceTo.Value = 0;

        dtpDateFrom.Value = DateTime.Today;
        dtpDateTo.Value = DateTime.Today;

        await LoadRegionsAsync();
        await LoadVenuesAsync();
    }

    private async void CbRegions_SelectedIndexChanged(object sender, EventArgs e)
    {
        await LoadDistrictsAsync();
    }

    private async void CbDistricts_SelectedIndexChanged(object sender, EventArgs e)
    {
        await LoadCitiesAsync();
    }

    private async void CbCities_SelectedIndexChanged(object sender, EventArgs e)
    {
        await LoadVenuesAsync();
    }

    private async void CbVenueTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        await LoadVenuesAsync();
    }

    private async void TxtName_TextChanged(object sender, EventArgs e)
    {
        await LoadVenuesAsync();
    }

    private async void TxtStreet_TextChanged(object sender, EventArgs e)
    {
        await LoadVenuesAsync();
    }

    private async void NudRatingFrom_ValueChanged(object sender, EventArgs e)
    {
        await LoadVenuesAsync();
    }

    private async void NudRatingTo_ValueChanged(object sender, EventArgs e)
    {
        await LoadVenuesAsync();
    }

    private async void NudPriceFrom_ValueChanged(object sender, EventArgs e)
    {
        await LoadVenuesAsync();
    }

    private async void NudPriceTo_ValueChanged(object sender, EventArgs e)
    {
        await LoadVenuesAsync();
    }

    private async void DtpDateFrom_ValueChanged(object sender, EventArgs e)
    {
        await LoadVenuesAsync();
    }

    private async void DtpDateTo_ValueChanged(object sender, EventArgs e)
    {
        await LoadVenuesAsync();
    }

    private Task LoadRegionsAsync()
    {
        // сюда вставь уже существующую у тебя загрузку регионов через GetDistinctQuery
        return Task.CompletedTask;
    }

    private Task LoadDistrictsAsync()
    {
        // сюда вставь уже существующую у тебя загрузку районов через GetDistinctQuery
        return Task.CompletedTask;
    }

    private Task LoadCitiesAsync()
    {
        // сюда вставь уже существующую у тебя загрузку городов через GetDistinctQuery
        return Task.CompletedTask;
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
}