using AdVision.Application.Venues.GetDistinctQuery;
using AdVision.Application.Venues.GetVenuesQuery;
using AdVision.Application.VenueTypes.GetAllVenueTypesQuery;
using AdVision.Contracts;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation;

public partial class MainForm : Form
{
    private int page = 1;
    private const int PAGE_SIZE = 10;

    private readonly CancellationTokenSource _cts = new();
    private readonly INotificationService _notificationService;
    private readonly IQueryHandler<IReadOnlyList<string>, GetDistinctQuery> _getDistinctQueryHandler;
    private readonly IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> _venueTypesQueryHandler;
    private readonly GetVenuesQueryHandler _queryHandler;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MainForm> _logger;

    public MainForm(
        INotificationService notificationService,
        IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> venueTypesQueryHandler,
        IQueryHandler<IReadOnlyList<string>, GetDistinctQuery> getDistinctQueryHandler,
        GetVenuesQueryHandler queryHandler,
        IServiceProvider serviceProvider,
        ILogger<MainForm> logger)
    {
        _notificationService = notificationService;
        _getDistinctQueryHandler = getDistinctQueryHandler;
        _venueTypesQueryHandler = venueTypesQueryHandler;
        _logger = logger;
        _queryHandler = queryHandler;
        _serviceProvider = serviceProvider;
        InitializeComponent();
    }

    private void BtnCreate_Click(object sender, EventArgs e)
    {
        var form = _serviceProvider.GetRequiredService<VenueForm>();
        form.ShowDialog();
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        venuesDataGridView.AutoGenerateColumns = false;
        venuesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Name", // свойство модели
            HeaderText = @"Название", // отображаемое название
            Name = "colName"
        });

        venuesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Type",
            HeaderText = @"Тип",
            Name = "colType"
        });

        venuesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Region",
            HeaderText = @"Регион",
            Name = "colRegion"
        });

        venuesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "District",
            HeaderText = @"Район",
            Name = "colDistrict"
        });

        venuesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "City",
            HeaderText = @"Город",
            Name = "colCity"
        });

        venuesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Street",
            HeaderText = @"Улица",
            Name = "colStreet"
        });

        venuesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "House",
            HeaderText = @"Дом",
            Name = "colHouse"
        });

        venuesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Latitude",
            HeaderText = @"Широта",
            Name = "colLatitude"
        });

        venuesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Longitude",
            HeaderText = @"Долгота",
            Name = "colLongitude"
        });

        venuesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Width",
            HeaderText = @"Ширина",
            Name = "colWidth"
        });

        venuesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Height",
            HeaderText = @"Высота",
            Name = "colHeight"
        });

        venuesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Rating",
            HeaderText = @"Рейтинг",
            Name = "colRating"
        });

        try
        {
            var result = await _queryHandler.Handle(new GetVenuesQuery(page, PAGE_SIZE));

            if (result.IsFailure)
            {
                _notificationService.ShowError("Ошибка загрузки площадок", string.Join("", result.Error));
                _logger.LogError("Ошибка загрузки площадок: {Errors}", result.Error);
                return;
            }

            var bindingSource = new BindingSource();
            bindingSource.DataSource = result.Value;
            venuesDataGridView.DataSource = bindingSource;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка загрузки площадок: {Message}", ex.Message);
        }

        await LoadVenueTypes();
        await LoadRegions();
        await LoadDistricts();
        await LoadCities();
    }

    private async Task LoadRegions()
    {
        cbRegions.DataSource = null;

        var result = await _getDistinctQueryHandler.Handle(new GetDistinctQuery(v => v.Address.Region), _cts.Token);
        if (result.IsFailure)
        {
            _logger.LogError("Ошибка загрузки регионов: {Error}", result.Error);
            _notificationService.ShowError("Ошибка загрузки регионов", string.Join("", result.Error));
            return;
        }

        cbRegions.DataSource = result.Value;
        cbRegions.SelectedIndex = -1;
    }
    
    private async Task LoadDistricts()
    {
        cbDistricts.DataSource = null;

        var result = await _getDistinctQueryHandler.Handle(new GetDistinctQuery(v => v.Address.District), _cts.Token);
        if (result.IsFailure)
        {
            _logger.LogError("Ошибка загрузки районов: {Error}", result.Error);
            _notificationService.ShowError("Ошибка загрузки районов", string.Join("", result.Error));
            return;
        }

        cbDistricts.DataSource = result.Value;
        cbDistricts.SelectedIndex = -1;
    }
    
    private async Task LoadCities()
    {
        cbCities.DataSource = null;

        var result = await _getDistinctQueryHandler.Handle(new GetDistinctQuery(v => v.Address.City), _cts.Token);
        if (result.IsFailure)
        {
            _logger.LogError("Ошибка загрузки городов: {Error}", result.Error);
            _notificationService.ShowError("Ошибка загрузки городов", string.Join("", result.Error));
            return;
        }

        cbCities.DataSource = result.Value;
        cbCities.SelectedIndex = -1;
    }

    private async Task LoadVenueTypes(string? name = null)
    {
        cbVenueTypes.DataSource = null;

        var result = await _venueTypesQueryHandler.Handle(
            new GetAllVenueTypesQuery(),
            _cts.Token);

        if (result.IsFailure)
        {
            _logger.LogError("Не удалось загрузить типы площадок: {Error}", result.Error);
            _notificationService.ShowError("Ошибка загрузки типов площадок",
                string.Join(Environment.NewLine, result.Error));
            return;
        }

        var list = result.Value
            .OrderBy(x => x.Name)
            .ToList();

        cbVenueTypes.DataSource = list;
        cbVenueTypes.DisplayMember = "Name";
        cbVenueTypes.ValueMember = "Id";
        cbVenueTypes.SelectedIndex = -1;
    }

    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        _cts.Dispose();
    }
}