using AdVision.Domain.Venues;
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Tariffs;

public sealed class Tariff
{
    /// <summary>
    /// Минимальная стоимость размещения в день
    /// </summary>
    public const double MIN_PRICE = 500;
    
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
    public double Price { get; private set; }

    /// <summary>
    /// Приватный конструктор
    /// </summary>
    /// <param name="venueId">Идентификатор площадки</param>
    /// <param name="interval">Интервал размещения</param>
    /// <param name="price">Стоимость размещения за сутки</param>
    private Tariff(VenueId venueId, DateInterval interval, double price)
    {
        Id = new TariffId(Guid.NewGuid());
        VenueId = venueId;
        Interval = interval;
        Price = price;
    }

    /// <summary>
    /// Фабричный метод
    /// </summary>
    /// <param name="venueId">Идентификатор площадки</param>
    /// <param name="interval">Интервал размещения</param>
    /// <param name="price">Стоимость размещения за сутки</param>
    /// <returns>Новый тариф</returns>
    public static Result<Tariff, Error> Create(VenueId venueId, DateInterval interval, double price)
    {
        if (price <= MIN_PRICE)
        {
            CommonErrors.ValueIsLessThanMin(nameof(price), price, MIN_PRICE);
        }
        
        return new Tariff(venueId, interval, price);
    }

    // EF Core
    private Tariff()
    {
    }
}