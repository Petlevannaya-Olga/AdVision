using System.ComponentModel;
using AdVision.Application.Orders.CreateOrderCommand;
using AdVision.Application.Repositories;
using AdVision.Contracts;
using AdVision.Domain.Customers;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation;

public partial class OrderForm : Form
{
    private const int PageSize = 10;

    private readonly ICustomerDiscountRepository _customerDiscountRepository;
    private readonly ICommandHandler<Guid, CreateOrderCommand> _createOrderCommandHandler;
    private readonly INotificationService _notificationService;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OrderForm> _logger;

    private readonly BindingList<CreateOrderItemRow> _items = [];
    private readonly BindingSource _pagedItemsBindingSource = new();

    private Guid? _selectedContractId;
    private Guid? _selectedCustomerId;
    private decimal _maxDiscountPercent;

    private bool _isLoading;
    private int _page = 1;

    private int TotalPages => _items.Count == 0
        ? 0
        : (int)Math.Ceiling((double)_items.Count / PageSize);

    public event Action? OrderCreated;

    public OrderForm(
        ICustomerDiscountRepository customerDiscountRepository,
        ICommandHandler<Guid, CreateOrderCommand> createOrderCommandHandler,
        INotificationService notificationService,
        IServiceProvider serviceProvider,
        ILogger<OrderForm> logger)
    {
        _customerDiscountRepository = customerDiscountRepository;
        _createOrderCommandHandler = createOrderCommandHandler;
        _notificationService = notificationService;
        _serviceProvider = serviceProvider;
        _logger = logger;

        InitializeComponent();

        
    }

    private void SubscribeControls()
    {
        orderItemsPagingUserControl.AddClicked += AddPosition;
        orderItemsPagingUserControl.PrevClicked += GoToPreviousOrderItemsPage;
        orderItemsPagingUserControl.NextClicked += GoToNextOrderItemsPage;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        
        btnSave.Enabled = false;
        pbContractValidation.Image = Properties.Resources.exception;

        ConfigureOrderItemsGrid();
        SubscribeControls();
        RefreshOrderItemsGrid();
        UpdateOrderTotal();
        UpdateOrderItemsPagingState();
    }

