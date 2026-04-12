using AdVision.Application.VenueTypes.CreateVenueTypeCommand;
using AdVision.Contracts;
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
                    _notificationService.ShowError("Ошибка базы данных", $"Ошибка сохранения типа площадки: {errors}");
                    return;
                }

                _logger.LogInformation("Добавлен новый тип площадки с Id: {Id}", result.Value);
                _notificationService.ShowSuccess("Данные успешно сохранены", $"Добавлен новый тип площадки с Id: {result.Value}");
                VenueTypeCreated?.Invoke(txtName.Text);
                Close();
            }
            catch (Exception ex)
            {
                _notificationService.ShowError("Неизвестная ошибка",$"Ошибка сохранения названия типа площадки: {ex.Message}");
                _logger.LogError(ex, "Ошибка сохранения названия типа площадки: {Message}", ex.Message);
            }
        }

        private void VenueTypeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ct.Dispose();
        }
    }
}