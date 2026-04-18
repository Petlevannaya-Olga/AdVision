using AdVision.Domain.Tariffs;

namespace AdVision.Domain.Orders;

public sealed class OrderItem
{
    /// <summary>
    /// Идентификатор, PK
    /// </summary>
    public OrderItemId Id { get; private set; }

    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public OrderId OrderId { get; private set; }

    /// <summary>
    /// Идентификатор тарифа
    /// </summary>
    public TariffId TariffId { get; private set; }

    /// <summary>
    /// Статус позиции заказа
    /// </summary>
    public OrderItemStatus Status { get; private set; }

    /// <summary>
    /// Цена позиции на момент создания заказа
    /// </summary>
    public Money Price { get; private set; }

    /// <summary>
    /// Период размещения
    /// </summary>
    public DateInterval Period { get; private set; }

    public OrderItem(
        OrderId orderId,
        TariffId tariffId,
        Money price,
        DateInterval period)
    {
        Id = new OrderItemId(Guid.NewGuid());
        OrderId = orderId;
        TariffId = tariffId;
        Status = OrderItemStatus.Planned;
        Price = price;
        Period = period;
    }

    // EF Core
    private OrderItem()
    {
    }
}