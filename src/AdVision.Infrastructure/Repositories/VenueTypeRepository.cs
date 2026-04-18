using System.Linq.Expressions;
using AdVision.Application.Repositories;
using AdVision.Application.SharedErrors;
using AdVision.Domain.VenueTypes;
using CSharpFunctionalExtensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace AdVision.Infrastructure.Repositories;

public class VenueTypeRepository(
    ApplicationDbContext dbContext,
    ILogger<VenueTypeRepository> logger) : IVenueTypeRepository
{
    public async Task<Result<Guid, Error>> AddAsync(VenueType venueType, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.VenueTypes.AddAsync(venueType, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.InnerException is SqliteException sqliteException)
        {
            // SQLITE_CONSTRAINT = 19
            if (sqliteException.SqliteErrorCode == 19 &&
                sqliteException.Message.Contains("venue_types", StringComparison.InvariantCultureIgnoreCase))
            {
                return VenueTypeErrors.VenueTypeNameConflict(venueType.Name.Value);
            }

            logger.LogError(e, "Ошибка добавления нового типа площадки '{VenueTypeName}'", venueType.Name.Value);

            return CommonErrors.Db(
                "add.venue.type.to.db.exception",
                $"Ошибка добавления нового типа площадки '{venueType.Name.Value}'");
        }
        catch (OperationCanceledException e)
        {
            logger.LogError(e, "Операция создания нового типа площадки '{VenueTypeName}' была отменена", venueType.Name.Value);

            return CommonErrors.OperationCancelled("add.venue.type.was.canceled");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Ошибка добавления нового типа площадки '{VenueTypeName}'", venueType.Name.Value);

            return CommonErrors.Db(
                "add.venue.type.to.db.exception",
                $"Ошибка добавления нового типа площадки '{venueType.Name.Value}'");
        }

        return venueType.Id.Value;
    }

    public async Task<Result<VenueType?, Error>> GetByAsync(Expression<Func<VenueType, bool>> expression,
        CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .VenueTypes
                .FirstOrDefaultAsync(expression, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения типа площадки была отменена");
            return CommonErrors.OperationCancelled("get.venue.type.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении типа площадки");
            return CommonErrors.Db(
                "get.venue.type.from.db.exception",
                "Ошибка при получении типа площадки");
        }
    }

    public async Task<Result<IReadOnlyList<VenueType>, Error>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .VenueTypes
                .ToListAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения всех типов площадок была отменена");
            return CommonErrors.OperationCancelled("get.venue.types.operation.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении всех площадок");
            return CommonErrors.Db(
                "get.venue.types.from.db.exception",
                "Ошибка при получении всех площадок");
        }
    }
}