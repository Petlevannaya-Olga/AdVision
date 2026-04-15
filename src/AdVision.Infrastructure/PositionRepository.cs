using System.Diagnostics.CodeAnalysis;
using AdVision.Application;
using AdVision.Domain.Positions;
using System.Linq.Expressions;
using AdVision.Application.SharedErrors;
using CSharpFunctionalExtensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace AdVision.Infrastructure;

public class PositionRepository(
    ApplicationDbContext dbContext,
    ILogger<PositionRepository> logger) : IPositionRepository
{
    public async Task<Result<Guid, Error>> AddAsync(Position position, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Positions.AddAsync(position, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.InnerException is SqliteException sqliteException)
        {
            // SQLITE_CONSTRAINT = 19
            if (sqliteException.SqliteErrorCode == 19 &&
                sqliteException.Message.Contains("positions", StringComparison.InvariantCultureIgnoreCase))
            {
                return PositionErrors.PositionNameConflict(position.Name.Value);
            }

            logger.LogError(e, "Ошибка добавления новой позиции '{PositionName}'", position.Name.Value);

            return CommonErrors.Db(
                "add.position.to.db.exception",
                $"Ошибка добавления новой позиции '{position.Name.Value}'");
        }
        catch (OperationCanceledException e)
        {
            logger.LogError(e, "Операция создания новой позиции '{PositionName}' была отменена", position.Name.Value);

            return CommonErrors.OperationCancelled("add.position.was.canceled");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Ошибка добавления новой позиции '{PositionName}'", position.Name.Value);

            return CommonErrors.Db(
                "add.position.to.db.exception",
                $"Ошибка добавления новой позиции '{position.Name.Value}'");
        }

        return position.Id.Value;
    }

    public async Task<Result<Position?, Error>> GetByAsync(
        Expression<Func<Position, bool>> expression,
        CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .Positions
                .FirstOrDefaultAsync(expression, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения позиции была отменена");
            return CommonErrors.OperationCancelled("get.position.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении позиции");
            return CommonErrors.Db(
                "get.position.from.db.exception",
                "Ошибка при получении позиции");
        }
    }

    public async Task<Result<IReadOnlyList<Position>, Error>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .Positions
                .ToListAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения всех позиций была отменена");
            return CommonErrors.OperationCancelled("get.positions.operation.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении всех позиций");
            return CommonErrors.Db(
                "get.positions.from.db.exception",
                "Ошибка при получении всех позиций");
        }
    }
}