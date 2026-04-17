using System.Linq.Expressions;
using AdVision.Application;
using AdVision.Application.SharedErrors;
using AdVision.Domain.CustomerDiscounts;
using AdVision.Domain.Customers;
using CSharpFunctionalExtensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace AdVision.Infrastructure;

public class CustomerDiscountRepository(
    ApplicationDbContext dbContext,
    ILogger<CustomerDiscountRepository> logger) : ICustomerDiscountRepository
{
    public async Task<Result<Guid, Error>> AddAsync(CustomerDiscount customerDiscount, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.CustomerDiscounts.AddAsync(customerDiscount, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.InnerException is SqliteException sqliteException)
        {
            if (sqliteException.SqliteErrorCode == 19 &&
                sqliteException.Message.Contains("customer_discounts", StringComparison.InvariantCultureIgnoreCase))
            {
                return CustomerDiscountErrors.AlreadyAssigned(
                    customerDiscount.CustomerId.Value,
                    customerDiscount.DiscountId.Value);
            }

            logger.LogError(
                e,
                "Ошибка добавления связи клиента '{CustomerId}' и скидки '{DiscountId}'",
                customerDiscount.CustomerId.Value,
                customerDiscount.DiscountId.Value);

            return CommonErrors.Db(
                "add.customer.discount.to.db.exception",
                $"Ошибка добавления связи клиента '{customerDiscount.CustomerId.Value}' и скидки '{customerDiscount.DiscountId.Value}'");
        }
        catch (OperationCanceledException e)
        {
            logger.LogError(
                e,
                "Операция создания связи клиента '{CustomerId}' и скидки '{DiscountId}' была отменена",
                customerDiscount.CustomerId.Value,
                customerDiscount.DiscountId.Value);

            return CommonErrors.OperationCancelled("add.customer.discount.was.canceled");
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Ошибка добавления связи клиента '{CustomerId}' и скидки '{DiscountId}'",
                customerDiscount.CustomerId.Value,
                customerDiscount.DiscountId.Value);

            return CommonErrors.Db(
                "add.customer.discount.to.db.exception",
                $"Ошибка добавления связи клиента '{customerDiscount.CustomerId.Value}' и скидки '{customerDiscount.DiscountId.Value}'");
        }

        return customerDiscount.Id.Value;
    }

    public async Task<Result<CustomerDiscount?, Error>> GetByAsync(
        Expression<Func<CustomerDiscount, bool>> expression,
        CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .CustomerDiscounts
                .FirstOrDefaultAsync(expression, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения связи клиента и скидки была отменена");
            return CommonErrors.OperationCancelled("get.customer.discount.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении связи клиента и скидки");
            return CommonErrors.Db(
                "get.customer.discount.from.db.exception",
                "Ошибка при получении связи клиента и скидки");
        }
    }

    public async Task<Result<IReadOnlyList<CustomerDiscount>, Error>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .CustomerDiscounts
                .ToListAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения всех связей клиентов и скидок была отменена");
            return CommonErrors.OperationCancelled("get.customer.discounts.operation.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении всех связей клиентов и скидок");
            return CommonErrors.Db(
                "get.customer.discounts.from.db.exception",
                "Ошибка при получении всех связей клиентов и скидок");
        }
    }

    public async Task<Result<IReadOnlyList<CustomerDiscount>, Error>> GetByCustomerIdAsync(
        CustomerId customerId,
        CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .CustomerDiscounts
                .Where(x => x.CustomerId == customerId)
                .ToListAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError(
                "Операция получения скидок клиента '{CustomerId}' была отменена",
                customerId.Value);

            return CommonErrors.OperationCancelled("get.customer.discounts.by.customer.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при получении скидок клиента '{CustomerId}'",
                customerId.Value);

            return CommonErrors.Db(
                "get.customer.discounts.by.customer.from.db.exception",
                $"Ошибка при получении скидок клиента '{customerId.Value}'");
        }
    }
}