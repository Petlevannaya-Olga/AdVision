using AdVision.Application;
using AdVision.Application.Contracts.GetContractsQuery;
using AdVision.Application.Customers.GetAllCustomersQuery;
using AdVision.Application.Employees.GetAllEmployeesQuery;
using AdVision.Contracts;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation;

public partial class SelectContractForm : Form
{
    private readonly IQueryHandler<PagedResult<ContractDto>, GetContractsQuery> _contractsQueryHandler;
    private readonly IQueryHandler<IReadOnlyList<CustomerDto>, GetAllCustomersQuery> _customersQueryHandler;
    private readonly IQueryHandler<IReadOnlyList<EmployeeDto>, GetAllEmployeesQuery> _employeesQueryHandler;
    private readonly INotificationService _notificationService;
    private readonly ILogger<SelectContractForm> _logger;

    public ContractDto? SelectedContract { get; private set; }

    public SelectContractForm(
        IQueryHandler<PagedResult<ContractDto>, GetContractsQuery> contractsQueryHandler,
        IQueryHandler<IReadOnlyList<CustomerDto>, GetAllCustomersQuery> customersQueryHandler,
        IQueryHandler<IReadOnlyList<EmployeeDto>, GetAllEmployeesQuery> employeesQueryHandler,
        INotificationService notificationService,
        ILogger<SelectContractForm> logger)
    {
        _contractsQueryHandler = contractsQueryHandler;
        _customersQueryHandler = customersQueryHandler;
        _employeesQueryHandler = employeesQueryHandler;
        _notificationService = notificationService;
        _logger = logger;

        InitializeComponent();
        ConfigureContractsGrid();
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        await LoadFiltersAsync();
        await LoadContractsAsync();
    }

    private void ConfigureContractsGrid()
    {
        dgvContracts.AutoGenerateColumns = false;
        dgvContracts.Columns.Clear();
        dgvContracts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvContracts.MultiSelect = false;
        dgvContracts.ReadOnly = true;

        dgvContracts.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ContractDto.Number),
            HeaderText = @"Номер договора",
            Name = "colNumber"
        });

        dgvContracts.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ContractDto.EmployeeFullName),
            HeaderText = @"Исполнитель",
            Name = "colEmployee",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        dgvContracts.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ContractDto.CustomerFullName),
            HeaderText = @"Заказчик",
            Name = "colCustomer",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        dgvContracts.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ContractDto.StartDate),
            HeaderText = @"Дата начала",
            Name = "colStartDate"
        });

        dgvContracts.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ContractDto.EndDate),
            HeaderText = @"Дата окончания",
            Name = "colEndDate"
        });

        dgvContracts.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ContractDto.SignedDate),
            HeaderText = @"Дата подписания",
            Name = "colSignedDate"
        });

        dgvContracts.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ContractDto.Status),
            HeaderText = @"Статус",
            Name = "colStatus"
        });
    }

    private async Task LoadFiltersAsync()
    {
        var customersResult = await _customersQueryHandler.Handle(
            new GetAllCustomersQuery(),
            CancellationToken.None);

        if (customersResult.IsFailure)
        {
            ShowError("Ошибка загрузки заказчиков", customersResult.Error);
            return;
        }

        var customers = customersResult.Value
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ToList();

        cbCustomer.DataSource = customers;
        cbCustomer.DisplayMember = nameof(CustomerDto.FullName);
        cbCustomer.ValueMember = nameof(CustomerDto.Id);
        cbCustomer.SelectedIndex = -1;

        var employeesResult = await _employeesQueryHandler.Handle(
            new GetAllEmployeesQuery(),
            CancellationToken.None);

        if (employeesResult.IsFailure)
        {
            ShowError("Ошибка загрузки сотрудников", employeesResult.Error);
            return;
        }

        var employees = employeesResult.Value
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ToList();

        cbEmployee.DataSource = employees;
        cbEmployee.DisplayMember = nameof(EmployeeDto.FullName);
        cbEmployee.ValueMember = nameof(EmployeeDto.Id);
        cbEmployee.SelectedIndex = -1;
    }

    private async Task LoadContractsAsync()
    {
        var result = await _contractsQueryHandler.Handle(
            new GetContractsQuery(
                1,
                1000,
                string.IsNullOrWhiteSpace(txtNumber.Text) ? null : txtNumber.Text.Trim(),
                cbCustomer.SelectedValue is Guid customerId ? customerId : null,
                cbEmployee.SelectedValue is Guid employeeId ? employeeId : null,
                ContractStatusDto.Active,
                DateOnly.MinValue,
                DateOnly.MaxValue,
                DateOnly.MinValue,
                DateOnly.MaxValue,
                DateOnly.MinValue,
                DateOnly.MaxValue,
                null,
                false),
            CancellationToken.None);

        if (result.IsFailure)
        {
            ShowError("Ошибка загрузки договоров", result.Error);
            return;
        }

        dgvContracts.DataSource = new BindingSource
        {
            DataSource = result.Value.Items
        };

        btnSelect.Enabled = result.Value.Items.Count > 0;
    }

    private async void BtnApply_Click(object? sender, EventArgs e)
    {
        await LoadContractsAsync();
    }

    private async void BtnReset_Click(object? sender, EventArgs e)
    {
        txtNumber.Clear();
        cbCustomer.SelectedIndex = -1;
        cbEmployee.SelectedIndex = -1;

        await LoadContractsAsync();
    }

    private void BtnSelect_Click(object? sender, EventArgs e)
    {
        SelectCurrentContract();
    }

    private void DgvContracts_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        SelectCurrentContract();
    }

    private void SelectCurrentContract()
    {
        if (dgvContracts.CurrentRow?.DataBoundItem is not ContractDto contract)
        {
            _notificationService.ShowWarning("Выбор договора", "Выберите договор");
            return;
        }

        SelectedContract = contract;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void BtnClose_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void ShowError(string title, IEnumerable<object> errors)
    {
        var message = string.Join(Environment.NewLine, errors);
        _logger.LogError("{Title}: {Message}", title, message);
        _notificationService.ShowWarning(title, string.IsNullOrWhiteSpace(message) ? "Не удалось загрузить данные" : message);
    }
}