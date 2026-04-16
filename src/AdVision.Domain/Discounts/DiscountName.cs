using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Discounts;

public sealed class DiscountName
{
    /// <summary>
    /// Минимальная длина строки
    /// </summary>
    public const int MIN_LENGTH = 10;
    
    /// <summary>
    /// Максимальная длина строки
    /// </summary>
    public const int MAX_LENGTH = 200;

    /// <summary>
    /// Значение
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Приватный конструктор
    /// </summary>
    /// <param name="value">Значение</param>
    private DiscountName(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Фабричный метод
    /// </summary>
    /// <param name="value">Значение</param>
    /// <returns>Новое название</returns>
    public static Result<DiscountName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return CommonErrors.IsRequired(nameof(value));
        }

        var trimmed = value.Trim();

        if (trimmed.Length is < MIN_LENGTH or > MAX_LENGTH)
        {
            return CommonErrors.LengthIsWrong(nameof(value), MIN_LENGTH, MAX_LENGTH);
        }

        return new DiscountName(trimmed);
    }
}