namespace AdVision.Contracts;

public sealed record SelectedPositionDto(
    Guid TariffId,
    string VenueName,
    decimal Price,
    DateOnly StartDate,
    DateOnly EndDate);