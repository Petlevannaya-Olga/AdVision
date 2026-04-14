using System.Linq.Expressions;
using AdVision.Application.Tariffs.GetTariffsByVenueIdQuery;
using AdVision.Contracts;
using AdVision.Domain.Tariffs;
using AdVision.Domain.Venues;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.Extensions;

namespace AdVision.Presentation
{
    public partial class TariffForm : Form
    {
        private VenueDto? _venueDto;
        private readonly CancellationTokenSource _cts = new();
        private readonly IQueryHandler<IReadOnlyList<TariffDto>, GetTariffsByVenueIdQuery> _getTariffsQueryHandler;
        private readonly ILogger<TariffForm> _logger;
        private readonly INotificationService _notificationService;
        private readonly IServiceProvider _serviceProvider;

        private DateOnly? _minTariffDate;
        private DateOnly? _maxTariffDate;

        public TariffForm(
            IQueryHandler<IReadOnlyList<TariffDto>, GetTariffsByVenueIdQuery> getTariffsQueryHandler,
            INotificationService notificationService,
            IServiceProvider serviceProvider,
            ILogger<TariffForm> logger)
        {
            InitializeComponent();

            _getTariffsQueryHandler = getTariffsQueryHandler;
            _notificationService = notificationService;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ConfigureGrid();
        }

        public async void LoadVenue(VenueDto venue)
        {
            try
            {
                _venueDto = venue;
                FillVenueInfo(venue);

                await LoadTariffBoundsAsync();
                ResetFilters();
                await LoadTariffsAsync();
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Загрузка данных площадки {VenueId} была отменена", venue.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка загрузки данных площадки {VenueId}", venue.Id);
                _notificationService.ShowError("Ошибка загрузки данных площадки", ex.Message);
            }
        }

        private void FillVenueInfo(VenueDto venue)
        {
            txtName.Text = venue.Name;
            txtType.Text = venue.Type;
            txtRating.Text = venue.Rating.ToString("N0");
            txtDistrict.Text = venue.District;
            txtRegion.Text = venue.Region;
            txtCity.Text = venue.City;
            txtStreet.Text = venue.Street;
            txtHouseNumber.Text = venue.House;
            txtHeight.Text = venue.Height.ToString("N2");
            txtWidth.Text = venue.Width.ToString("N2");
            txtLatitude.Text = venue.Latitude.ToString("N10");
            txtLongitude.Text = venue.Longitude.ToString("N10");
            txtDescription.Text = venue.Description;
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (_venueDto is null)
            {
                _notificationService.ShowError("Ошибка", "Площадка не выбрана");
                return;
            }

            var form = _serviceProvider.GetRequiredService<NewTariffForm>();
            form.SetVenueId(_venueDto.Id);
            form.TariffCreated += OnTariffCreated;
            form.ShowDialog();
            form.TariffCreated -= OnTariffCreated;
        }

        private async void OnTariffCreated()
        {
            try
            {
                await LoadTariffBoundsAsync();
                ResetFilters();
                await LoadTariffsAsync();
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Обновление списка тарифов было отменено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении списка тарифов");
                _notificationService.ShowError("Ошибка обновления тарифов", ex.Message);
            }
        }

        private async Task LoadTariffBoundsAsync()
        {
            if (_venueDto is null)
            {
                return;
            }

            var result = await _getTariffsQueryHandler.Handle(
                new GetTariffsByVenueIdQuery(new VenueId(_venueDto.Id), null),
                _cts.Token);

            if (result.IsFailure)
            {
                var errors = string.Join(Environment.NewLine, result.Error);
                _logger.LogError(
                    "Не удалось загрузить диапазон дат тарифов для площадки {VenueId}: {Errors}",
                    _venueDto.Id,
                    errors);

                _notificationService.ShowError("Ошибка загрузки тарифов", errors);
                return;
            }

            if (result.Value.Count == 0)
            {
                _minTariffDate = DateOnly.FromDateTime(DateTime.Today);
                _maxTariffDate = DateOnly.FromDateTime(DateTime.Today);
                return;
            }

            _minTariffDate = result.Value.Min(t => t.StartDate);
            _maxTariffDate = result.Value.Max(t => t.EndDate);
        }

        private async Task LoadTariffsAsync()
        {
            if (_venueDto is null)
            {
                _notificationService.ShowError("Ошибка загрузки тарифов", "Площадка не выбрана");
                return;
            }

            try
            {
                var filter = BuildFilter();

                var result = await _getTariffsQueryHandler.Handle(
                    new GetTariffsByVenueIdQuery(
                        new VenueId(_venueDto.Id),
                        filter),
                    _cts.Token);

                if (result.IsFailure)
                {
                    var errors = string.Join(Environment.NewLine, result.Error);
                    _logger.LogError(
                        "Не удалось загрузить тарифы для площадки {VenueId}: {Errors}",
                        _venueDto.Id,
                        errors);

                    _notificationService.ShowError("Ошибка загрузки тарифов", errors);
                    return;
                }

                dgvTariffs.DataSource = new BindingSource
                {
                    DataSource = result.Value
                };
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation(
                    "Загрузка тарифов для площадки {VenueId} была отменена",
                    _venueDto.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Ошибка загрузки тарифов для площадки {VenueId}",
                    _venueDto.Id);

                _notificationService.ShowError("Ошибка загрузки тарифов", ex.Message);
            }
        }

