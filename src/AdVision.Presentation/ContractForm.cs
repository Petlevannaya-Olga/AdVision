using AdVision.Application.Contracts.CreateContractCommand;
using AdVision.Application.Customers.GetAllCustomersQuery;
using AdVision.Application.Employees.GetAllEmployeesQuery;
using AdVision.Contracts;
using AdVision.Domain;
using AdVision.Domain.Contracts;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation;

public partial class ContractForm : Form
{
	private const string ValidationErrorTitle = "Ошибка валидации";
	private const string SaveErrorTitle = "Ошибка сохранения договора";
	private const string UnknownErrorTitle = "Неизвестная ошибка";
	private const string SaveSuccessTitle = "Данные успешно сохранены";
	private const string DefaultSaveErrorMessage = "Не удалось сохранить договор";
	private const string LoadCustomersErrorTitle = "Ошибка загрузки заказчиков";
	private const string LoadEmployeesErrorTitle = "Ошибка загрузки сотрудников";

	private readonly INotificationService _notificationService;
	private readonly ICommandHandler<Guid, CreateContractCommand> _commandHandler;
	private readonly IQueryHandler<IReadOnlyList<CustomerDto>, GetAllCustomersQuery> _customersQueryHandler;
	private readonly IQueryHandler<IReadOnlyList<EmployeeDto>, GetAllEmployeesQuery> _employeesQueryHandler;
	private readonly ILogger<ContractForm> _logger;
	private readonly CancellationTokenSource _ct = new();
	private static readonly Random _random = new();

	private bool _isSaving;

	public event Action? ContractCreated;

	public ContractForm(
		INotificationService notificationService,
		ICommandHandler<Guid, CreateContractCommand> commandHandler,
		IQueryHandler<IReadOnlyList<CustomerDto>, GetAllCustomersQuery> customersQueryHandler,
		IQueryHandler<IReadOnlyList<EmployeeDto>, GetAllEmployeesQuery> employeesQueryHandler,
		ILogger<ContractForm> logger)
	{
		_notificationService = notificationService;
		_commandHandler = commandHandler;
		_customersQueryHandler = customersQueryHandler;
		_employeesQueryHandler = employeesQueryHandler;
		_logger = logger;

		InitializeComponent();
		SubscribeEvents();
	}

	protected override async void OnLoad(EventArgs e)
	{
		base.OnLoad(e);

		try
		{
			await InitializeAsync();
		}
		catch (OperationCanceledException)
		{
			_logger.LogInformation("Инициализация формы договора отменена");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Ошибка инициализации формы договора");
			_notificationService.ShowError(UnknownErrorTitle, ex.Message);
		}
	}

	private void SubscribeEvents()
	{
		txtContractNumber.TextChanged += AnyFieldChanged;
		cbEmployees.SelectedIndexChanged += AnyFieldChanged;
		cbCustomers.SelectedIndexChanged += AnyFieldChanged;
		dtpStartDate.ValueChanged += AnyFieldChanged;
		dtpEndDate.ValueChanged += AnyFieldChanged;
		dtpSignedDate.ValueChanged += AnyFieldChanged;
		cbStatuses.SelectedIndexChanged += AnyFieldChanged;

		FormClosed += ContractForm_FormClosed;
	}

	private async Task InitializeAsync()
	{
		await LoadEmployeesAsync();
		await LoadCustomersAsync();
		LoadStatuses();
		UpdateValidationState();
	}

	private async Task LoadEmployeesAsync()
	{
		cbEmployees.DataSource = null;

		var result = await _employeesQueryHandler.Handle(
			new GetAllEmployeesQuery(),
			_ct.Token);

		if (result.IsFailure)
		{
			ShowLoadError(LoadEmployeesErrorTitle, result.Error);
			return;
		}

		var employees = result.Value
			.OrderBy(x => x.LastName)
			.ThenBy(x => x.FirstName)
			.ThenBy(x => x.MiddleName)
			.Select(x => new
			{
				x.Id,
				FullName = $"{x.LastName} {x.FirstName} {x.MiddleName}"
			})
			.ToList();

		cbEmployees.DataSource = employees;
		cbEmployees.DisplayMember = "FullName";
		cbEmployees.ValueMember = "Id";
		cbEmployees.SelectedIndex = -1;
	}

