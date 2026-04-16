using AdVision.Application.Employees.CreateEmployeeCommand;
using AdVision.Application.Generators.Employees;
using AdVision.Application.Positions.GetAllPositionsQuery;
using AdVision.Contracts;
using AdVision.Domain.Employees;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation;

public partial class EmployeeForm : Form
{
	private const string ValidationErrorTitle = "Ошибка валидации";
	private const string SaveErrorTitle = "Ошибка сохранения сотрудника";
	private const string LoadPositionsErrorTitle = "Ошибка загрузки должностей";
	private const string UnknownErrorTitle = "Неизвестная ошибка";
	private const string SaveSuccessTitle = "Данные успешно сохранены";
	private const string DefaultSaveErrorMessage = "Не удалось сохранить сотрудника";

	private readonly INotificationService _notificationService;
	private readonly ICommandHandler<Guid, CreateEmployeeCommand> _commandHandler;
	private readonly IQueryHandler<IReadOnlyList<PositionDto>, GetAllPositionsQuery> _positionsQueryHandler;
	private readonly IEmployeeFakeGenerator _employeeFakeGenerator;
	private readonly ILogger<EmployeeForm> _logger;
	private readonly IServiceProvider _serviceProvider;
	private readonly CancellationTokenSource _ct = new();

	private bool _isSaving;
	private bool _isLoadingPositions;

	public event Action? EmployeeCreated;

	public EmployeeForm(
		INotificationService notificationService,
		ICommandHandler<Guid, CreateEmployeeCommand> commandHandler,
		IQueryHandler<IReadOnlyList<PositionDto>, GetAllPositionsQuery> positionsQueryHandler,
		IEmployeeFakeGenerator employeeFakeGenerator,
		IServiceProvider serviceProvider,
		ILogger<EmployeeForm> logger)
	{
		_notificationService = notificationService;
		_commandHandler = commandHandler;
		_positionsQueryHandler = positionsQueryHandler;
		_employeeFakeGenerator = employeeFakeGenerator;
		_serviceProvider = serviceProvider;
		_logger = logger;

		InitializeComponent();
	}

	protected override async void OnLoad(EventArgs e)
	{
		base.OnLoad(e);

		try
		{
			await LoadPositionsAsync();
			UpdateValidationState();
		}
		catch (OperationCanceledException)
		{
			_logger.LogInformation("Загрузка формы сотрудника отменена");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Ошибка инициализации формы сотрудника");
			_notificationService.ShowError(UnknownErrorTitle, ex.Message);
		}
	}

	private async Task LoadPositionsAsync()
	{
		_isLoadingPositions = true;
		UpdateValidationState();

		try
		{
			cbPosition.DataSource = null;

			var result = await _positionsQueryHandler.Handle(
				new GetAllPositionsQuery(),
				_ct.Token);

			if (result.IsFailure)
			{
				var message = string.Join(Environment.NewLine, result.Error.Select(x => x.Message));

				_logger.LogError("Ошибка загрузки должностей: {Error}", message);
				_notificationService.ShowError(
					LoadPositionsErrorTitle,
					string.IsNullOrWhiteSpace(message) ? "Не удалось загрузить должности" : message);

				return;
			}

			var positions = result.Value
				.OrderBy(x => x.Name)
				.ToList();

			cbPosition.DataSource = positions;
			cbPosition.DisplayMember = nameof(PositionDto.Name);
			cbPosition.ValueMember = nameof(PositionDto.Id);
			cbPosition.SelectedIndex = -1;
		}
		finally
		{
			_isLoadingPositions = false;
			UpdateValidationState();
		}
	}

	private async void BtnSave_Click(object sender, EventArgs e)
	{
		try
		{
			await SaveEmployeeAsync();
		}
		catch (OperationCanceledException)
		{
			_logger.LogInformation("Сохранение сотрудника отменено");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Непредвиденная ошибка при сохранении сотрудника");
			_notificationService.ShowError(
				UnknownErrorTitle,
				$"{SaveErrorTitle}: {ex.Message}");
		}
	}

	private void BtnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private async void BtnAddPosition_Click(object sender, EventArgs e)
	{
		try
		{
			var form = _serviceProvider.GetRequiredService<PositionForm>();
			form.PositionCreated += OnPositionCreated;
			await form.ShowDialogAsync();
			form.PositionCreated -= OnPositionCreated;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Ошибка открытия формы должности");
			_notificationService.ShowError(UnknownErrorTitle, ex.Message);
		}
	}

