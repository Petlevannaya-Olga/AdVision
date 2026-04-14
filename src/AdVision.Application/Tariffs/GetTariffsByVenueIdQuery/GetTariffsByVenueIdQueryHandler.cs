using AdVision.Contracts;
using AdVision.Domain.Tariffs;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Tariffs.GetTariffsByVenueIdQuery;

public sealed class GetTariffsByVenueIdQueryHandler(
    ITariffRepository repository,
    ILogger<GetTariffsByVenueIdQueryHandler> logger)
    : IQueryHandler<IReadOnlyList<TariffDto>, GetTariffsByVenueIdQuery>
{
    public async Task<Result<IReadOnlyList<TariffDto>, Errors>> Handle(
        GetTariffsByVenueIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetByVenueIdAsync(
            query.VenueId,
            cancellationToken);

        if (result.IsFailure)
        {
            logger.LogError(
                "Не удалось получить тарифы по площадке {VenueId}: {Error}",
                query.VenueId.Value,
                result.Error);

            return result.Error.ToErrors();
        }

        var dtos = result.Value
            .Select(Map)
            .ToList();

        return dtos;
    }

    private static TariffDto Map(Tariff tariff)
    {
        return new TariffDto(
            tariff.Id.Value,
            tariff.VenueId.Value,
            tariff.Interval.StartDate,
            tariff.Interval.EndDate,
            tariff.Price
        );
    }
}