using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Orders;

public sealed class DiscountPercent
{
    public decimal Value { get; private set; }

    private DiscountPercent(decimal value)
    {
        Value = value;
    }

    public static Result<DiscountPercent, Error> Create(decimal value)
    {
        const string propertyName = nameof(value);

        return value switch
        {
            < 0 => CommonErrors.ValueIsLessThanMin(propertyName, (double)value, 0),
            > 100 => CommonErrors.ValueIsGreaterThanMax(propertyName, (double)value, 100),
            _ => new DiscountPercent(value)
        };
    }

    // EF Core
    private DiscountPercent()
    {
    }
}