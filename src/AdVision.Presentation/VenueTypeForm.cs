using AdVision.Application.VenueTypes.CreateVenueTypeCommand;
using AdVision.Contracts;
using AdVision.Domain.VenueTypes;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation
{
	public partial class VenueTypeForm : Form
	{
		private readonly INotificationService _notificationService;
		private readonly ICommandHandler<Guid, CreateVenueTypeCommand> _commandHandler;
		private readonly CancellationTokenSource _ct = new();
		private readonly ILogger<VenueTypeForm> _logger;
		private readonly Image _exception = Image.FromFile("..\\..\\..\\Resources\\exception.png");
		private readonly Image _success = Image.FromFile("..\\..\\..\\Resources\\success.png");

		public event Action<string>? VenueTypeCreated;

		public VenueTypeForm(
			INotificationService notificationService,
			ICommandHandler<Guid, CreateVenueTypeCommand> commandHandler,
			ILogger<VenueTypeForm> logger)
		{
			_notificationService = notificationService;
			_commandHandler = commandHandler;
			_logger = logger;

			InitializeComponent();

			btnSave.Enabled = false;
			pbValidation.Image = _exception;
		}

		private async void BtnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(txtName.Text))
				{
					_notificationService.ShowError("Ошибка валидации", "Введите название типа площадки");
					return;
				}

				var result =
					await _commandHandler.Handle(new CreateVenueTypeCommand(new CreateVenueTypeDto(txtName.Text)),
						_ct.Token);

				if (result.IsFailure)
				{
					var errors = string.Join(Environment.NewLine, result.Error.Select(x => x.Message).ToList());
					_logger.LogError("Ошибка сохранения типа площадки: {Error}", errors);
					_notificationService.ShowError("Ошибка сохранения названия типа площадки", $"Ошибка сохранения типа площадки: {errors}");
					return;
				}

				_logger.LogInformation("Добавлен новый тип площадки с Id: {Id}", result.Value);
				_notificationService.ShowSuccess("Данные успешно сохранены", $"Добавлен новый тип площадки с Id: {result.Value}");
				VenueTypeCreated?.Invoke(txtName.Text);
				Close();
			}
			catch (Exception ex)
			{
				_notificationService.ShowError("Неизвестная ошибка", $"Ошибка сохранения названия типа площадки: {ex.Message}");
				_logger.LogError(ex, "Ошибка сохранения названия типа площадки: {Message}", ex.Message);
			}
		}

		private void VenueTypeForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			_ct.Dispose();
		}

		private void TxtName_TextChanged(object sender, EventArgs e)
		{
			var success = !string.IsNullOrWhiteSpace(txtName.Text) && 
			              txtName.Text.Length is >= VenueTypeName.MIN_LENGTH and <= VenueTypeName.MAX_LENGTH;

			btnSave.Enabled = success;
			pbValidation.Image = success ? _success : _exception;
		}
	}
}