        private void ConfigureGrid()
        {
            dgvTariffs.AutoGenerateColumns = false;
            dgvTariffs.Columns.Clear();

            dgvTariffs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTariffs.MultiSelect = false;
            dgvTariffs.ReadOnly = true;
            dgvTariffs.AllowUserToAddRows = false;
            dgvTariffs.AllowUserToDeleteRows = false;
            dgvTariffs.RowHeadersVisible = false;
            dgvTariffs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvTariffs.Columns.Add(CreateTextColumn(
                nameof(TariffDto.StartDate),
                "Дата начала",
                "colStartDate",
                "dd.MM.yyyy",
                DataGridViewContentAlignment.MiddleCenter));

            dgvTariffs.Columns.Add(CreateTextColumn(
                nameof(TariffDto.EndDate),
                "Дата окончания",
                "colEndDate",
                "dd.MM.yyyy",
                DataGridViewContentAlignment.MiddleCenter));

            dgvTariffs.Columns.Add(CreateTextColumn(
                nameof(TariffDto.Price),
                "Стоимость",
                "colPrice",
                "N2",
                DataGridViewContentAlignment.MiddleRight));
        }

        private static DataGridViewTextBoxColumn CreateTextColumn(
            string dataPropertyName,
            string headerText,
            string columnName,
            string format,
            DataGridViewContentAlignment alignment)
        {
            return new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataPropertyName,
                HeaderText = headerText,
                Name = columnName,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = format,
                    Alignment = alignment
                }
            };
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TariffForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }

            _cts.Dispose();
        }

        private Expression<Func<Tariff, bool>> BuildFilter()
        {
            Expression<Func<Tariff, bool>> filter = x => true;

            var dateFrom = DateOnly.FromDateTime(dtpDateFrom.Value);
            var dateTo = DateOnly.FromDateTime(dtpDateTo.Value);

            var hasPriceFrom = double.TryParse(txtPriceFrom.Text.Trim(), out var priceFrom);
            var hasPriceTo = double.TryParse(txtPriceTo.Text.Trim(), out var priceTo);

            if (dateFrom <= dateTo)
            {
                filter = filter.And(x =>
                    x.Interval.StartDate <= dateTo &&
                    x.Interval.EndDate >= dateFrom);
            }

            if (hasPriceFrom)
            {
                filter = filter.And(x => x.Price >= priceFrom);
            }

            if (hasPriceTo)
            {
                filter = filter.And(x => x.Price <= priceTo);
            }

            return filter;
        }

        private async void BtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFilterInputs())
                {
                    return;
                }

                await LoadTariffsAsync();
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Применение фильтров было отменено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка применения фильтров тарифов");
                _notificationService.ShowError("Ошибка применения фильтров", ex.Message);
            }
        }

        private async void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetFilters();
                await LoadTariffsAsync();
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Сброс фильтров был отменен");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка сброса фильтров тарифов");
                _notificationService.ShowError("Ошибка сброса фильтров", ex.Message);
            }
        }

        private void ResetFilters()
        {
            var minDate = _minTariffDate ?? DateOnly.FromDateTime(DateTime.Today);
            var maxDate = _maxTariffDate ?? DateOnly.FromDateTime(DateTime.Today);

            dtpDateFrom.Value = minDate.ToDateTime(TimeOnly.MinValue);
            dtpDateTo.Value = maxDate.ToDateTime(TimeOnly.MinValue);

            txtPriceFrom.Clear();
            txtPriceTo.Clear();
        }

        private bool ValidateFilterInputs()
        {
            var dateFrom = DateOnly.FromDateTime(dtpDateFrom.Value);
            var dateTo = DateOnly.FromDateTime(dtpDateTo.Value);

            if (dateFrom > dateTo)
            {
                _notificationService.ShowError(
                    "Ошибка валидации",
                    "Дата \"с\" не может быть больше даты \"по\"");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtPriceFrom.Text) &&
                !double.TryParse(txtPriceFrom.Text.Trim(), out _))
            {
                _notificationService.ShowError(
                    "Ошибка валидации",
                    "Некорректное значение поля \"от\"");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtPriceTo.Text) &&
                !double.TryParse(txtPriceTo.Text.Trim(), out _))
            {
                _notificationService.ShowError(
                    "Ошибка валидации",
                    "Некорректное значение поля \"до\"");
                return false;
            }

            if (double.TryParse(txtPriceFrom.Text.Trim(), out var priceFrom) &&
                double.TryParse(txtPriceTo.Text.Trim(), out var priceTo) &&
                priceFrom > priceTo)
            {
                _notificationService.ShowError(
                    "Ошибка валидации",
                    "Значение \"от\" не может быть больше значения \"до\"");
                return false;
            }

            return true;
        }
    }
}