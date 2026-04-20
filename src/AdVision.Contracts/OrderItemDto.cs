namespace AdVision.Contracts;

public sealed record OrderItemDto(
    Guid Id,
    Guid TariffId,
    Guid VenueId,
    string VenueName,
    decimal Price,
    DateOnly StartDate,
    DateOnly EndDate,
    string Status);