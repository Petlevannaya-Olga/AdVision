using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Discounts;

public sealed class DiscountPercent
{
    /// <summary>
    /// Минимальное значение
    /// </summary>
    public const decimal MIN_VALUE = 1;
    
    /// <summary>
    /// Максимальное значение
    /// </summary>
    public const decimal MAX_VALUE = 100;

    /// <summary>
    /// Значение
    /// </summary>
    public decimal Value { get; private set; }

    /// <summary>
    /// Приватный конструктор
    /// </summary>
    /// <param name="value">Значение</param>
    private DiscountPercent(decimal value)
    {
        Value = value;
    }

    /// <summary>
    /// Фабричный метод
    /// </summary>
    /// <param name="value">Значение</param>
    /// <returns>Новый процент скидки</returns>
    public static Result<DiscountPercent, Error> Create(decimal value)
    {
        if (value is < MIN_VALUE or > MAX_VALUE)
        {
            return CommonErrors.Validation(
                "discount.percent.invalid",
                $"Процент скидки должен быть от {MIN_VALUE} до {MAX_VALUE}");
        }

        return new DiscountPercent(value);
    }
}