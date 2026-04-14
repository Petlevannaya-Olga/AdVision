using AdVision.Application.Tariffs.CreateTariffCommand;
using AdVision.Contracts;
using AdVision.Domain.Tariffs;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation
{
    public partial class NewTariffForm : Form
    {
        private const string ValidationErrorTitle = "Ошибка валидации";
        private const string CreateErrorTitle = "Ошибка создания тарифа";
        private const string UnknownErrorTitle = "Неизвестная ошибка";
        private const string SuccessTitle = "Успех";

        private Guid _venueId;
        private bool _isSaving;
        private readonly CancellationTokenSource _cts = new();

        private readonly ICommandHandler<TariffId, CreateTariffCommand> _createTariffCommandHandler;
        private readonly INotificationService _notificationService;
        private readonly ILogger<NewTariffForm> _logger;

        public event Action? TariffCreated;

        public NewTariffForm(
            ICommandHandler<TariffId, CreateTariffCommand> createTariffCommandHandler,
            INotificationService notificationService,
            ILogger<NewTariffForm> logger)
        {
            InitializeComponent();

            _createTariffCommandHandler = createTariffCommandHandler;
            _notificationService = notificationService;
            _logger = logger;

            UpdateSaveButtonState();
        }

        public void SetVenueId(Guid venueId)
        {
            _venueId = venueId;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                await SaveTariffAsync();
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Создание тарифа было отменено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания тарифа");
                _notificationService.ShowError(UnknownErrorTitle, ex.Message);
            }
        }

        private async Task SaveTariffAsync()
        {
            if (_isSaving)
            {
                return;
            }

            if (_venueId == Guid.Empty)
            {
                _notificationService.ShowError(
                    ValidationErrorTitle,
                    "Не выбрана площадка для создания тарифа");
                return;
            }

            if (!TryGetPrice(out var price))
            {
                _logger.LogWarning("Введено некорректное значение цены");
                _notificationService.ShowError(
                    ValidationErrorTitle,
                    "Некорректное значение цены");
                return;
            }

            if (!HasValidDateRange())
            {
                _notificationService.ShowError(
                    ValidationErrorTitle,
                    "Дата начала не может быть больше даты окончания");
                return;
            }

            _isSaving = true;
            UseWaitCursor = true;
            UpdateSaveButtonState();

            try
            {
                var dto = new CreateTariffDto(
                    _venueId,
                    DateOnly.FromDateTime(dtpStartDate.Value),
                    DateOnly.FromDateTime(dtpEndDate.Value),
                    price);

                var result = await _createTariffCommandHandler.Handle(
                    new CreateTariffCommand(dto),
                    _cts.Token);

                if (result.IsFailure)
                {
                    var errors = string.Join(Environment.NewLine, result.Error);
                    _notificationService.ShowError(CreateErrorTitle, errors);
                    return;
                }

                _notificationService.ShowSuccess(SuccessTitle, "Тариф успешно создан");

                TariffCreated?.Invoke();
                Close();
            }
            finally
            {
                _isSaving = false;
                UseWaitCursor = false;

                if (!IsDisposed)
                {
                    UpdateSaveButtonState();
                }
            }
        }

        private bool TryGetPrice(out double price)
        {
            return double.TryParse(txtPrice.Text.Trim(), out price);
        }

        private bool HasValidDateRange()
        {
            return DateOnly.FromDateTime(dtpStartDate.Value) <=
                   DateOnly.FromDateTime(dtpEndDate.Value);
        }

        private bool IsFormValid()
        {
            return TryGetPrice(out _) &&
                   HasValidDateRange();
        }

        private void UpdateSaveButtonState()
        {
            btnSave.Enabled = !_isSaving && IsFormValid();
        }

        private void TxtPrice_TextChanged(object sender, EventArgs e)
        {
            UpdateSaveButtonState();
        }

        private void DtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateSaveButtonState();
        }

        private void DtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateSaveButtonState();
        }

        private void NewTariffForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }

            _cts.Dispose();
        }
    }
}