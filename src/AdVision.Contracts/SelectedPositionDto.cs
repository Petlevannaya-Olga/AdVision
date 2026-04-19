namespace AdVision.Contracts;

public sealed record SelectedPositionDto(
    Guid TariffId,
    Guid VenueId,
    string VenueName,
    decimal Price,
    DateOnly StartDate,
    DateOnly EndDate);