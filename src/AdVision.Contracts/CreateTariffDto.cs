namespace AdVision.Contracts;

public sealed record CreateTariffDto(
    Guid VenueId,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal Price
);