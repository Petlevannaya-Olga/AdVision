using AdVision.Application.Customers.CreateCustomerCommand;
using AdVision.Contracts;
using AdVision.Domain;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation;

public partial class CustomerForm : Form
{
    private const string ValidationErrorTitle = "Ошибка валидации";
    private const string SaveErrorTitle = "Ошибка сохранения заказчика";
    private const string UnknownErrorTitle = "Неизвестная ошибка";
    private const string SaveSuccessTitle = "Данные успешно сохранены";
    private const string DefaultSaveErrorMessage = "Не удалось сохранить заказчика";

    private readonly INotificationService _notificationService;
    private readonly ICommandHandler<Guid, CreateCustomerCommand> _commandHandler;
    private readonly CancellationTokenSource _ct = new();
    private readonly ILogger<CustomerForm> _logger;

    private bool _isSaving;

    public event Action? CustomerCreated;

    public CustomerForm(
        INotificationService notificationService,
        ICommandHandler<Guid, CreateCustomerCommand> commandHandler,
        ILogger<CustomerForm> logger)
    {
        _notificationService = notificationService;
        _commandHandler = commandHandler;
        _logger = logger;

        InitializeComponent();
        SubscribeEvents();
        UpdateValidationState();
    }

    private void SubscribeEvents()
    {
        btnSave.Click += BtnSave_Click;
        btnClose.Click += BtnClose_Click;
        btnGenerate.Click += BtnGenerate_Click;

        txtLastName.TextChanged += AnyField_TextChanged;
        txtFirstName.TextChanged += AnyField_TextChanged;
        txtMiddleName.TextChanged += AnyField_TextChanged;
        txtPhone.TextChanged += AnyField_TextChanged;

        FormClosed += CustomerForm_FormClosed;
    }