    private void ConfigureOrderItemsGrid()
    {
        dgvOrderItems.AutoGenerateColumns = false;
        dgvOrderItems.Columns.Clear();
        dgvOrderItems.ReadOnly = true;
        dgvOrderItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvOrderItems.MultiSelect = false;

        dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(CreateOrderItemRow.VenueName),
            HeaderText = @"Площадка",
            Name = "colVenue",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(CreateOrderItemRow.Price),
            HeaderText = @"Цена",
            Name = "colPrice"
        });

        dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(CreateOrderItemRow.StartDate),
            HeaderText = @"Дата начала",
            Name = "colStartDate"
        });

        dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(CreateOrderItemRow.EndDate),
            HeaderText = @"Дата окончания",
            Name = "colEndDate"
        });

        dgvOrderItems.DataSource = _pagedItemsBindingSource;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void BtnSelect_Click(object sender, EventArgs e)
    {
        SelectContract();
    }

    private void SelectContract()
    {
        var form = _serviceProvider.GetRequiredService<SelectContractForm>();

        if (form.ShowDialog() != DialogResult.OK || form.SelectedContract is null)
        {
            return;
        }

        var contract = form.SelectedContract;

        txtContractNumber.Text = contract.Number;
        pbContractValidation.Image = Properties.Resources.success;
        _selectedContractId = contract.Id;
        _selectedCustomerId = contract.CustomerId;

        UpdateSaveButtonState();
        _ = RefreshDiscountAsync();
    }

    private async Task RefreshDiscountAsync()
    {
        if (_selectedCustomerId is null)
        {
            _maxDiscountPercent = 0;
            UpdateOrderTotal();
            return;
        }

        var totalWithoutDiscount = CalculateTotalWithoutDiscount();

        var result = await _customerDiscountRepository.GetByCustomerIdAsync(
            new CustomerId(_selectedCustomerId.Value),
            CancellationToken.None);

        if (result.IsFailure)
        {
            _maxDiscountPercent = 0;
            UpdateOrderTotal();
            return;
        }

        _maxDiscountPercent = result.Value
            .Where(x => totalWithoutDiscount >= x.Discount.MinTotal.Value)
            .Select(x => x.Discount.Percent.Value)
            .DefaultIfEmpty(0)
            .Max();

        UpdateOrderTotal();
    }

    private void AddPosition()
    {
        var form = _serviceProvider.GetRequiredService<OrderItemForm>();

        if (form.ShowDialog() != DialogResult.OK || form.SelectedPosition is null)
        {
            return;
        }

        var position = form.SelectedPosition;

        var hasConflict = _items.Any(x =>
            x.VenueId == position.VenueId &&
            Intersects(x.StartDate, x.EndDate, position.StartDate, position.EndDate));

        if (hasConflict)
        {
            _notificationService.ShowError(
                "Конфликт позиции",
                "Эта площадка уже добавлена в заказ на пересекающиеся даты.");

            return;
        }

        _items.Add(new CreateOrderItemRow(
            position.TariffId,
            position.VenueId,
            position.VenueName,
            position.Price,
            position.StartDate,
            position.EndDate));

        _page = TotalPages == 0 ? 1 : TotalPages;

        UpdateSaveButtonState();
        RefreshOrderItemsGrid();
        _ = RefreshDiscountAsync();
        UpdateOrderTotal();
    }

    private void RefreshOrderItemsGrid()
    {
        if (TotalPages == 0)
        {
            _page = 1;
            _pagedItemsBindingSource.DataSource = new List<CreateOrderItemRow>();
            UpdateOrderItemsPagingState();
            return;
        }

        if (_page > TotalPages)
        {
            _page = TotalPages;
        }

        if (_page < 1)
        {
            _page = 1;
        }

        var pageItems = _items
            .Skip((_page - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        _pagedItemsBindingSource.DataSource = pageItems;

        UpdateOrderItemsPagingState();
    }

    private void UpdateOrderItemsPagingState()
    {
        orderItemsPagingUserControl.SetState(
            _items.Count,
            _page,
            TotalPages,
            _isLoading);

        orderItemsPagingUserControl.SetAddVisible(true);
        orderItemsPagingUserControl.SetAddEnabled(!_isLoading);
    }

    private void GoToPreviousOrderItemsPage()
    {
        if (_isLoading || _page <= 1)
        {
            return;
        }

        _page--;
        RefreshOrderItemsGrid();
    }

    private void GoToNextOrderItemsPage()
    {
        if (_isLoading || TotalPages == 0 || _page >= TotalPages)
        {
            return;
        }

        _page++;
        RefreshOrderItemsGrid();
    }

    private void UpdateOrderTotal()
    {
        var totalWithoutDiscount = CalculateTotalWithoutDiscount();
        var totalWithDiscount = ApplyDiscount(totalWithoutDiscount, _maxDiscountPercent);

        lblTotalAmount.Text = totalWithDiscount.ToString("N2");
    }

    private static int GetDaysCount(DateOnly startDate, DateOnly endDate)
    {
        return endDate.DayNumber - startDate.DayNumber + 1;
    }

    private static decimal CalculatePositionTotal(CreateOrderItemRow item)
    {
        var daysCount = GetDaysCount(item.StartDate, item.EndDate);
        return item.Price * daysCount;
    }

    private decimal CalculateTotalWithoutDiscount()
    {
        return _items.Sum(CalculatePositionTotal);
    }

    private static decimal ApplyDiscount(decimal total, decimal maxDiscountPercent)
    {
        return total * (1 - maxDiscountPercent / 100m);
    }

    private async void BtnSave_Click(object sender, EventArgs e)
    {
        if (_selectedContractId is null)
        {
            _notificationService.ShowError(
                "Не выбран договор",
                "Выберите договор для заказа.");
            return;
        }

        if (_items.Count == 0)
        {
            _notificationService.ShowError(
                "Нет позиций",
                "Добавьте хотя бы одну позицию в заказ.");
            return;
        }

        var dto = new CreateOrderDto(
            _selectedContractId.Value,
            _items.Select(x => new CreateOrderItemDto(
                x.TariffId,
                x.StartDate,
                x.EndDate)).ToList());

        var result = await _createOrderCommandHandler.Handle(
            new CreateOrderCommand(dto),
            CancellationToken.None);

        if (result.IsFailure)
        {
            var message = string.Join(Environment.NewLine, result.Error);
            _logger.LogError("Ошибка создания заказа: {Errors}", message);
            _notificationService.ShowError("Ошибка создания заказа", message);
            return;
        }

        _logger.LogInformation("Заказ создан с id = {OrderId}", result.Value);

        OrderCreated?.Invoke();
        DialogResult = DialogResult.OK;
        Close();
    }

    private static bool Intersects(
        DateOnly start1,
        DateOnly end1,
        DateOnly start2,
        DateOnly end2)
    {
        return start1 <= end2 && end1 >= start2;
    }
    
    private bool IsFormValid()
    {
        return _selectedContractId is not null
               && _items.Count > 0;
    }
    
    private void UpdateSaveButtonState()
    {
        btnSave.Enabled = IsFormValid();
    }
}