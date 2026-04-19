using Shared.Abstractions;

namespace AdVision.Application.Venues.GetAvailableVenuesQuery;

public sealed record GetAvailableVenuesQuery(
    int Page,
    int PageSize,
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