using System.Linq.Expressions;
using AdVision.Domain.Venues;
using Shared.Abstractions;

namespace AdVision.Application.Venues.GetDistinctQuery;

public sealed record GetDistinctQuery(Expression<Func<Venue, string>> Selector) : IQuery;