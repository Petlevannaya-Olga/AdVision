using AdVision.Application.Discounts.CreateDiscountCommand;
using AdVision.Contracts;
using AdVision.Domain.Discounts;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation;

public partial class DiscountForm : Form
{
    private const string ValidationErrorTitle = "Ошибка валидации";
    private const string SaveErrorTitle = "Ошибка сохранения скидки";
    private const string UnknownErrorTitle = "Неизвестная ошибка";
    private const string SaveSuccessTitle = "Данные успешно сохранены";
    private const string DefaultSaveErrorMessage = "Не удалось сохранить скидку";

    private readonly INotificationService _notificationService;
    private readonly ICommandHandler<Guid, CreateDiscountCommand> _commandHandler;
    private readonly CancellationTokenSource _ct = new();
    private readonly ILogger<DiscountForm> _logger;

    private bool _isSaving;

    public event Action<string>? DiscountCreated;

    public DiscountForm(
        INotificationService notificationService,
        ICommandHandler<Guid, CreateDiscountCommand> commandHandler,
        ILogger<DiscountForm> logger)
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
            await SaveDiscountAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Сохранение скидки отменено");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Непредвиденная ошибка при сохранении скидки");
            _notificationService.ShowError(
                UnknownErrorTitle,
                $"{SaveErrorTitle}: {ex.Message}");
        }
    }

    private async Task SaveDiscountAsync()
    {
        if (_isSaving)
        {
            return;
        }

        var name = GetTrimmedName();
        var percent = nudPercent.Value;
        var minTotal = nudMinTotal.Value;

        var validationErrors = ValidateDiscount(name, percent, minTotal);

        if (validationErrors.Count > 0)
        {
            ShowValidationError(validationErrors);
            UpdateValidationState();
            return;
        }

        _isSaving = true;
        UseWaitCursor = true;
        UpdateValidationState();

        try
        {
            var command = new CreateDiscountCommand(
                new CreateDiscountDto(
                    name,
                    percent,
                    minTotal));

            var result = await _commandHandler.Handle(command, _ct.Token);

            if (result.IsFailure)
            {
                ShowSaveError(result.Error?.Select(x => x.Message));
                return;
            }

            _logger.LogInformation("Добавлена новая скидка с Id: {Id}", result.Value);

            _notificationService.ShowSuccess(
                SaveSuccessTitle,
                $"Добавлена новая скидка с Id: {result.Value}");

            DiscountCreated?.Invoke(name);
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

    private static List<string> ValidateDiscount(string name, decimal percent, decimal minTotal)
    {
        var errors = new List<string>();

        var nameError = ValidateName(name);
        if (!string.IsNullOrWhiteSpace(nameError))
        {
            errors.Add(nameError);
        }

        var percentError = ValidatePercent(percent);
        if (!string.IsNullOrWhiteSpace(percentError))
        {
            errors.Add(percentError);
        }

        var minTotalError = ValidateMinTotal(minTotal);
        if (!string.IsNullOrWhiteSpace(minTotalError))
        {
            errors.Add(minTotalError);
        }

        return errors;
    }

    private static string? ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return "Название скидки обязательно";
        }

        if (name.Length < DiscountName.MIN_LENGTH || name.Length > DiscountName.MAX_LENGTH)
        {
            return $"Название должно быть от {DiscountName.MIN_LENGTH} до {DiscountName.MAX_LENGTH} символов";
        }

        return null;
    }

    private static string? ValidatePercent(decimal percent)
    {
        if (percent < (decimal)DiscountPercent.MIN_VALUE || percent > (decimal)DiscountPercent.MAX_VALUE)
        {
            return $"Процент скидки должен быть от {DiscountPercent.MIN_VALUE} до {DiscountPercent.MAX_VALUE}";
        }

        return null;
    }

    private static string? ValidateMinTotal(decimal minTotal)
    {
        if (minTotal < DiscountMinTotal.MIN_VALUE)
        {
            return $"Минимальная сумма не может быть меньше {DiscountMinTotal.MIN_VALUE}";
        }

        return null;
    }

    private void ShowValidationError(IEnumerable<string> errors)
    {
        var message = string.Join(Environment.NewLine, errors);

        _notificationService.ShowError(
            ValidationErrorTitle,
            message);
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

        _logger.LogError("Ошибка сохранения скидки: {Error}", normalizedMessage);
        _notificationService.ShowError(SaveErrorTitle, normalizedMessage);
    }

    private string GetTrimmedName()
    {
        return txtName.Text?.Trim() ?? string.Empty;
    }

    private bool IsDiscountValid(string name, decimal percent, decimal minTotal)
    {
        return ValidateDiscount(name, percent, minTotal).Count == 0;
    }

    private void UpdateValidationState()
    {
        var name = GetTrimmedName();
        var percent = nudPercent.Value;
        var minTotal = nudMinTotal.Value;

        var nameError = ValidateName(name);
        var percentError = ValidatePercent(percent);
        var minTotalError = ValidateMinTotal(minTotal);

        UpdateValidationIcon(pbNameValidation, nameError);
        UpdateValidationIcon(pbPercentValidation, percentError);
        UpdateValidationIcon(pbMinTotalValidation, minTotalError);

        var isValid =
            string.IsNullOrWhiteSpace(nameError) &&
            string.IsNullOrWhiteSpace(percentError) &&
            string.IsNullOrWhiteSpace(minTotalError);

        btnSave.Enabled = !_isSaving && isValid;
    }

    private void UpdateValidationIcon(PictureBox pictureBox, string? error)
    {
        pictureBox.Image = string.IsNullOrWhiteSpace(error)
            ? Properties.Resources.success
            : Properties.Resources.exception;
    }

    private void TxtName_TextChanged(object sender, EventArgs e)
    {
        UpdateValidationState();
    }

    private void NudPercent_ValueChanged(object sender, EventArgs e)
    {
        UpdateValidationState();
    }

    private void NudMinTotal_ValueChanged(object sender, EventArgs e)
    {
        UpdateValidationState();
    }

    private void DiscountForm_FormClosed(object sender, FormClosedEventArgs e)
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