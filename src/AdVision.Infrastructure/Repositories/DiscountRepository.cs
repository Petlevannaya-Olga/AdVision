using System.Linq.Expressions;
using AdVision.Application.Repositories;
using AdVision.Application.SharedErrors;
using AdVision.Domain.Discounts;
using CSharpFunctionalExtensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace AdVision.Infrastructure.Repositories;

public class DiscountRepository(
    ApplicationDbContext dbContext,
    ILogger<DiscountRepository> logger) : IDiscountRepository
{
    public async Task<Result<Guid, Error>> AddAsync(Discount discount, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Discounts.AddAsync(discount, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.InnerException is SqliteException sqliteException)
        {
            // SQLITE_CONSTRAINT = 19
            if (sqliteException.SqliteErrorCode == 19 &&
                sqliteException.Message.Contains("discounts", StringComparison.InvariantCultureIgnoreCase))
            {
                return DiscountErrors.DiscountNameConflict(discount.Name.Value);
            }

            logger.LogError(e, "Ошибка добавления новой скидки '{DiscountName}'", discount.Name.Value);

            return CommonErrors.Db(
                "add.discount.to.db.exception",
                $"Ошибка добавления новой скидки '{discount.Name.Value}'");
        }
        catch (OperationCanceledException e)
        {
            logger.LogError(e, "Операция создания новой скидки '{DiscountName}' была отменена", discount.Name.Value);

            return CommonErrors.OperationCancelled("add.discount.was.canceled");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Ошибка добавления новой скидки '{DiscountName}'", discount.Name.Value);

            return CommonErrors.Db(
                "add.discount.to.db.exception",
                $"Ошибка добавления новой скидки '{discount.Name.Value}'");
        }

        return discount.Id.Value;
    }

    public async Task<Result<Discount?, Error>> GetByAsync(
        Expression<Func<Discount, bool>> expression,
        CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .Discounts
                .FirstOrDefaultAsync(expression, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения скидки была отменена");
            return CommonErrors.OperationCancelled("get.discount.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении скидки");
            return CommonErrors.Db(
                "get.discount.from.db.exception",
                "Ошибка при получении скидки");
        }
    }

    public async Task<Result<IReadOnlyList<Discount>, Error>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .Discounts
                .ToListAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения всех скидок была отменена");
            return CommonErrors.OperationCancelled("get.discounts.operation.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении всех скидок");
            return CommonErrors.Db(
                "get.discounts.from.db.exception",
                "Ошибка при получении всех скидок");
        }
    }
}