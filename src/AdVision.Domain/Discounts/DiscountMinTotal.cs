using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Discounts;

public sealed class DiscountMinTotal
{
    /// <summary>
    /// Минимальное значение
    /// </summary>
    public const decimal MIN_VALUE = 1;

    public decimal Value { get; private set; }

    /// <summary>
    /// Приватный конструктор
    /// </summary>
    /// <param name="value">Значение</param>
    private DiscountMinTotal(decimal value)
    {
        Value = value;
    }

    /// <summary>
    /// Фабричный метод
    /// </summary>
    /// <param name="value">Значение</param>
    /// <returns>Новая скидка</returns>
    public static Result<DiscountMinTotal, Error> Create(decimal value)
    {
        if (value < MIN_VALUE)
        {
            return CommonErrors.Validation(
                $"discount.min.total.less.than.zero",
                $"Минимальная сумма не может быть меньше {MIN_VALUE}");
        }

        return new DiscountMinTotal(value);
    }
}