using System.Linq.Expressions;
using AdVision.Domain.VenueTypes;
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Application;

public interface IVenueTypeRepository
{
    Task<Result<Guid, Error>> AddAsync(VenueType venueType, CancellationToken cancellationToken);

    Task<Result<VenueType?, Error>> GetByAsync(
        Expression<Func<VenueType, bool>> expression,
        CancellationToken cancellationToken);
    
    Task<Result<IReadOnlyList<VenueType>, Error>> GetAllAsync(CancellationToken cancellationToken);
}