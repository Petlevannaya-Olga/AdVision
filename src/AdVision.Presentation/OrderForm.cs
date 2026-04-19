using System.ComponentModel;
using AdVision.Application.Orders.CreateOrderCommand;
using AdVision.Contracts;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation;

public partial class OrderForm : Form
{
    private readonly ICommandHandler<Guid, CreateOrderCommand> _createOrderCommandHandler;
    private readonly INotificationService _notificationService;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OrderForm> _logger;

    private Guid? _selectedContractId;
    private readonly BindingList<CreateOrderItemRow> _items = [];

    public event Action? OrderCreated;

    public OrderForm(
        ICommandHandler<Guid, CreateOrderCommand> createOrderCommandHandler,
        INotificationService notificationService,
        IServiceProvider serviceProvider,
        ILogger<OrderForm> logger)
    {
        _createOrderCommandHandler = createOrderCommandHandler;
        _notificationService = notificationService;
        _serviceProvider = serviceProvider;
        _logger = logger;

        InitializeComponent();

        pbContractValidation.Image = Properties.Resources.exception;

        ConfigureOrderItemsGrid();
        SubscribeControls();
        UpdateOrderTotal();
    }

    private void SubscribeControls()
    {
        orderItemsPagingUserControl.AddClicked += AddPosition;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
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

        dgvOrderItems.DataSource = _items;
    }

    private void AddPosition()
    {
        var form = _serviceProvider.GetRequiredService<CreatePositionForm>();

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

        UpdateOrderTotal();
    }

    private void UpdateOrderTotal()
    {
        var total = _items.Sum(x => x.Price);
        lblTotalAmount.Text = @"Итого:" + total.ToString("N2") + @"с учетом скидки!!! по дням";
        // TODO с учетом скидки
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
    
    
}