using System.Linq.Expressions;
using AdVision.Domain.Tariffs;
using AdVision.Domain.Venues;
using Shared.Abstractions;

namespace AdVision.Application.Tariffs.GetTariffsByVenueIdQuery;

public sealed record GetTariffsByVenueIdQuery(
    VenueId VenueId,
    Expression<Func<Tariff, bool>>? Filter = null
) : IQuery;