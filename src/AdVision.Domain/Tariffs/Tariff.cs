using AdVision.Domain.Venues;
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Tariffs;

public sealed class Tariff
{
    /// <summary>
    /// Минимальная стоимость размещения в день
    /// </summary>
    public static readonly Money MIN_PRICE = Money.Create(500).Value;

    /// <summary>
    /// Уникальный идентификатор тарифа
    /// </summary>
    public TariffId Id { get; private set; }

    /// <summary>
    /// Идентификатор площадки, FK
    /// </summary>
    public VenueId VenueId { get; private set; }

    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public Venue Venue { get; init; } = null!;

    /// <summary>
    /// Временной интервал
    /// </summary>
    public DateInterval Interval { get; init; }

    /// <summary>
    /// Стоимость размещения за сутки
    /// </summary>
    public Money Price { get; private set; }

    private Tariff(VenueId venueId, DateInterval interval, Money price)
    {
        Id = new TariffId(Guid.NewGuid());
        VenueId = venueId;
        Interval = interval;
        Price = price;
    }

    public static Result<Tariff, Error> Create(
        VenueId venueId,
        DateInterval interval,
        decimal price)
    {
        var priceResult = Money.Create(price);

        if (priceResult.IsFailure)
        {
            return priceResult.Error;
        }

        if (priceResult.Value.Value < MIN_PRICE.Value)
        {
            return CommonErrors.ValueIsLessThanMin(
                nameof(price),
                (double)price,
                (double)MIN_PRICE.Value);
        }

        return new Tariff(venueId, interval, priceResult.Value);
    }

    // EF Core
    private Tariff()
    {
    }
}