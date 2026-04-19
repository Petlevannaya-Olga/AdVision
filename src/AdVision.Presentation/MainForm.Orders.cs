using AdVision.Application.Customers.GetAllCustomersQuery;
using AdVision.Application.Employees.GetAllEmployeesQuery;
using AdVision.Application.Orders.GetOrdersQuery;
using AdVision.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace AdVision.Presentation;

public partial class MainForm
{
    private DateTime _orderStartDateFromDefault;
    private DateTime _orderStartDateToDefault;
    private DateTime _orderEndDateFromDefault;
    private DateTime _orderEndDateToDefault;

    #region Заказы

    private void ConfigureOrdersGrid()
    {
        dgvOrders.AutoGenerateColumns = false;
        dgvOrders.Columns.Clear();

        dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(OrderDto.Id),
            HeaderText = @"Номер заказа",
            Name = "colOrderId"
        });

        dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(OrderDto.ContractNumber),
            HeaderText = @"Договор",
            Name = "colContract"
        });

        dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(OrderDto.EmployeeName),
            HeaderText = @"Исполнитель",
            Name = "colEmployee",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(OrderDto.CustomerName),
            HeaderText = @"Заказчик",
            Name = "colCustomer",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(OrderDto.TotalAmount),
            HeaderText = @"Сумма",
            Name = "colTotalAmount"
        });

        dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(OrderDto.StartDate),
            HeaderText = @"Дата начала",
            Name = "colStartDate"
        });

        dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(OrderDto.EndDate),
            HeaderText = @"Дата окончания",
            Name = "colEndDate"
        });

        dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(OrderDto.Status),
            HeaderText = @"Статус",
            Name = "colStatus"
        });
    }

    private async Task LoadOrdersAsync()
    {
        /*var result = await _ordersQueryHandler.Handle(
            new GetOrdersQuery(
                _ordersPage,
                OrdersPageSize,
                string.IsNullOrWhiteSpace(txtOrderId.Text) ? null : txtOrderId.Text.Trim(),
                cbOrderCustomer.SelectedValue is Guid customerId ? customerId : null,
                cbOrderEmployee.SelectedValue is Guid employeeId ? employeeId : null,
                cbOrderStatuses.SelectedValue is OrderStatusDto status ? status : null,
                decimal.TryParse(txtOrderAmountFrom.Text.Trim(), out var amountFrom) ? amountFrom : null,
                decimal.TryParse(txtOrderAmountTo.Text.Trim(), out var amountTo) ? amountTo : null,
                DateOnly.FromDateTime(dtpOrderStartDateFrom.Value),
                DateOnly.FromDateTime(dtpOrderStartDateTo.Value),
                DateOnly.FromDateTime(dtpOrderEndDateFrom.Value),
                DateOnly.FromDateTime(dtpOrderEndDateTo.Value),
                cbOrderSort.SelectedItem?.ToString(),
                cbOrderDesc.Checked),
            _cts.Token);

        if (result.IsFailure)
        {
            ShowLoadError(LoadOrdersErrorTitle, result.Error);
            return;
        }

        var paged = result.Value;
        _ordersTotalCount = paged.TotalCount;

        dgvOrders.DataSource = new BindingSource
        {
            DataSource = paged.Items
        };

        UpdateOrdersPagingState();*/
    }

    private void UpdateOrdersPagingState()
    {
        ordersPagingUserControl.SetState(
            _ordersTotalCount,
            _ordersPage,
            OrdersTotalPages,
            _isLoading);

        ordersPagingUserControl.SetAddVisible(true);
        ordersPagingUserControl.SetAddEnabled(!_isLoading);
    }

    private async void GoToPreviousOrdersPage()
    {
        if (_isLoading || _ordersPage <= 1)
        {
            return;
        }

        await RunUiActionAsync(
            async () =>
            {
                _ordersPage--;
                await LoadOrdersAsync();
            },
            "Переход на предыдущую страницу заказов отменен",
            "Ошибка загрузки предыдущей страницы заказов");
    }

    private async void GoToNextOrdersPage()
    {
        if (_isLoading || _ordersPage >= OrdersTotalPages)
        {
            return;
        }

        await RunUiActionAsync(
            async () =>
            {
                _ordersPage++;
                await LoadOrdersAsync();
            },
            "Переход на следующую страницу заказов отменен",
            "Ошибка загрузки следующей страницы заказов");
    }

    private void AddOrder()
    {
        var form = _serviceProvider.GetRequiredService<OrderForm>();
        form.OrderCreated += OnOrderCreated;
        form.ShowDialog();
        form.OrderCreated -= OnOrderCreated;
    }

    private async void OnOrderCreated()
    {
        // await RunUiActionAsync(
        //     async () =>
        //     {
        //         await ResetOrdersFiltersAsync();
        //         _ordersPage = 1;
        //         await LoadOrdersAsync();
        //         UpdateOrdersResetButtonState();
        //     },
        //     "Обновление списка заказов отменено",
        //     "Ошибка обновления списка заказов");
    }

    /*private async Task ResetOrdersFiltersAsync()
    {
        txtOrderId.Clear();
        txtOrderAmountFrom.Clear();
        txtOrderAmountTo.Clear();

        cbOrderEmployee.SelectedIndex = -1;
        cbOrderCustomer.SelectedIndex = -1;
        cbOrderStatuses.SelectedIndex = -1;
        cbOrderSort.SelectedIndex = -1;
        cbOrderDesc.Checked = false;

        await InitializeOrdersDateFiltersFromDbAsync();
    }*/

    /*private bool HasActiveOrdersFilters()
    {
        return !string.IsNullOrWhiteSpace(txtOrderId.Text) ||
               !string.IsNullOrWhiteSpace(txtOrderAmountFrom.Text) ||
               !string.IsNullOrWhiteSpace(txtOrderAmountTo.Text) ||
               cbOrderEmployee.SelectedIndex >= 0 ||
               cbOrderCustomer.SelectedIndex >= 0 ||
               cbOrderStatuses.SelectedIndex >= 0 ||
               cbOrderSort.SelectedIndex >= 0 ||
               cbOrderDesc.Checked ||
               dtpOrderStartDateFrom.Value != _orderStartDateFromDefault ||
               dtpOrderStartDateTo.Value != _orderStartDateToDefault ||
               dtpOrderEndDateFrom.Value != _orderEndDateFromDefault ||
               dtpOrderEndDateTo.Value != _orderEndDateToDefault;
    }*/

    // private void UpdateOrdersResetButtonState()
    // {
    //     btnOrdersReset.Enabled = HasActiveOrdersFilters();
    // }

    private async void BtnOrdersApply_Click(object? sender, EventArgs e)
    {
        // await RunUiActionAsync(
        //     async () =>
        //     {
        //         _ordersPage = 1;
        //         await LoadOrdersAsync();
        //         UpdateOrdersResetButtonState();
        //     },
        //     "Применение фильтра заказов отменено",
        //     "Ошибка применения фильтра заказов");
    }

    private async void BtnOrdersReset_Click(object sender, EventArgs e)
    {
        
    }

    private async Task LoadOrdersFiltersAsync()
    {
        
    }

    private void LoadOrderStatuses()
    {
        
    }

    private void LoadOrderSorting()
    {
        
    }

    private static string MapOrderStatusToRu(OrderStatusDto status)
    {
        return status switch
        {
            OrderStatusDto.Planned => "Запланирован",
            OrderStatusDto.InProgress => "В работе",
            OrderStatusDto.Completed => "Завершен",
            OrderStatusDto.Cancelled => "Отменен",
            _ => status.ToString()
        };
    }

    private async Task InitializeOrdersDateFiltersFromDbAsync()
    {
        var result = await _orderRepository.GetFilterBoundsAsync(_cts.Token);

        if (result.IsFailure)
        {
            ShowLoadError(LoadOrdersErrorTitle, new[] { result.Error });
            return;
        }

        var bounds = result.Value;

        if (bounds is null)
        {
            var today = DateTime.Today;

            _orderStartDateFromDefault = today;
            _orderStartDateToDefault = today;
            _orderEndDateFromDefault = today;
            _orderEndDateToDefault = today;
        }
        else
        {
            _orderStartDateFromDefault = bounds.StartDateMin?.ToDateTime(TimeOnly.MinValue) ?? DateTime.Today;
            _orderStartDateToDefault = bounds.StartDateMax?.ToDateTime(TimeOnly.MinValue) ?? DateTime.Today;
            _orderEndDateFromDefault = bounds.EndDateMin?.ToDateTime(TimeOnly.MinValue) ?? DateTime.Today;
            _orderEndDateToDefault = bounds.EndDateMax?.ToDateTime(TimeOnly.MinValue) ?? DateTime.Today;
        }

        // dtpOrderStartDateFrom.Value = _orderStartDateFromDefault;
        // dtpOrderStartDateTo.Value = _orderStartDateToDefault;
        // dtpOrderEndDateFrom.Value = _orderEndDateFromDefault;
        // dtpOrderEndDateTo.Value = _orderEndDateToDefault;
    }

    #endregion
}