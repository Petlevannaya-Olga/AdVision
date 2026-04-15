using System.Linq.Expressions;
using AdVision.Domain.Venues;
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Application;

public interface IVenueRepository
{
    Task<Result<Guid, Error>> AddAsync(Venue venue, CancellationToken cancellationToken);

    Task<Result<Venue?, Error>> GetByAsync(
        Expression<Func<Venue, bool>> expression,
        CancellationToken cancellationToken);

    Task<Result<PagedResult<Venue>, Error>> GetAsync(
        int page,
        int size,
        Expression<Func<Venue, bool>>? filter = null,
        Expression<Func<Venue, object>>? orderBy = null,
        bool orderByDescending = false,
        CancellationToken cancellationToken = default);

    Task<Result<IReadOnlyList<string>, Error>> GetDistinctAsync(
        Expression<Func<Venue, string>> selector,
        CancellationToken cancellationToken);
}