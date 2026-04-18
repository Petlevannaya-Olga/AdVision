namespace AdVision.Contracts;

public sealed record TariffDto(
    Guid Id,
    Guid VenueId,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal Price
);