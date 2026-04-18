using AdVision.Application.Contracts.GetContractsQuery;
using AdVision.Application.Customers.GetAllCustomersQuery;
using AdVision.Application.Employees.GetAllEmployeesQuery;
using AdVision.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace AdVision.Presentation;

public partial class MainForm
{
    private DateTime _startDateFromDefault;
    private DateTime _startDateToDefault;
    private DateTime _endDateFromDefault;
    private DateTime _endDateToDefault;
    private DateTime _signedDateFromDefault;
    private DateTime _signedDateToDefault;
    
    #region Контракты

    private void ConfigureContractsGrid()
    {
        dgvContracts.AutoGenerateColumns = false;
        dgvContracts.Columns.Clear();

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

    private async Task LoadContractsAsync()
    {
        var result = await _contractsQueryHandler.Handle(
            new GetContractsQuery(
                _contractsPage,
                ContractsPageSize,
                string.IsNullOrWhiteSpace(txtContractNumber.Text) ? null : txtContractNumber.Text.Trim(),
                cbCustomer.SelectedValue is Guid customerId ? customerId : null,
                cbEmployee.SelectedValue is Guid employeeId ? employeeId : null,
                cbStatuses.SelectedValue is ContractStatusDto status ? status : null,
                DateOnly.FromDateTime(dtpStartDateFrom.Value),
                DateOnly.FromDateTime(dtpStartDateTo.Value),
                DateOnly.FromDateTime(dtpEndDateFrom.Value),
                DateOnly.FromDateTime(dtpEndDateTo.Value),
                DateOnly.FromDateTime(dtpSignedDateFrom.Value),
                DateOnly.FromDateTime(dtpSignedDateTo.Value),
                cbOrder.SelectedItem?.ToString(),
                cbDesc.Checked),
            _cts.Token);

        if (result.IsFailure)
        {
            ShowLoadError(LoadContractsErrorTitle, result.Error);
            return;
        }

        var paged = result.Value;
        _contractsTotalCount = paged.TotalCount;

        dgvContracts.DataSource = new BindingSource
        {
            DataSource = paged.Items
        };

        UpdateContractsPagingState();
    }

    private void UpdateContractsPagingState()
    {
        contractsPagingUserControl.SetState(
            _contractsTotalCount,
            _contractsPage,
            ContractsTotalPages,
            _isLoading);

        contractsPagingUserControl.SetAddVisible(true);
        contractsPagingUserControl.SetAddEnabled(!_isLoading);
    }

    private async void GoToPreviousContractsPage()
    {
        if (_isLoading || _contractsPage <= 1)
        {
            return;
        }

        await RunUiActionAsync(
            async () =>
            {
                _contractsPage--;
                await LoadContractsAsync();
            },
            "Переход на предыдущую страницу договоров отменен",
            "Ошибка загрузки предыдущей страницы договоров");
    }

    private async void GoToNextContractsPage()
    {
        if (_isLoading || _contractsPage >= ContractsTotalPages)
        {
            return;
        }

        await RunUiActionAsync(
            async () =>
            {
                _contractsPage++;
                await LoadContractsAsync();
            },
            "Переход на следующую страницу договоров отменен",
            "Ошибка загрузки следующей страницы договоров");
    }

    private void AddContract()
    {
        var form = _serviceProvider.GetRequiredService<ContractForm>();
        form.ContractCreated += OnContractCreated;
        form.ShowDialog();
        form.ContractCreated -= OnContractCreated;
    }

    private async void OnContractCreated()
    {
        await RunUiActionAsync(
            async () =>
            {
                await ResetContractsFiltersAsync();
                _contractsPage = 1;
                await LoadContractsAsync();
                UpdateContractsResetButtonState();
            },
            "Обновление списка договоров отменено",
            "Ошибка обновления списка договоров");
    }

    private async Task ResetContractsFiltersAsync()
    {
        txtContractNumber.Clear();
        cbEmployee.SelectedIndex = -1;
        cbCustomer.SelectedIndex = -1;
        cbStatuses.SelectedIndex = -1;
        cbOrder.SelectedIndex = -1;
        cbDesc.Checked = false;

        await InitializeContractsDateFiltersFromDbAsync();
    }

    private bool HasActiveContractsFilters()
    {
        return !string.IsNullOrWhiteSpace(txtContractNumber.Text) ||
               cbEmployee.SelectedIndex >= 0 ||
               cbCustomer.SelectedIndex >= 0 ||
               cbStatuses.SelectedIndex >= 0 ||
               cbOrder.SelectedIndex >= 0 ||
               cbDesc.Checked ||
               dtpStartDateFrom.Value != _startDateFromDefault ||
               dtpStartDateTo.Value != _startDateToDefault ||
               dtpEndDateFrom.Value != _endDateFromDefault ||
               dtpEndDateTo.Value != _endDateToDefault ||
               dtpSignedDateFrom.Value != _signedDateFromDefault ||
               dtpSignedDateTo.Value != _signedDateToDefault;
    }

    private void UpdateContractsResetButtonState()
    {
        btnContractsReset.Enabled = HasActiveContractsFilters();
    }

    private async void BtnContractsApply_Click(object? sender, EventArgs e)
    {
        await RunUiActionAsync(
            async () =>
            {
                _contractsPage = 1;
                await LoadContractsAsync();
                UpdateContractsResetButtonState();
            },
            "Применение фильтра договоров отменено",
            "Ошибка применения фильтра договоров");
    }

    private async void BtnContractsReset_Click(object sender, EventArgs e)
    {
        await RunUiActionAsync(
            async () =>
            {
                await ResetContractsFiltersAsync();
                _contractsPage = 1;
                await LoadContractsAsync();
                UpdateContractsResetButtonState();
            },
            "Сброс фильтров договоров отменен",
            "Ошибка сброса фильтров договоров");
    }

    private void ContractsFiltersChanged(object? sender, EventArgs e)
    {
        UpdateContractsResetButtonState();
    }

    private async Task LoadContractsFiltersAsync()
    {
        // --- сотрудники ---
        var employeesResult = await _employeesQueryHandler.Handle(
            new GetAllEmployeesQuery(),
            _cts.Token);

        if (employeesResult.IsFailure)
        {
            ShowLoadError("Ошибка загрузки сотрудников", employeesResult.Error);
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

        // --- заказчики ---
        var customersResult = await _customersQueryHandler.Handle(
            new GetAllCustomersQuery(),
            _cts.Token);

        if (customersResult.IsFailure)
        {
            ShowLoadError("Ошибка загрузки заказчиков", customersResult.Error);
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

        LoadContractStatuses();
        LoadContractSorting();
    }

    private void LoadContractStatuses()
    {
        var statuses = Enum.GetValues(typeof(ContractStatusDto))
            .Cast<ContractStatusDto>()
            .Select(s => new
            {
                Value = s,
                Name = MapStatusToRu(s)
            })
            .ToList();

        cbStatuses.DataSource = statuses;
        cbStatuses.DisplayMember = "Name";
        cbStatuses.ValueMember = "Value";
        cbStatuses.SelectedIndex = -1;
    }

    private void LoadContractSorting()
    {
        cbOrder.Items.Clear();

        cbOrder.Items.AddRange(new object[]
        {
            "Номер",
            "Дата начала",
            "Дата окончания",
            "Дата подписания",
            "Статус"
        });

        cbOrder.SelectedIndex = -1;
    }

    private static string MapStatusToRu(ContractStatusDto status)
    {
        return status switch
        {
            ContractStatusDto.Draft => "Черновик",
            ContractStatusDto.Active => "Активен",
            ContractStatusDto.Signed => "Подписан",
            ContractStatusDto.Completed => "Завершен",
            ContractStatusDto.Cancelled => "Отменен",
            _ => status.ToString()
        };
    }

    private async Task InitializeContractsDateFiltersFromDbAsync()
    {
        var result = await _contractRepository.GetDateBoundsAsync(_cts.Token);

        if (result.IsFailure)
        {
            ShowLoadError(LoadContractsErrorTitle, new[] { result.Error });
            return;
        }

        var bounds = result.Value;

        if (bounds is null)
        {
            var today = DateTime.Today;

            _startDateFromDefault = today;
            _startDateToDefault = today;
            _endDateFromDefault = today;
            _endDateToDefault = today;
            _signedDateFromDefault = today;
            _signedDateToDefault = today;
        }
        else
        {
            _startDateFromDefault = bounds.StartDateMin.ToDateTime(TimeOnly.MinValue);
            _startDateToDefault = bounds.StartDateMax.ToDateTime(TimeOnly.MinValue);

            _endDateFromDefault = bounds.EndDateMin.ToDateTime(TimeOnly.MinValue);
            _endDateToDefault = bounds.EndDateMax.ToDateTime(TimeOnly.MinValue);

            var signedFrom = bounds.SignedDateMin ?? bounds.StartDateMin;
            var signedTo = bounds.SignedDateMax ?? bounds.EndDateMax;

            _signedDateFromDefault = signedFrom.ToDateTime(TimeOnly.MinValue);
            _signedDateToDefault = signedTo.ToDateTime(TimeOnly.MinValue);
        }

        dtpStartDateFrom.Value = _startDateFromDefault;
        dtpStartDateTo.Value = _startDateToDefault;
        dtpEndDateFrom.Value = _endDateFromDefault;
        dtpEndDateTo.Value = _endDateToDefault;
        dtpSignedDateFrom.Value = _signedDateFromDefault;
        dtpSignedDateTo.Value = _signedDateToDefault;
    }

    private void TxtContractNumber_TextChanged(object sender, EventArgs e)
    {
        UpdateContractsResetButtonState();
    }

    private void CbEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateContractsResetButtonState();
    }

    private void CbCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateContractsResetButtonState();
    }

    private void DtpStartDateFrom_ValueChanged(object sender, EventArgs e)
    {
        UpdateContractsResetButtonState();
    }

    private void DtpStartDateTo_ValueChanged(object sender, EventArgs e)
    {
        UpdateContractsResetButtonState();
    }

    private void DtpEndDateFrom_ValueChanged(object sender, EventArgs e)
    {
        UpdateContractsResetButtonState();
    }

    private void DtpEndDateTo_ValueChanged(object sender, EventArgs e)
    {
        UpdateContractsResetButtonState();
    }

    private void DtpSignedDateFrom_ValueChanged(object sender, EventArgs e)
    {
        UpdateContractsResetButtonState();
    }

    private void DtpSignedDateTo_ValueChanged(object sender, EventArgs e)
    {
        UpdateContractsResetButtonState();
    }

    private void CbStatuses_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateContractsResetButtonState();
    }

    private void CbOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateContractsResetButtonState();
    }

    private void CbDesc_CheckedChanged(object sender, EventArgs e)
    {
        UpdateContractsResetButtonState();
    }

    #endregion
}