	private async Task LoadCustomersAsync()
	{
		cbCustomers.DataSource = null;

		var result = await _customersQueryHandler.Handle(
			new GetAllCustomersQuery(),
			_ct.Token);

		if (result.IsFailure)
		{
			ShowLoadError(LoadCustomersErrorTitle, result.Error);
			return;
		}

		var customers = result.Value
			.OrderBy(x => x.LastName)
			.ThenBy(x => x.FirstName)
			.ThenBy(x => x.MiddleName)
			.Select(x => new
			{
				x.Id,
				FullName = $"{x.LastName} {x.FirstName} {x.MiddleName}"
			})
			.ToList();

		cbCustomers.DataSource = customers;
		cbCustomers.DisplayMember = "FullName";
		cbCustomers.ValueMember = "Id";
		cbCustomers.SelectedIndex = -1;
	}

	private void LoadStatuses()
	{
		var statuses = Enum.GetValues(typeof(ContractStatusDto))
			.Cast<ContractStatusDto>()
			.Select(x => new
			{
				Value = x,
				Name = x.ToDisplay()
			})
			.ToList();

		cbStatuses.DataSource = statuses;
		cbStatuses.DisplayMember = "Name";
		cbStatuses.ValueMember = "Value";
		cbStatuses.SelectedIndex = -1;
	}

