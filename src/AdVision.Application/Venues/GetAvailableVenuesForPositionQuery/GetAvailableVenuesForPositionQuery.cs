using Shared.Abstractions;

namespace AdVision.Application.Venues.GetAvailableVenuesForPositionQuery;

public sealed record GetAvailableVenuesForPositionQuery(
    string? Name,
    Guid? VenueTypeId,
    string? Region,
    string? District,
    string? City,
    string? Street,
    int RatingFrom,
    int RatingTo,
    decimal? PriceFrom,
    decimal? PriceTo,
    DateOnly? DateFrom,
    DateOnly? DateTo) : IQuery;