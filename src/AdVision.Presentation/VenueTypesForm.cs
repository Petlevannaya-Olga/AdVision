using AdVision.Application.VenueTypes.GetAllVenueTypesQuery;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdVision.Presentation
{
	public partial class VenueTypesForm : Form
	{
		private readonly GetAllVenueTypesQueryHandler _handler;
		private readonly ILogger<VenueTypesForm> _logger;
		private readonly CancellationTokenSource _ct = new();
		private readonly IServiceProvider _serviceProvider;

		public VenueTypesForm(
			GetAllVenueTypesQueryHandler queryHandler,
			IServiceProvider serviceProvider,
			ILogger<VenueTypesForm> logger)
		{
			_handler = queryHandler;
			_logger = logger;
			_serviceProvider = serviceProvider;
			InitializeComponent();
		}

		private void BtnCreate_Click(object sender, EventArgs e)
		{
			var form = _serviceProvider.GetRequiredService<VenueTypeForm>();
			form.VenueTypeCreated += ReloadVenueTypes;
			form.ShowDialog();
		}

		private async void ReloadVenueTypes()
		{
			try
			{
				lvVenueTypes.Items.Clear();
				var list = await _handler.Handle(new GetAllVenueTypesQuery(), _ct.Token);

				if (list.IsFailure)
				{
					_logger.LogError("Ошибка загрузки типов площадок: {Error}", list.Error);
					return;
				}

				foreach (var item in list.Value.OrderBy(x=>x.Name))
				{
					lvVenueTypes.Items.Add(item.Name);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка загрузки типов площадок: {Message}", ex.Message);
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ReloadVenueTypes();
		}

		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
			_ct.Cancel();
		}
	}
}