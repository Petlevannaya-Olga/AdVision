using System.Linq.Expressions;
using AdVision.Domain.Tariffs;
using AdVision.Domain.Venues;
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Application.Repositories;

public interface ITariffRepository
{
    Task<Result<IReadOnlyList<Tariff>, Error>> GetByVenueIdAsync(
        VenueId venueId,
        Expression<Func<Tariff, bool>>? filter = null,
        CancellationToken cancellationToken = default);


    Task<Result<TariffId, Error>> AddAsync(
        Tariff tariff,
        CancellationToken cancellationToken = default);
    
    Task<Result<Tariff?, Error>> GetByAsync(
        Expression<Func<Tariff, bool>> expression,
        CancellationToken cancellationToken);
    
    Task<Result<IReadOnlyList<Tariff>, Error>> GetAllAsync(CancellationToken cancellationToken);
}