	private async void BtnSave_Click(object? sender, EventArgs e)
	{
		try
		{
			await SaveContractAsync();
		}
		catch (OperationCanceledException)
		{
			_logger.LogInformation("Сохранение договора отменено");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Непредвиденная ошибка при сохранении договора");
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

	private async Task SaveContractAsync()
	{
		if (_isSaving)
		{
			return;
		}

		var number = GetTrimmedNumber();

		if (!IsFormValid(number))
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
			var command = new CreateContractCommand(
				new CreateContractDto(
					number,
					GetSelectedCustomerId(),
					GetSelectedEmployeeId(),
					DateOnly.FromDateTime(dtpStartDate.Value),
					DateOnly.FromDateTime(dtpEndDate.Value),
					GetSelectedStatus(),
					DateOnly.FromDateTime(dtpSignedDate.Value)));

			var result = await _commandHandler.Handle(command, _ct.Token);

			if (result.IsFailure)
			{
				ShowSaveError(result.Error?.Select(x => x.Message));
				return;
			}

			_logger.LogInformation("Добавлен новый договор с Id: {Id}", result.Value);

			_notificationService.ShowSuccess(
				SaveSuccessTitle,
				$"Добавлен новый договор с Id: {result.Value}");

			ContractCreated?.Invoke();
			// Close();
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

		if (!IsContractNumberValid(GetTrimmedNumber()))
		{
			errors.Add($"Номер договора должен быть от {ContractNumber.MIN_LENGTH} до {ContractNumber.MAX_LENGTH} символов");
		}

		if (cbEmployees.SelectedItem is null)
		{
			errors.Add("Необходимо выбрать исполнителя");
		}

		if (cbCustomers.SelectedItem is null)
		{
			errors.Add("Необходимо выбрать заказчика");
		}

		if (!IsDateIntervalValid())
		{
			errors.Add("Дата начала не может быть больше даты окончания");
		}

		if (cbStatuses.SelectedItem is null)
		{
			errors.Add("Необходимо выбрать статус");
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

		_logger.LogError("Ошибка сохранения договора: {Error}", normalizedMessage);
		_notificationService.ShowError(SaveErrorTitle, normalizedMessage);
	}

	private void ShowLoadError(string title, IEnumerable<object>? errors)
	{
		var message = string.Join(Environment.NewLine, errors ?? []);
		_notificationService.ShowError(title, message);
	}

	private string GetTrimmedNumber()
	{
		return txtContractNumber.Text?.Trim() ?? string.Empty;
	}

	private Guid GetSelectedEmployeeId()
	{
		return cbEmployees.SelectedValue is Guid value ? value : Guid.Empty;
	}

	private Guid GetSelectedCustomerId()
	{
		return cbCustomers.SelectedValue is Guid value ? value : Guid.Empty;
	}

	private ContractStatusDto GetSelectedStatus()
	{
		return cbStatuses.SelectedValue is ContractStatusDto value
			? value
			: default;
	}

	private bool IsContractNumberValid(string number)
	{
		return !string.IsNullOrWhiteSpace(number) &&
			   number.Length >= ContractNumber.MIN_LENGTH &&
			   number.Length <= ContractNumber.MAX_LENGTH;
	}

	private bool IsDateIntervalValid()
	{
		var startDate = DateOnly.FromDateTime(dtpStartDate.Value);
		var endDate = DateOnly.FromDateTime(dtpEndDate.Value);

		return DateInterval.Create(startDate, endDate).IsSuccess;
	}

	private bool IsFormValid(string number)
	{
		return IsContractNumberValid(number) &&
			   cbEmployees.SelectedItem is not null &&
			   cbCustomers.SelectedItem is not null &&
			   cbStatuses.SelectedItem is not null &&
			   IsDateIntervalValid();
	}

	private void UpdateValidationState()
	{
		var numberValid = IsContractNumberValid(GetTrimmedNumber());
		var employeeValid = cbEmployees.SelectedItem is not null;
		var customerValid = cbCustomers.SelectedItem is not null;
		var startDateValid = IsDateIntervalValid();
		var endDateValid = IsDateIntervalValid();
		var signedDateValid = true;
		var statusValid = cbStatuses.SelectedItem is not null;

		btnSave.Enabled = !_isSaving &&
									numberValid &&
									employeeValid &&
									customerValid &&
									startDateValid &&
									endDateValid &&
									signedDateValid &&
									statusValid;

		pbContractNumberValidation.Image = numberValid
			? Properties.Resources.success
			: Properties.Resources.exception;

		pbEmployeeValidation.Image = employeeValid
			? Properties.Resources.success
			: Properties.Resources.exception;

		pbCustomerValidation.Image = customerValid
			? Properties.Resources.success
			: Properties.Resources.exception;

		pbStartDateValidation.Image = startDateValid
			? Properties.Resources.success
			: Properties.Resources.exception;

		pbEndDateValidation.Image = endDateValid
			? Properties.Resources.success
			: Properties.Resources.exception;

		pbSignedDateValidation.Image = signedDateValid
			? Properties.Resources.success
			: Properties.Resources.exception;

		pbStatusValidation.Image = statusValid
			? Properties.Resources.success
			: Properties.Resources.exception;
	}

	private void AnyFieldChanged(object? sender, EventArgs e)
	{
		UpdateValidationState();
	}

	private void ContractForm_FormClosed(object? sender, FormClosedEventArgs e)
	{
		if (!_ct.IsCancellationRequested)
		{
			_ct.Cancel();
		}

		_ct.Dispose();
	}

	private void GenerateDemoData()
	{
		// 📌 Номер договора (более живой формат)
		var year = DateTime.Now.Year;
		var number = _random.Next(1000, 9999);
		txtContractNumber.Text = $"DOG-{year}-{number}";

		// 👤 Случайный сотрудник
		if (cbEmployees.Items.Count > 0)
		{
			cbEmployees.SelectedIndex = _random.Next(0, cbEmployees.Items.Count);
		}

		// 👤 Случайный заказчик
		if (cbCustomers.Items.Count > 0)
		{
			cbCustomers.SelectedIndex = _random.Next(0, cbCustomers.Items.Count);
		}

		// 📅 Даты (логичные интервалы)
		var startDate = DateTime.Today.AddDays(_random.Next(-30, 30)); // +/- месяц
		var durationDays = _random.Next(7, 90); // от недели до 3 месяцев
		var endDate = startDate.AddDays(durationDays);

		dtpStartDate.Value = startDate;
		dtpEndDate.Value = endDate;

		// 📅 Дата подписания
		var signedDate = startDate.AddDays(_random.Next(0, 5));
		dtpSignedDate.Value = signedDate;

		// 📊 Статус (зависит от дат — реалистично)
		var status = GenerateStatus(startDate, endDate);
		cbStatuses.SelectedValue = status;

		UpdateValidationState();
	}

	private ContractStatusDto GenerateStatus(DateTime start, DateTime end)
	{
		var today = DateTime.Today;

		if (today < start)
		{
			return ContractStatusDto.Draft;
		}

		if (today >= start && today <= end)
		{
			return _random.Next(0, 2) == 0
				? ContractStatusDto.Active
				: ContractStatusDto.Signed;
		}

		// Уже закончился
		return _random.Next(0, 5) == 0
			? ContractStatusDto.Cancelled
			: ContractStatusDto.Completed;
	}
}