using AdVision.Domain.Contracts;
using AdVision.Domain.Tariffs;

namespace AdVision.Domain.Orders;

public sealed class Order
{
    private readonly List<OrderItem> _items = [];

    /// <summary>
    /// Идентификатор, PK
    /// </summary>
    public OrderId Id { get; private set; }

    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus Status { get; private set; }

    /// <summary>
    /// Итоговая сумма заказа с учетом скидки
    /// </summary>
    public Money TotalAmount { get; private set; }

    /// <summary>
    /// Идентификатор договора
    /// </summary>
    public ContractId ContractId { get; private set; }
    
    public Contract Contract { get; private set; }

    /// <summary>
    /// Позиции заказа
    /// </summary>
    public IReadOnlyList<OrderItem> Items => _items;

    public Order(
        ContractId contractId,
        IEnumerable<(TariffId tariffId, Money price, DateInterval period)> items,
        decimal maxDiscountPercent)
    {
        Id = new OrderId(Guid.NewGuid());
        Status = OrderStatus.InProgress;
        ContractId = contractId;

        foreach (var item in items)
        {
            _items.Add(new OrderItem(
                Id,
                item.tariffId,
                item.price,
                item.period));
        }

        TotalAmount = CalculateTotalAmount(maxDiscountPercent);
    }

    private Money CalculateTotalAmount(decimal maxDiscountPercent)
    {
        var total = Money.Zero();

        foreach (var item in _items)
        {
            total = total.Add(item.Price);
        }

        return total.ApplyDiscount(maxDiscountPercent);
    }

    // EF Core
    private Order()
    {
    }
}