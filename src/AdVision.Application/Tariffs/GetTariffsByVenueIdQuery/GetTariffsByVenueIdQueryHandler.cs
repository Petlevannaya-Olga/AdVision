using AdVision.Contracts;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Tariffs.GetTariffsByVenueIdQuery;

public sealed class GetTariffsByVenueIdQueryHandler(
    ITariffRepository repository)
    : IQueryHandler<IReadOnlyList<TariffDto>, GetTariffsByVenueIdQuery>
{
    public async Task<Result<IReadOnlyList<TariffDto>, Errors>> Handle(
        GetTariffsByVenueIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetByVenueIdAsync(
            query.VenueId,
            query.Filter,
            cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }

        return result.Value
            .Select(x => new TariffDto(
                x.Id.Value,
                x.VenueId.Value,
                x.Interval.StartDate,
                x.Interval.EndDate,
                x.Price))
            .ToList();
    }
}