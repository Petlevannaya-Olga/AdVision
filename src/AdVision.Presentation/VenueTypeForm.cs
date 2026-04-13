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
        private const string ValidationErrorTitle = "Ошибка валидации";
        private const string SaveErrorTitle = "Ошибка сохранения типа площадки";
        private const string UnknownErrorTitle = "Неизвестная ошибка";
        private const string SaveSuccessTitle = "Данные успешно сохранены";
        private const string DefaultSaveErrorMessage = "Не удалось сохранить тип площадки";

        private readonly INotificationService _notificationService;
        private readonly ICommandHandler<Guid, CreateVenueTypeCommand> _commandHandler;
        private readonly CancellationTokenSource _ct = new();
        private readonly ILogger<VenueTypeForm> _logger;

        private bool _isSaving;

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
            UpdateValidationState();
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                await SaveVenueTypeAsync();
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Сохранение типа площадки отменено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Непредвиденная ошибка при сохранении типа площадки");
                _notificationService.ShowError(
                    UnknownErrorTitle,
                    $"{SaveErrorTitle}: {ex.Message}");
            }
        }

        private async Task SaveVenueTypeAsync()
        {
            if (_isSaving)
            {
                return;
            }

            var name = GetTrimmedName();
            if (!IsNameValid(name))
            {
                ShowValidationError();
                UpdateValidationState();
                return;
            }

            _isSaving = true;
            UseWaitCursor = true;
            UpdateValidationState();

            try
            {
                var command = new CreateVenueTypeCommand(new CreateVenueTypeDto(name));
                var result = await _commandHandler.Handle(command, _ct.Token);

                if (result.IsFailure)
                {
                    ShowSaveError(result.Error?.Select(x => x.Message));
                    return;
                }

                _logger.LogInformation("Добавлен новый тип площадки с Id: {Id}", result.Value);

                _notificationService.ShowSuccess(
                    SaveSuccessTitle,
                    $"Добавлен новый тип площадки с Id: {result.Value}");

                VenueTypeCreated?.Invoke(name);
                Close();
            }
            finally
            {
                _isSaving = false;
                UseWaitCursor = false;

                if (!IsDisposed)
                {
                    UpdateValidationState();
                }
            }
        }

        private void ShowValidationError()
        {
            _notificationService.ShowError(
                ValidationErrorTitle,
                $"Название должно быть от {VenueTypeName.MIN_LENGTH} до {VenueTypeName.MAX_LENGTH} символов");
        }

        private void ShowSaveError(IEnumerable<string>? errors)
        {
            var message = string.Join(Environment.NewLine, errors ?? Enumerable.Empty<string>());
            ShowSaveError(message);
        }

        private void ShowSaveError(string? message)
        {
            var normalizedMessage = string.IsNullOrWhiteSpace(message)
                ? DefaultSaveErrorMessage
                : message;

            _logger.LogError("Ошибка сохранения типа площадки: {Error}", normalizedMessage);
            _notificationService.ShowError(SaveErrorTitle, normalizedMessage);
        }

        private string GetTrimmedName()
        {
            return txtName.Text?.Trim() ?? string.Empty;
        }

        private bool IsNameValid(string name)
        {
            return !string.IsNullOrWhiteSpace(name) &&
                   name.Length >= VenueTypeName.MIN_LENGTH &&
                   name.Length <= VenueTypeName.MAX_LENGTH;
        }

        private void UpdateValidationState()
        {
            var isValid = IsNameValid(GetTrimmedName());

            btnSave.Enabled = !_isSaving && isValid;
            pbValidation.Image = isValid
                ? Properties.Resources.success
                : Properties.Resources.exception;
        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            UpdateValidationState();
        }

        private void VenueTypeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_ct.IsCancellationRequested)
            {
                _ct.Cancel();
            }

            _ct.Dispose();
        }
    }
}