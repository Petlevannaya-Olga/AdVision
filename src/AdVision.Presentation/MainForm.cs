using AdVision.Application.Venues.GetVenuesQuery;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdVision.Presentation;

public partial class MainForm : Form
{
	private readonly ILogger<MainForm> _logger;
	private readonly GetVenuesQueryHandler _queryHandler;
	private readonly IServiceProvider _serviceProvider;
	private int page = 1;
	private const int PAGE_SIZE = 10;

	public MainForm(GetVenuesQueryHandler queryHandler, IServiceProvider serviceProvider, ILogger<MainForm> logger)
	{
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
		try
		{
			base.OnLoad(e);
			var result = await _queryHandler.Handle(new GetVenuesQuery(page, PAGE_SIZE));

			if (result.IsFailure)
			{
				_logger.LogError("Ошибка загрузки площадок: {Errors}", result.Error);
				return;
			}

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

			var bindingSource = new BindingSource();
			bindingSource.DataSource = result.Value;
			venuesDataGridView.DataSource = bindingSource;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Ошибка загрузки площадок: {Message}", ex.Message);
		}
	}
}