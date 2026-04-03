using AdVision.Application.VenueTypes.CreateVenueTypeCommand;
using AdVision.Contracts;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation
{
	public partial class VenueTypeForm : Form
	{
		private readonly ICommandHandler<Guid, CreateVenueTypeCommand> _commandHandler;
		private readonly CancellationTokenSource _ct = new();
		private readonly ILogger<VenueTypeForm> _logger;
		public event Action? VenueTypeCreated;
		
		public VenueTypeForm(ICommandHandler<Guid, CreateVenueTypeCommand> commandHandler, ILogger<VenueTypeForm> logger)
		{
			_commandHandler = commandHandler;
			_logger = logger;
			InitializeComponent();
		}

		private async void BtnSave_Click(object sender, EventArgs e)
		{
			try
			{
				errorProvider1.SetError(txtName,
					string.IsNullOrWhiteSpace(txtName.Text) ? "Введите название площадки" : "");

				var result = await _commandHandler.Handle(new CreateVenueTypeCommand(new CreateVenueTypeDto(txtName.Text)), _ct.Token);

				if (result.IsFailure)
				{
					var errors = string.Join(Environment.NewLine, result.Error.Select(x=>x.Message).ToList());
					_logger.LogError("Ошибка сохранения типа площадки: {Error}", errors);
					MessageBox.Show(errors, @"Ошибка загрузки типа площадки", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				Close();
				VenueTypeCreated?.Invoke();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка сохранения типа площадки: {Message}", ex.Message);
			}
		}

		private void VenueTypeForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			_ct.Dispose();
		}
	}
}