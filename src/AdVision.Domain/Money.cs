using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain;

public sealed class Money
{
    /// <summary>
    /// Значение суммы
    /// </summary>
    public decimal Value { get; private set; }

    private Money(decimal value)
    {
        Value = value;
    }
    
    public static bool operator <(Money left, Money right) => left.Value < right.Value;
    public static bool operator <=(Money left, Money right) => left.Value <= right.Value;
    public static bool operator >(Money left, Money right) => left.Value > right.Value;
    public static bool operator >=(Money left, Money right) => left.Value >= right.Value;

    public static Result<Money, Error> Create(decimal value)
    {
        const string propertyName = nameof(value);

        if (value < 0)
        {
            return CommonErrors.ValueIsLessThanMin(propertyName, (double)value, 0);
        }

        var rounded = decimal.Round(value, 2, MidpointRounding.AwayFromZero);

        return new Money(rounded);
    }

    /// <summary>
    /// Нулевая сумма
    /// </summary>
    public static Money Zero() => new(0m);

    /// <summary>
    /// Сложение
    /// </summary>
    public Money Add(Money other)
    {
        return new(decimal.Round(Value + other.Value, 2, MidpointRounding.AwayFromZero));
    }

    /// <summary>
    /// Умножение
    /// </summary>
    public Money Multiply(decimal factor)
    {
        return new(decimal.Round(Value * factor, 2, MidpointRounding.AwayFromZero));
    }

    /// <summary>
    /// Применить скидку
    /// </summary>
    public Money ApplyDiscount(decimal percent)
    {
        var factor = 1 - percent / 100m;
        return Multiply(factor);
    }

    public override string ToString()
    {
        return $"{Value:N2}";
    }

    // EF Core
    private Money()
    {
    }
}