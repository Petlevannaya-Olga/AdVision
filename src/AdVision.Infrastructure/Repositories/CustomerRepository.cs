using System.Linq.Expressions;
using AdVision.Application.Repositories;
using AdVision.Application.SharedErrors;
using AdVision.Domain.Customers;
using CSharpFunctionalExtensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace AdVision.Infrastructure.Repositories;

public class CustomerRepository(
    ApplicationDbContext dbContext,
    ILogger<CustomerRepository> logger) : ICustomerRepository
{
    public async Task<Result<Guid, Error>> AddAsync(Customer customer, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Customers.AddAsync(customer, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.InnerException is SqliteException sqliteException)
        {
            // SQLITE_CONSTRAINT = 19
            if (sqliteException.SqliteErrorCode == 19 &&
                sqliteException.Message.Contains("customers", StringComparison.InvariantCultureIgnoreCase))
            {
                return CustomerErrors.CustomerPhoneConflict(customer.PhoneNumber.Value);
            }

            logger.LogError(e, "Ошибка добавления нового клиента с телефоном '{PhoneNumber}'", customer.PhoneNumber.Value);

            return CommonErrors.Db(
                "add.customer.to.db.exception",
                $"Ошибка добавления нового клиента с телефоном '{customer.PhoneNumber.Value}'");
        }
        catch (OperationCanceledException e)
        {
            logger.LogError(e, "Операция создания нового клиента с телефоном '{PhoneNumber}' была отменена", customer.PhoneNumber.Value);

            return CommonErrors.OperationCancelled("add.customer.was.canceled");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Ошибка добавления нового клиента с телефоном '{PhoneNumber}'", customer.PhoneNumber.Value);

            return CommonErrors.Db(
                "add.customer.to.db.exception",
                $"Ошибка добавления нового клиента с телефоном '{customer.PhoneNumber.Value}'");
        }

        return customer.Id.Value;
    }

    public async Task<Result<Customer?, Error>> GetByAsync(
        Expression<Func<Customer, bool>> expression,
        CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .Customers
                .FirstOrDefaultAsync(expression, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения клиента была отменена");
            return CommonErrors.OperationCancelled("get.customer.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении клиента");
            return CommonErrors.Db(
                "get.customer.from.db.exception",
                "Ошибка при получении клиента");
        }
    }

    public async Task<Result<IReadOnlyList<Customer>, Error>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .Customers
                .ToListAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения всех клиентов была отменена");
            return CommonErrors.OperationCancelled("get.customers.operation.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении всех клиентов");
            return CommonErrors.Db(
                "get.customers.from.db.exception",
                "Ошибка при получении всех клиентов");
        }
    }
}