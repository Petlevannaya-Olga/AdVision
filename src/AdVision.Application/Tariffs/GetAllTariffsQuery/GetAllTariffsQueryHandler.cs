using AdVision.Application.Repositories;
using AdVision.Contracts;
using AdVision.Domain.Tariffs;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Tariffs.GetAllTariffsQuery;

public sealed class GetAllTariffsQueryHandler(
    ITariffRepository tariffRepository)
    : IQueryHandler<IReadOnlyList<TariffDto>, GetAllTariffsQuery>
{
    public async Task<Result<IReadOnlyList<TariffDto>, Errors>> Handle(
        GetAllTariffsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await tariffRepository.GetAllAsync(cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }

        var items = result.Value
            .Select(x => new TariffDto(
                x.Id.Value,
                x.VenueId.Value,
                x.Interval.StartDate,
                x.Interval.EndDate,
                x.Price.Value))
            .ToList();

        return items;
    }

    private static string BuildDisplayName(Tariff tariff)
    {
        return $"Площадка: {tariff.VenueId.Value} | " +
               $"Период: {tariff.Interval.StartDate:dd.MM.yyyy} - {tariff.Interval.EndDate:dd.MM.yyyy} | " +
               $"Цена: {tariff.Price.Value:N2}";
    }
}