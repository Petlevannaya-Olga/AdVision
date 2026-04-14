using AdVision.Domain.Tariffs;
using AdVision.Domain.Venues;
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Application;

public interface ITariffRepository
{
    Task<Result<IReadOnlyList<Tariff>, Error>> GetByVenueIdAsync(
        VenueId venueId,
        CancellationToken cancellationToken = default);

    Task<Result<TariffId, Error>> AddAsync(
        Tariff tariff,
        CancellationToken cancellationToken = default);
}