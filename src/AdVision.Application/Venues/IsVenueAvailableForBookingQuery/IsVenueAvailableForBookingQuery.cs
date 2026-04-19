using AdVision.Domain.Venues;
using Shared.Abstractions;

namespace AdVision.Application.Venues.IsVenueAvailableForBookingQuery;

public sealed record IsVenueAvailableForBookingQuery(VenueId VenueId, DateOnly DateFrom, DateOnly DateTo) : IQuery;