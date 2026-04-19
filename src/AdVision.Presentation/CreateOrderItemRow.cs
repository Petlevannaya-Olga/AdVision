namespace AdVision.Presentation;

public sealed record CreateOrderItemRow(
    Guid TariffId,
    Guid VenueId,
    string VenueName,
    decimal Price,
    DateOnly StartDate,
    DateOnly EndDate);