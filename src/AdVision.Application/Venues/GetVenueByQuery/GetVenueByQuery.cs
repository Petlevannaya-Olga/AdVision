using System.Linq.Expressions;
using AdVision.Domain.Venues;
using Shared.Abstractions;

namespace AdVision.Application.Venues.GetVenueByQuery;

public sealed record GetVenueByQuery(Expression<Func<Venue, bool>> Expression) : IQuery;