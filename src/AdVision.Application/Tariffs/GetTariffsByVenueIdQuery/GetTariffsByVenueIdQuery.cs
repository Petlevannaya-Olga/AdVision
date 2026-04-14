using AdVision.Domain.Venues;
using Shared.Abstractions;

namespace AdVision.Application.Tariffs.GetTariffsByVenueIdQuery;

public sealed record GetTariffsByVenueIdQuery(VenueId VenueId) : IQuery;