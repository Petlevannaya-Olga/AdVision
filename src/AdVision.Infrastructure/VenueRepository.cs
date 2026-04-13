using System.Linq.Expressions;
using AdVision.Application;
using AdVision.Domain.Venues;
using CSharpFunctionalExtensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace AdVision.Infrastructure;

public class VenueRepository(ApplicationDbContext dbContext, ILogger<VenueRepository> logger) : IVenueRepository
{
    public async Task<Result<Guid, Error>> AddAsync(Venue venue, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Venues.AddAsync(venue, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.InnerException is SqliteException sqliteException)
        {
            // SQLITE_CONSTRAINT = 19
            if (sqliteException.SqliteErrorCode == 19 &&
                sqliteException.Message.Contains("venues", StringComparison.InvariantCultureIgnoreCase))
            {
                return CommonErrors.Conflict("venue.is.conflict", "Площадка уже существует");
            }

            logger.LogError(e, "Ошибка добавления новой площадки '{VenueName}'", venue.Name.Value);

            return CommonErrors.Db(
                "add.venue.to.db.exception",
                $"Ошибка добавления новой площадки '{venue.Name.Value}'");
        }
        catch (OperationCanceledException e)
        {
            logger.LogError(e, "Операция создания новой площадки '{VenueName}' была отменена", venue.Name.Value);

            return CommonErrors.OperationCancelled("add.venue.was.canceled");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Ошибка добавления новой площадки '{VenueName}'", venue.Name.Value);

            return CommonErrors.Db(
                "add.venue.to.db.exception",
                $"Ошибка добавления новой площадки '{venue.Name.Value}'");
        }

        return venue.Id.Value;
    }

    public async Task<Result<Venue?, Error>> GetByAsync(Expression<Func<Venue, bool>> expression,
        CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .Venues
                .FirstOrDefaultAsync(expression, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения площадки была отменена");
            return CommonErrors.OperationCancelled("get.venue.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении площадки");
            return CommonErrors.Db(
                "get.venue.from.db.exception",
                "Ошибка при получении площадки");
        }
    }

    public async Task<Result<IReadOnlyList<Venue>, Error>> GetAsync(int page, int size,
        CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .Venues
                .Include(v => v.Type)
                .Skip((page - 1) * size)
                .Take(size)
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

    public async Task<Result<IReadOnlyList<string>, Error>> GetDistinctAsync(
        Expression<Func<Venue, string>> selector,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await dbContext
                .Venues
                .AsNoTracking()
                .Select(selector)
                .Distinct()
                .ToListAsync(cancellationToken);

            return result;
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения значений была отменена");
            return CommonErrors.OperationCancelled("get.values.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении значений");
            return CommonErrors.Db(
                "get.values.from.db.exception",
                "Ошибка при получении значений");
        }
    }
}