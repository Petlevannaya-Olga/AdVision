using AdVision.Application.Repositories;
using AdVision.Domain;
using AdVision.Domain.Tariffs;
using AdVision.Domain.Venues;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Tariffs.CreateTariffCommand;

public sealed class CreateTariffCommandHandler(
    ITariffRepository repository,
    ILogger<CreateTariffCommandHandler> logger)
    : ICommandHandler<TariffId, CreateTariffCommand>
{
    public async Task<Result<TariffId, Errors>> Handle(
        CreateTariffCommand command,
        CancellationToken cancellationToken = default)
    {
        var dto = command.Dto;

        var venueId = new VenueId(dto.VenueId);

        var intervalResult = DateInterval.Create(dto.StartDate, dto.EndDate);
        if (intervalResult.IsFailure)
        {
            return intervalResult.Error.ToErrors();
        }

        var existingTariffsResult = await repository.GetByVenueIdAsync(
            venueId,
            null,
            cancellationToken);

        if (existingTariffsResult.IsFailure)
        {
            logger.LogError(
                "Не удалось получить тарифы по площадке {VenueId}: {Error}",
                dto.VenueId,
                existingTariffsResult.Error);

            return existingTariffsResult.Error.ToErrors();
        }

        var hasIntersection = existingTariffsResult.Value.Any(x =>
            x.Interval.StartDate <= dto.EndDate &&
            x.Interval.EndDate >= dto.StartDate);

        if (hasIntersection)
        {
            return CommonErrors.Conflict(
                    "tariff.interval.intersects",
                    "Для этой площадки уже существует тариф, пересекающийся с указанным интервалом")
                .ToErrors();
        }

        var tariffResult = Tariff.Create(
            venueId,
            intervalResult.Value,
            dto.Price);

        if (tariffResult.IsFailure)
        {
            return tariffResult.Error.ToErrors();
        }

        var addResult = await repository.AddAsync(
            tariffResult.Value,
            cancellationToken);

        if (addResult.IsFailure)
        {
            logger.LogError(
                "Не удалось создать тариф для площадки {VenueId}: {Error}",
                dto.VenueId,
                addResult.Error);

            return addResult.Error.ToErrors();
        }

        logger.LogInformation(
            "Создан тариф {TariffId} для площадки {VenueId}",
            addResult.Value.Value,
            dto.VenueId);

        return addResult.Value;
    }
}