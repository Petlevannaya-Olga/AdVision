using System.Linq.Expressions;
using AdVision.Application.Venues.GetDistinctQuery;
using AdVision.Application.Venues.GetVenuesQuery;
using AdVision.Application.VenueTypes.GetAllVenueTypesQuery;
using AdVision.Contracts;
using AdVision.Domain.Venues;
using AdVision.Domain.VenueTypes;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.Extensions;

namespace AdVision.Presentation;

public partial class MainForm : Form
{
	private const int PageSize = 10;

	private const string LoadVenuesErrorTitle = "Ошибка загрузки площадок";
	private const string LoadVenueTypesErrorTitle = "Ошибка загрузки типов площадок";
	private const string LoadRegionsErrorTitle = "Ошибка загрузки регионов";
	private const string LoadDistrictsErrorTitle = "Ошибка загрузки районов";
	private const string LoadCitiesErrorTitle = "Ошибка загрузки городов";
	private const string UnknownErrorTitle = "Непредвиденная ошибка";
	private const string DefaultLoadErrorMessage = "Не удалось загрузить данные";

	private readonly CancellationTokenSource _cts = new();
	private readonly INotificationService _notificationService;
	private readonly IQueryHandler<IReadOnlyList<string>, GetDistinctQuery> _getDistinctQueryHandler;
	private readonly IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> _venueTypesQueryHandler;
	private readonly IQueryHandler<IReadOnlyList<VenueDto>, GetVenuesQuery> _venuesQueryHandler;
	private readonly IServiceProvider _serviceProvider;
	private readonly ILogger<MainForm> _logger;

	private int _page = 1;
	private bool _isLoading;

	public MainForm(
		INotificationService notificationService,
		IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> venueTypesQueryHandler,
		IQueryHandler<IReadOnlyList<string>, GetDistinctQuery> getDistinctQueryHandler,
		IQueryHandler<IReadOnlyList<VenueDto>, GetVenuesQuery> venuesQueryHandler,
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

		try
		{
			await LoadVenuesAsync();
			await LoadFiltersAsync();
		}
		finally
		{
			_isLoading = false;
			UseWaitCursor = false;
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

	private async Task LoadVenuesAsync()
	{
		var filter = BuildFilter();

		var result = await _venuesQueryHandler.Handle(
			new GetVenuesQuery(_page, PageSize, filter),
			_cts.Token);

		if (result.IsFailure)
		{
			ShowLoadError(LoadVenuesErrorTitle, result.Error);
			return;
		}

		venuesDataGridView.DataSource = new BindingSource
		{
			DataSource = result.Value
		};
	}

	private async Task LoadFiltersAsync()
	{
		await LoadVenueTypesAsync();
		await LoadRegionsAsync();
		await LoadDistrictsAsync();
		await LoadCitiesAsync();
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

	private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
	{
		if (!_cts.IsCancellationRequested)
		{
			_cts.Cancel();
		}

		_cts.Dispose();
	}

	private Expression<Func<Venue, bool>> BuildFilter()
	{
		Expression<Func<Venue, bool>> filter = v => true;

		var selectedRegion = cbRegions.SelectedItem as string;
		var selectedDistrict = cbDistricts.SelectedItem as string;
		var selectedCity = cbCities.SelectedItem as string;

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

		return filter;
	}

	private async void BtnApply_Click(object sender, EventArgs e)
	{
		await LoadVenuesAsync();
	}
}