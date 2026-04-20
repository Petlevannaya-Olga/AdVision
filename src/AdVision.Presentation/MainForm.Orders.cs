using AdVision.Application.Customers.GetAllCustomersQuery;
using AdVision.Application.Employees.GetAllEmployeesQuery;
using AdVision.Application.OrderItems.GetOrderItemsByIdQuery;
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
		dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		dgvOrders.MultiSelect = false;
		dgvOrders.ReadOnly = true;

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

	private void ConfigureOrderItemsGrid()
	{
		dgvOrderItems.AutoGenerateColumns = false;
		dgvOrderItems.Columns.Clear();
		dgvOrderItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		dgvOrderItems.MultiSelect = false;
		dgvOrderItems.ReadOnly = true;

		dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
		{
			DataPropertyName = nameof(OrderItemDto.VenueName),
			HeaderText = @"Площадка",
			Name = "colOrderItemVenue",
			AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
		});

		dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
		{
			DataPropertyName = nameof(OrderItemDto.Price),
			HeaderText = @"Цена",
			Name = "colOrderItemPrice"
		});

		dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
		{
			DataPropertyName = nameof(OrderItemDto.StartDate),
			HeaderText = @"Дата начала",
			Name = "colOrderItemStartDate"
		});

		dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
		{
			DataPropertyName = nameof(OrderItemDto.EndDate),
			HeaderText = @"Дата окончания",
			Name = "colOrderItemEndDate"
		});

		dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
		{
			DataPropertyName = nameof(OrderItemDto.Status),
			HeaderText = @"Статус",
			Name = "colOrderItemStatus"
		});
	}

	private async Task LoadOrdersAsync()
	{
		var result = await _ordersQueryHandler.Handle(
				new GetOrdersQuery(
					_ordersPage,
					OrdersPageSize,
					string.IsNullOrWhiteSpace(txtOrderContractNumber.Text)
						? null
						: txtOrderContractNumber.Text.Trim(),
					cbOrderCustomers.SelectedValue is Guid customerId ? customerId : null,
					cbOrderEmployees.SelectedValue is Guid employeeId ? employeeId : null,
					cbOrderStatuses.SelectedValue is OrderStatusDto status ? status : null,
					DateOnly.FromDateTime(dtpOrderStartDateFrom.Value),
					DateOnly.FromDateTime(dtpOrderStartDateTo.Value),
					DateOnly.FromDateTime(dtpOrderEndDateFrom.Value),
					DateOnly.FromDateTime(dtpOrderEndDateTo.Value),
					"Номер договора",
					false),
			_cts.Token);

		if (result.IsFailure)
		{
			ShowLoadError(LoadOrdersErrorTitle, result.Error);
			return;
		}

		var paged = result.Value;
		_ordersTotalCount = paged.TotalCount;

		_isLoading = true;
		try
		{
			dgvOrders.DataSource = new BindingSource
			{
				DataSource = paged.Items
			};
		}
		finally
		{
			_isLoading = false;
		}

		UpdateOrdersPagingState();
		await SelectFirstOrderIfAvailableAsync();
	}

	private async Task LoadOrderItemsAsync(Guid orderId)
	{
		var result = await _orderItemsQueryHandler.Handle(
			new GetOrderItemsByOrderIdQuery(orderId),
			_cts.Token);

		if (result.IsFailure)
		{
			ShowLoadError("Ошибка загрузки позиций заказа", result.Error);
			dgvOrderItems.DataSource = null;
			return;
		}

		dgvOrderItems.DataSource = new BindingSource
		{
			DataSource = result.Value
		};
	}

	private async Task SelectFirstOrderIfAvailableAsync()
	{
		if (dgvOrders.Rows.Count == 0)
		{
			dgvOrderItems.DataSource = null;
			return;
		}

		var firstRow = dgvOrders.Rows[0];

		firstRow.Selected = true;

		if (firstRow.Cells.Count > 0)
		{
			dgvOrders.CurrentCell = firstRow.Cells[0];
		}

		if (firstRow.DataBoundItem is OrderDto order)
		{
			await LoadOrderItemsAsync(order.Id);
		}
		else
		{
			dgvOrderItems.DataSource = null;
		}
	}

	private async void DgvOrders_SelectionChanged(object? sender, EventArgs e)
	{
		if (_isLoading)
		{
			return;
		}

		if (dgvOrders.CurrentRow?.DataBoundItem is not OrderDto order)
		{
			dgvOrderItems.DataSource = null;
			return;
		}

		await RunUiActionAsync(
			async () => await LoadOrderItemsAsync(order.Id),
			"Загрузка позиций заказа отменена",
			"Ошибка загрузки позиций заказа");
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
		await RunUiActionAsync(
			async () =>
			{
				_ordersPage = 1;
				await LoadOrdersAsync();
				await LoadOrderCustomersAsync();
				await LoadOrderEmployeesAsync();
			},
			"Обновление списка заказов отменено",
			"Ошибка обновления списка заказов");
	}

	private async void BtnOrdersApply_Click(object? sender, EventArgs e)
	{
		await RunUiActionAsync(
			async () =>
			{
				_ordersPage = 1;
				await LoadOrdersAsync();
			},
			"Применение фильтра заказов отменено",
			"Ошибка применения фильтра заказов");
	}

	private async void BtnOrdersReset_Click(object sender, EventArgs e)
	{
		await RunUiActionAsync(
			async () =>
			{
				_ordersPage = 1;
				await LoadOrdersAsync();
			},
			"Сброс фильтра заказов отменен",
			"Ошибка сброса фильтра заказов");
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
		var result = await _orderRepository.GetDateBoundsAsync(_cts.Token);

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

		dtpOrderStartDateFrom.Value = _orderStartDateFromDefault;
		dtpOrderStartDateTo.Value = _orderStartDateToDefault;
		dtpOrderEndDateFrom.Value = _orderEndDateFromDefault;
		dtpOrderEndDateTo.Value = _orderEndDateToDefault;
	}

	private void TxtOrderContractNumber_TextChanged(object sender, EventArgs e)
	{

	}

	private void CbOrderEmployees_SelectedIndexChanged(object sender, EventArgs e)
	{

	}

	private void CbOrderCustomers_SelectedIndexChanged(object sender, EventArgs e)
	{

	}

	private void DtpOrderStartDateFrom_ValueChanged(object sender, EventArgs e)
	{

	}

	private void DtpOrderStartDateTo_ValueChanged(object sender, EventArgs e)
	{

	}

	private void DtpOrderEndDateFrom_ValueChanged(object sender, EventArgs e)
	{

	}

	private void DtpOrderEndDateTo_ValueChanged(object sender, EventArgs e)
	{

	}

	private void CbOrderStatuses_SelectedIndexChanged(object sender, EventArgs e)
	{

	}

	private async void BtnOrderReset_Click(object sender, EventArgs e)
	{
		await RunUiActionAsync(
			async () =>
			{
				await ResetOrdersFiltersAsync();
				_ordersPage = 1;
				await LoadOrdersAsync();
				UpdateOrdersResetButtonState();
			},
			"Сброс фильтра заказов отменен",
			"Ошибка сброса фильтра заказов");
	}

	private async void BtnOrderApply_Click(object sender, EventArgs e)
	{
		await RunUiActionAsync(
			async () =>
			{
				_ordersPage = 1;
				await LoadOrdersAsync();
				UpdateOrdersResetButtonState();
			},
			"Применение фильтра заказов отменено",
			"Ошибка применения фильтра заказов");
	}
	
	private async Task LoadOrdersFiltersAsync()
	{
		await LoadOrderCustomersAsync();
		await LoadOrderEmployeesAsync();
		await LoadOrderStatusesAsync();
		await InitializeOrdersDateFiltersFromDbAsync();

		cbOrderCustomers.SelectedIndex = -1;
		cbOrderEmployees.SelectedIndex = -1;
		cbOrderStatuses.SelectedIndex = -1;
		cbOrderStatuses.SelectedIndex = -1;

		UpdateOrdersResetButtonState();
	}
	
	private async Task LoadOrderCustomersAsync()
	{
		cbOrderCustomers.DataSource = null;

		var result = await _orderRepository.GetDistinctCustomersAsync(_cts.Token);

		if (result.IsFailure)
		{
			ShowLoadError("Ошибка загрузки заказчиков", new[] { result.Error });
			return;
		}

		cbOrderCustomers.DataSource = result.Value;
		cbOrderCustomers.DisplayMember = nameof(CustomerDto.FullName);
		cbOrderCustomers.ValueMember = nameof(CustomerDto.Id);
		cbOrderCustomers.SelectedIndex = -1;
	}
	
	private async Task LoadOrderEmployeesAsync()
	{
		cbOrderEmployees.DataSource = null;

		var result = await _orderRepository.GetDistinctEmployeesAsync(_cts.Token);

		if (result.IsFailure)
		{
			ShowLoadError("Ошибка загрузки исполнителей", new[] { result.Error });
			return;
		}

		cbOrderEmployees.DataSource = result.Value;
		cbOrderEmployees.DisplayMember = nameof(EmployeeDto.FullName);
		cbOrderEmployees.ValueMember = nameof(EmployeeDto.Id);
		cbOrderEmployees.SelectedIndex = -1;
	}

	private async Task LoadOrderStatusesAsync()
	{
		cbOrderStatuses.DataSource = null;

		var result = await _orderRepository.GetDistinctStatusesAsync(_cts.Token);

		if (result.IsFailure)
		{
			ShowLoadError("Ошибка загрузки статусов заказов", new[] { result.Error });
			return;
		}

		var statuses = result.Value
			.Select(x => new
			{
				Value = x,
				Name = MapOrderStatusToRu(x)
			})
			.ToList();

		cbOrderStatuses.DataSource = statuses;
		cbOrderStatuses.DisplayMember = "Name";
		cbOrderStatuses.ValueMember = "Value";
		cbOrderStatuses.SelectedIndex = -1;
	}
	
	private void UpdateOrdersResetButtonState()
	{
		btnOrderReset.Enabled =
			cbOrderCustomers.SelectedIndex >= 0 ||
			cbOrderEmployees.SelectedIndex >= 0 ||
			cbOrderStatuses.SelectedIndex >= 0 ||
			cbOrderStatuses.SelectedIndex >= 0 ||
			dtpOrderStartDateFrom.Value != _orderStartDateFromDefault ||
			dtpOrderStartDateTo.Value != _orderStartDateToDefault ||
			dtpOrderEndDateFrom.Value != _orderEndDateFromDefault ||
			dtpOrderEndDateTo.Value != _orderEndDateToDefault;
	}
	
	private async Task ResetOrdersFiltersAsync()
	{
		cbOrderCustomers.SelectedIndex = -1;
		cbOrderEmployees.SelectedIndex = -1;
		cbOrderStatuses.SelectedIndex = -1;
		cbOrderStatuses.SelectedIndex = -1;

		await InitializeOrdersDateFiltersFromDbAsync();
	}
	
	#endregion
}