	private async void OnPositionCreated(string _)
	{
		try
		{
			await LoadPositionsAsync();
		}
		catch (OperationCanceledException)
		{
			_logger.LogInformation("Обновление списка должностей отменено");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Ошибка обновления списка должностей");
			_notificationService.ShowError(UnknownErrorTitle, ex.Message);
		}
	}

	private async Task SaveEmployeeAsync()
	{
		if (_isSaving || _isLoadingPositions)
		{
			return;
		}

		var validationErrors = ValidateEmployee();

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
			var command = new CreateEmployeeCommand(new CreateEmployeeDto(
				GetTrimmedLastName(),
				GetTrimmedFirstName(),
				GetTrimmedMiddleName(),
				GetTrimmedAddress(),
				GetPassportSeries(),
				GetPassportNumber(),
				GetPhoneNumber(),
				GetSelectedPositionId()));

			var result = await _commandHandler.Handle(command, _ct.Token);

			if (result.IsFailure)
			{
				ShowSaveError(result.Error?.Select(x => x.Message));
				return;
			}

			_logger.LogInformation("Добавлен новый сотрудник с Id: {Id}", result.Value);

			_notificationService.ShowSuccess(
				SaveSuccessTitle,
				$"Добавлен новый сотрудник с Id: {result.Value}");

			EmployeeCreated?.Invoke();
			//Close();
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

	private List<string> ValidateEmployee()
	{
		var errors = new List<string>();

		AddErrorIfExists(errors, ValidateLastName());
		AddErrorIfExists(errors, ValidateFirstName());
		AddErrorIfExists(errors, ValidateMiddleName());
		AddErrorIfExists(errors, ValidateAddress());
		AddErrorIfExists(errors, ValidatePassportSeriesField());
		AddErrorIfExists(errors, ValidatePassportNumberField());
		AddErrorIfExists(errors, ValidatePhoneField());
		AddErrorIfExists(errors, ValidatePosition());

		return errors;
	}

	private static void AddErrorIfExists(List<string> errors, string? error)
	{
		if (!string.IsNullOrWhiteSpace(error))
		{
			errors.Add(error);
		}
	}

	private string? ValidateLastName()
	{
		var result = PersonName.Create(GetTrimmedLastName());
		return result.IsFailure ? result.Error.Message : null;
	}

	private string? ValidateFirstName()
	{
		var result = PersonName.Create(GetTrimmedFirstName());
		return result.IsFailure ? result.Error.Message : null;
	}

	private string? ValidateMiddleName()
	{
		var result = PersonName.Create(GetTrimmedMiddleName());
		return result.IsFailure ? result.Error.Message : null;
	}

	private string? ValidateAddress()
	{
		var result = EmployeeAddress.Create(GetTrimmedAddress());
		return result.IsFailure ? result.Error.Message : null;
	}

	private string? ValidatePassportSeriesField()
	{
		var result = PassportSeries.Create(GetPassportSeries());
		return result.IsFailure ? result.Error.Message : null;
	}

	private string? ValidatePassportNumberField()
	{
		var result = PassportNumber.Create(GetPassportNumber());
		return result.IsFailure ? result.Error.Message : null;
	}

	private string? ValidatePhoneField()
	{
		var result = PhoneNumber.Create(GetPhoneNumber());
		return result.IsFailure ? result.Error.Message : null;
	}

	private string? ValidatePosition()
	{
		if (cbPosition.SelectedItem is not PositionDto)
		{
			return "Необходимо выбрать должность";
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

		_logger.LogError("Ошибка сохранения сотрудника: {Error}", normalizedMessage);
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

	private string GetTrimmedAddress()
	{
		return txtAddress.Text?.Trim() ?? string.Empty;
	}

	private string GetPassportSeries()
	{
		return txtSeries.Text?.Trim() ?? string.Empty;
	}

	private string GetPassportNumber()
	{
		return txtNumber.Text?.Trim() ?? string.Empty;
	}

	private string GetPhoneNumber()
	{
		return txtPhone.Text?.Trim() ?? string.Empty;
	}

	private Guid GetSelectedPositionId()
	{
		return cbPosition.SelectedItem is PositionDto position
			? position.Id
			: Guid.Empty;
	}

	private void UpdateValidationState()
	{
		var lastNameError = ValidateLastName();
		var firstNameError = ValidateFirstName();
		var middleNameError = ValidateMiddleName();
		var addressError = ValidateAddress();
		var passportSeriesError = ValidatePassportSeriesField();
		var passportNumberError = ValidatePassportNumberField();
		var phoneError = ValidatePhoneField();
		var positionError = ValidatePosition();

		UpdateValidationIcon(pbLastNameValidation, lastNameError);
		UpdateValidationIcon(pbFirstNameValidation, firstNameError);
		UpdateValidationIcon(pbMiddleNameValidation, middleNameError);
		UpdateValidationIcon(pbPositionValidation, positionError);
		UpdateValidationIcon(pbSeriesValidation, passportSeriesError);
		UpdateValidationIcon(pbNumberValidation, passportNumberError);
		UpdateValidationIcon(pbAddressValidation, addressError);
		UpdateValidationIcon(pbPhoneValidation, phoneError);

		var isValid =
			string.IsNullOrWhiteSpace(lastNameError) &&
			string.IsNullOrWhiteSpace(firstNameError) &&
			string.IsNullOrWhiteSpace(middleNameError) &&
			string.IsNullOrWhiteSpace(addressError) &&
			string.IsNullOrWhiteSpace(passportSeriesError) &&
			string.IsNullOrWhiteSpace(passportNumberError) &&
			string.IsNullOrWhiteSpace(phoneError) &&
			string.IsNullOrWhiteSpace(positionError);

		btnSave.Enabled = !_isSaving && !_isLoadingPositions && isValid;
	}

	private void UpdateValidationIcon(PictureBox pictureBox, string? error)
	{
		pictureBox.Image = string.IsNullOrWhiteSpace(error)
			? Properties.Resources.success
			: Properties.Resources.exception;
	}

	private void TxtLastName_TextChanged(object sender, EventArgs e)
	{
		UpdateValidationState();
	}

	private void TxtFirstName_TextChanged(object sender, EventArgs e)
	{
		UpdateValidationState();
	}

	private void TxtMiddleName_TextChanged(object sender, EventArgs e)
	{
		UpdateValidationState();
	}

	private void TxtAddress_TextChanged(object sender, EventArgs e)
	{
		UpdateValidationState();
	}

	private void CbPosition_SelectedIndexChanged(object sender, EventArgs e)
	{
		UpdateValidationState();
	}

	private void TxtSeries_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
	{
		UpdateValidationState();
	}

	private void TxtNumber_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
	{
		UpdateValidationState();
	}

	private void TxtPhone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
	{
		UpdateValidationState();
	}

	private void TxtSeries_TextChanged(object sender, EventArgs e)
	{
		UpdateValidationState();
	}

	private void TxtNumber_TextChanged(object sender, EventArgs e)
	{
		UpdateValidationState();
	}

	private void TxtPhone_TextChanged(object sender, EventArgs e)
	{
		UpdateValidationState();
	}

	private void EmployeeForm_FormClosed(object sender, FormClosedEventArgs e)
	{
		if (!_ct.IsCancellationRequested)
		{
			_ct.Cancel();
		}

		_ct.Dispose();
	}

	private void BtnGenerate_Click(object sender, EventArgs e)
	{
		var employee = _employeeFakeGenerator.Generate();

		txtLastName.Text = employee.LastName.Value;
		txtFirstName.Text = employee.FirstName.Value;
		txtMiddleName.Text = employee.MiddleName.Value;

		txtSeries.Text = employee.Passport.Series.Value;
		txtNumber.Text = employee.Passport.Number.Value;

		txtAddress.Text = employee.Address.Value;

		txtPhone.Text = NormalizePhoneForMaskedTextBox(employee.PhoneNumber.Value);

		ValidateChildren();
	}
	
	private static string NormalizePhoneForMaskedTextBox(string phone)
	{
		var digits = new string(phone.Where(char.IsDigit).ToArray());

		if (digits.StartsWith('7') && digits.Length == 11 || digits.StartsWith('8') && digits.Length == 11)
			return digits[1..];

		return digits;
	}
}