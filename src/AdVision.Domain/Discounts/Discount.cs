namespace AdVision.Domain.Discounts;

public sealed class Discount
{
    /// <summary>
    /// Идентификатор, PK
    /// </summary>
    public DiscountId Id { get; private set; }

    /// <summary>
    /// Название
    /// </summary>
    public DiscountName Name { get; private set; }

    /// <summary>
    /// Процент скидки
    /// </summary>
    public DiscountPercent Percent { get; private set; }

    /// <summary>
    /// Минимальная сумма заказа
    /// </summary>
    public DiscountMinTotal MinTotal { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="name">Название скидки</param>
    /// <param name="percent">Процент скидки</param>
    /// <param name="minTotal">Минимальная сумма заказа</param>
    public Discount(
        DiscountName name,
        DiscountPercent percent,
        DiscountMinTotal minTotal)
    {
        Id = new DiscountId(Guid.NewGuid());
        Name = name;
        Percent = percent;
        MinTotal = minTotal;
    }

    // EF Core
    private Discount()
    {
    }
}