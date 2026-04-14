using AdVision.Application;
using AdVision.Domain.Tariffs;
using AdVision.Domain.Venues;
using CSharpFunctionalExtensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace AdVision.Infrastructure;

public class TariffRepository(ApplicationDbContext dbContext, ILogger<VenueRepository> logger) : ITariffRepository
{
    public async Task<Result<IReadOnlyList<Tariff>, Error>> GetByVenueIdAsync(
        VenueId venueId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var tariffs = await dbContext.Tariffs
                .Where(x => x.VenueId == venueId)
                .OrderBy(x => x.Interval.StartDate)
                .ThenBy(x => x.Interval.EndDate)
                .ToListAsync(cancellationToken);

            return tariffs;
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation(
                "Операция получения тарифов по площадке {VenueId} была отменена",
                venueId.Value);

            return CommonErrors.OperationCancelled("get.tariffs.by.venue.operation.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при получении тарифов по площадке {VenueId}",
                venueId.Value);

            return CommonErrors.Db(
                "get.tariffs.by.venue.from.db.exception",
                "Ошибка при получении тарифов по площадке");
        }
    }
    
    public async Task<Result<TariffId, Error>> AddAsync(
        Tariff tariff,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await dbContext.Tariffs.AddAsync(tariff, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.InnerException is SqliteException sqliteException)
        {
            // SQLITE_CONSTRAINT = 19
            if (sqliteException.SqliteErrorCode == 19 &&
                sqliteException.Message.Contains("tariffs", StringComparison.InvariantCultureIgnoreCase))
            {
                return CommonErrors.Conflict(
                    "tariff.is.conflict",
                    "Тариф с таким интервалом для этой площадки уже существует");
            }

            logger.LogError(
                e,
                "Ошибка добавления тарифа для площадки {VenueId}",
                tariff.VenueId.Value);

            return CommonErrors.Db(
                "add.tariff.to.db.exception",
                "Ошибка добавления тарифа");
        }
        catch (OperationCanceledException e)
        {
            logger.LogError(
                e,
                "Операция создания тарифа для площадки {VenueId} была отменена",
                tariff.VenueId.Value);

            return CommonErrors.OperationCancelled("add.tariff.was.canceled");
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Ошибка добавления тарифа для площадки {VenueId}",
                tariff.VenueId.Value);

            return CommonErrors.Db(
                "add.tariff.to.db.exception",
                "Ошибка добавления тарифа");
        }

        return tariff.Id;
    }
}