using System.Linq.Expressions;
using AdVision.Domain.Venues;
using Shared.Abstractions;

namespace AdVision.Application.Venues.GetVenueByAsyncQuery;

public sealed record GetVenueByQueryAsync(Expression<Func<Venue, bool>> Expression) : IQuery;