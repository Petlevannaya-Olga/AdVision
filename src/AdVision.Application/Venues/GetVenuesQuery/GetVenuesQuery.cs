using System.Linq.Expressions;
using AdVision.Domain.Venues;
using Shared.Abstractions;

namespace AdVision.Application.Venues.GetVenuesQuery;

public sealed record GetVenuesQuery(
    int Page,
    int Size,
    Expression<Func<Venue, bool>>? Filter = null,
    Expression<Func<Venue, object>>? OrderBy = null,
    bool OrderByDescending = false) : IQuery;