    private async void BtnSave_Click(object? sender, EventArgs e)
    {
        try
        {
            await SaveCustomerAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Сохранение заказчика отменено");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Непредвиденная ошибка при сохранении заказчика");
            _notificationService.ShowError(
                UnknownErrorTitle,
                $"{SaveErrorTitle}: {ex.Message}");
        }
    }

    private void BtnClose_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void BtnGenerate_Click(object? sender, EventArgs e)
    {
        GenerateDemoData();
        UpdateValidationState();
    }

    private async Task SaveCustomerAsync()
    {
        if (_isSaving)
        {
            return;
        }

        var lastName = GetTrimmedLastName();
        var firstName = GetTrimmedFirstName();
        var middleName = GetTrimmedMiddleName();
        var phone = GetNormalizedPhone();

        if (!IsFormValid(lastName, firstName, middleName, phone))
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
            var command = new CreateCustomerCommand(
                new CreateCustomerDto(lastName, firstName, middleName, phone));

            var result = await _commandHandler.Handle(command, _ct.Token);

            if (result.IsFailure)
            {
                ShowSaveError(result.Error?.Select(x => x.Message));
                return;
            }

            _logger.LogInformation("Добавлен новый заказчик с Id: {Id}", result.Value);

            _notificationService.ShowSuccess(
                SaveSuccessTitle,
                $"Добавлен новый заказчик с Id: {result.Value}");

            CustomerCreated?.Invoke();
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
        var errors = new List<string>();

        var lastName = GetTrimmedLastName();
        var firstName = GetTrimmedFirstName();
        var middleName = GetTrimmedMiddleName();
        var phone = GetNormalizedPhone();

        if (!IsPersonNameValid(lastName))
        {
            errors.Add($"Фамилия должна быть от {PersonName.MIN_LENGTH} до {PersonName.MAX_LENGTH} символов");
        }

        if (!IsPersonNameValid(firstName))
        {
            errors.Add($"Имя должно быть от {PersonName.MIN_LENGTH} до {PersonName.MAX_LENGTH} символов");
        }

        if (!IsPersonNameValid(middleName))
        {
            errors.Add($"Отчество должно быть от {PersonName.MIN_LENGTH} до {PersonName.MAX_LENGTH} символов");
        }

        if (!IsPhoneValid(phone))
        {
            errors.Add($"Телефон должен содержать от {PhoneNumber.MIN_LENGTH} до {PhoneNumber.MAX_LENGTH} цифр");
        }

        _notificationService.ShowError(
            ValidationErrorTitle,
            string.Join(Environment.NewLine, errors));
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

        _logger.LogError("Ошибка сохранения заказчика: {Error}", normalizedMessage);
        _notificationService.ShowError(SaveErrorTitle, normalizedMessage);
    }

    private string GetTrimmedLastName()
    {
        return txtLastName.Text?.Trim() ?? string.Empty;
    }

    private string GetTrimmedFirstName()
    {
        return txtFirstName.Text?.Trim() ?? string.Empty;
    }

    private string GetTrimmedMiddleName()
    {
        return txtMiddleName.Text?.Trim() ?? string.Empty;
    }

    private string GetNormalizedPhone()
    {
        return txtPhone.Text?.Trim() ?? string.Empty;
    }

    private bool IsPersonNameValid(string value)
    {
        return !string.IsNullOrWhiteSpace(value) &&
               value.Length >= PersonName.MIN_LENGTH &&
               value.Length <= PersonName.MAX_LENGTH;
    }

    private bool IsPhoneValid(string value)
    {
        return !string.IsNullOrWhiteSpace(value) &&
               value.Length >= PhoneNumber.MIN_LENGTH &&
               value.Length <= PhoneNumber.MAX_LENGTH;
    }

    private bool IsFormValid(
        string lastName,
        string firstName,
        string middleName,
        string phone)
    {
        return IsPersonNameValid(lastName) &&
               IsPersonNameValid(firstName) &&
               IsPersonNameValid(middleName) &&
               IsPhoneValid(phone);
    }

    private void UpdateValidationState()
    {
        var lastNameValid = IsPersonNameValid(GetTrimmedLastName());
        var firstNameValid = IsPersonNameValid(GetTrimmedFirstName());
        var middleNameValid = IsPersonNameValid(GetTrimmedMiddleName());
        var phoneValid = IsPhoneValid(GetNormalizedPhone());

        btnSave.Enabled = !_isSaving &&
                          lastNameValid &&
                          firstNameValid &&
                          middleNameValid &&
                          phoneValid;

        pbLastNameValidation.Image = lastNameValid
            ? Properties.Resources.success
            : Properties.Resources.exception;

        pbFirstNameValidation.Image = firstNameValid
            ? Properties.Resources.success
            : Properties.Resources.exception;

        pbMiddleNameValidation.Image = middleNameValid
            ? Properties.Resources.success
            : Properties.Resources.exception;

        pbPhoneValidation.Image = phoneValid
            ? Properties.Resources.success
            : Properties.Resources.exception;
    }

    private void AnyField_TextChanged(object? sender, EventArgs e)
    {
        UpdateValidationState();
    }

    private void CustomerForm_FormClosed(object? sender, FormClosedEventArgs e)
    {
        if (!_ct.IsCancellationRequested)
        {
            _ct.Cancel();
        }

        _ct.Dispose();
    }

    private void GenerateDemoData()
    {
        string[] lastNames = ["Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов"];
        string[] firstNames = ["Иван", "Петр", "Алексей", "Дмитрий", "Николай"];
        string[] middleNames = ["Иванович", "Петрович", "Алексеевич", "Дмитриевич", "Николаевич"];

        var random = new Random();

        txtLastName.Text = lastNames[random.Next(lastNames.Length)];
        txtFirstName.Text = firstNames[random.Next(firstNames.Length)];
        txtMiddleName.Text = middleNames[random.Next(middleNames.Length)];

        var phoneDigits = $"9{random.Next(100000000, 999999999)}";
        txtPhone.Text = phoneDigits;
    }
}