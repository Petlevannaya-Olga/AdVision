using AdVision.Domain.VenueTypes;

namespace AdVision.Domain.Venues;

public sealed class Venue
{
    /// <summary>
    /// Идентификатор, PK
    /// </summary>
    public VenueId Id { get; private set; }

    /// <summary>
    /// Внешний ключ venue_type_id
    /// </summary>
    public VenueTypeId VenueTypeId { get; private set; }
    
    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public VenueType VenueType { get; private set; }

    /// <summary>
    /// Адрес площадки
    /// </summary>
    public VenueAddress Address { get; private set; }

    /// <summary>
    /// Название площадки
    /// </summary>
    public VenueName Name { get; private set; }

    /// <summary>
    /// Размер площадки
    /// </summary>
    public VenueSize Size { get; private set; }

    /// <summary>
    /// Рейтинг
    /// </summary>
    public VenueRating Rating { get; private set; }

    /// <summary>
    /// Описание площадки
    /// </summary>
    public VenueDescription Description { get; private set; }

    /// <summary>
    /// Конструктор с параметрами
    /// </summary>
    /// <param name="name">Название</param>
    /// <param name="venueTypeId">Внешний ключ venue_type_id</param>
    /// <param name="address">Адрес</param>
    /// <param name="size">Размер</param>
    /// <param name="rating">Рейтинг</param>
    /// <param name="description">Описание площадки</param>
    public Venue(
        VenueName name,
        VenueTypeId venueTypeId,
        VenueAddress address,
        VenueSize size,
        VenueRating rating,
        VenueDescription description)
    {
        Id = new VenueId(Guid.NewGuid());
        Name = name;
        // VenueTypeId = venueTypeId;
        Address = address;
        Size = size;
        Rating = rating;
        Description = description;
    }

    // EF Core
    private Venue()
    {
    }
}