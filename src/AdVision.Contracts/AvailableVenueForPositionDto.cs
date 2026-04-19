namespace AdVision.Contracts;

public sealed record AvailableVenueForPositionDto(
    Guid VenueId,
    Guid TariffId,
    string VenueName,
    string VenueTypeName,
    string Region,
    string District,
    string City,
    string Street,
    int Rating,
    decimal Price,
    DateOnly TariffStartDate,
    DateOnly TariffEndDate,
    int FreeDaysCount,
    bool HasPartialAvailability);