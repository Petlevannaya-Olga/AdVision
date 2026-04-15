using AdVision.Application.Positions.CreatePositionCommand;
using AdVision.Contracts;
using AdVision.Domain.Positions;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation
{
    public partial class PositionForm : Form
    {
        private const string ValidationErrorTitle = "Ошибка валидации";
        private const string SaveErrorTitle = "Ошибка сохранения позиции";
        private const string UnknownErrorTitle = "Неизвестная ошибка";
        private const string SaveSuccessTitle = "Данные успешно сохранены";
        private const string DefaultSaveErrorMessage = "Не удалось сохранить позицию";

        private readonly INotificationService _notificationService;
        private readonly ICommandHandler<Guid, CreatePositionCommand> _commandHandler;
        private readonly CancellationTokenSource _ct = new();
        private readonly ILogger<PositionForm> _logger;

        private bool _isSaving;

        public event Action<string>? PositionCreated;

        public PositionForm(
            INotificationService notificationService,
            ICommandHandler<Guid, CreatePositionCommand> commandHandler,
            ILogger<PositionForm> logger)
        {
            _notificationService = notificationService;
            _commandHandler = commandHandler;
            _logger = logger;

            InitializeComponent();
            UpdateValidationState();
        }

        private async void BtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                await SavePositionAsync();
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Сохранение позиции отменено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Непредвиденная ошибка при сохранении позиции");
                _notificationService.ShowError(
                    UnknownErrorTitle,
                    $"{SaveErrorTitle}: {ex.Message}");
            }
        }

        private async Task SavePositionAsync()
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
                var command = new CreatePositionCommand(new CreatePositionDto(name));
                var result = await _commandHandler.Handle(command, _ct.Token);

                if (result.IsFailure)
                {
                    ShowSaveError(result.Error?.Select(x => x.Message));
                    return;
                }

                _logger.LogInformation("Добавлена новая позиция с Id: {Id}", result.Value);

                _notificationService.ShowSuccess(
                    SaveSuccessTitle,
                    $"Добавлена новая позиция с Id: {result.Value}");

                PositionCreated?.Invoke(name);
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
                $"Название должно быть от {PositionName.MIN_LENGTH} до {PositionName.MAX_LENGTH} символов");
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

            _logger.LogError("Ошибка сохранения позиции: {Error}", normalizedMessage);
            _notificationService.ShowError(SaveErrorTitle, normalizedMessage);
        }

        private string GetTrimmedName()
        {
            return txtName.Text?.Trim() ?? string.Empty;
        }

        private bool IsNameValid(string name)
        {
            return !string.IsNullOrWhiteSpace(name) &&
                   name.Length is >= PositionName.MIN_LENGTH and <= PositionName.MAX_LENGTH;
        }

        private void UpdateValidationState()
        {
            var isValid = IsNameValid(GetTrimmedName());

            btnCreate.Enabled = !_isSaving && isValid;
            pbValidation.Image = isValid
                ? Properties.Resources.success
                : Properties.Resources.exception;
        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            UpdateValidationState();
        }

        private void PositionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_ct.IsCancellationRequested)
            {
                _ct.Cancel();
            }

            _ct.Dispose();
        }


        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}