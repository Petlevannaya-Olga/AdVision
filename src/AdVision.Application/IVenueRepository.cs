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
    
    Task<Result<IReadOnlyList<Venue>, Error>> GetAsync(int page, int size, CancellationToken cancellationToken);
}