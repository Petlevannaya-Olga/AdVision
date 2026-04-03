using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Venues;

public sealed class VenueRating(double Value)
{
    /// <summary>
    /// Значение рейтинга
    /// </summary>
    public double Value { get; private set; } = Value;

    /// <summary>
    /// Минимальное значение рейтинга
    /// </summary>
    public const double MIN = 1;

    /// <summary>
    /// Максимальное значение рейтинга
    /// </summary>
    public const double MAX = 10;

    /// <summary>
    /// Фабричный метод
    /// </summary>
    /// <param name="value">Значение</param>
    /// <returns>Новый рейтинг</returns>
    public static Result<VenueRating, Error> Create(double value)
    {
        return value switch
        {
            < MIN => CommonErrors.ValueIsLessThanMin(nameof(value), value, MIN),
            > MAX => CommonErrors.ValueIsGreaterThanMax(nameof(value), value, MAX),
            _ => new VenueRating(value)
        };
    }
}