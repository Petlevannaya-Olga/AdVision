using System.Linq.Expressions;
using AdVision.Application;
using AdVision.Application.SharedErrors;
using AdVision.Domain.Employees;
using CSharpFunctionalExtensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace AdVision.Infrastructure;

public sealed class EmployeeRepository(
    ApplicationDbContext dbContext,
    ILogger<EmployeeRepository> logger) : IEmployeeRepository
{
    public async Task<Result<Guid, Error>> AddAsync(Employee employee, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Employees.AddAsync(employee, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.InnerException is SqliteException sqliteException)
        {
            // SQLITE_CONSTRAINT = 19
            if (sqliteException.SqliteErrorCode == 19)
            {
                if (sqliteException.Message.Contains("phone_number", StringComparison.InvariantCultureIgnoreCase))
                {
                    return EmployeeErrors.PhoneNumberConflict(employee.PhoneNumber.Value);
                }

                if (sqliteException.Message.Contains("passport_series", StringComparison.InvariantCultureIgnoreCase) ||
                    sqliteException.Message.Contains("passport_number", StringComparison.InvariantCultureIgnoreCase))
                {
                    return EmployeeErrors.PassportConflict(
                        employee.Passport.Series.Value,
                        employee.Passport.Number.Value);
                }

                if (sqliteException.Message.Contains("employees", StringComparison.InvariantCultureIgnoreCase))
                {
                    logger.LogError(
                        e,
                        "Конфликт при добавлении нового сотрудника '{LastName} {FirstName} {MiddleName}'",
                        employee.LastName.Value,
                        employee.FirstName.Value,
                        employee.MiddleName.Value);

                    return CommonErrors.Db(
                        "add.employee.to.db.conflict",
                        "Конфликт при добавлении нового сотрудника");
                }
            }

            logger.LogError(
                e,
                "Ошибка добавления нового сотрудника '{LastName} {FirstName} {MiddleName}'",
                employee.LastName.Value,
                employee.FirstName.Value,
                employee.MiddleName.Value);

            return CommonErrors.Db(
                "add.employee.to.db.exception",
                $"Ошибка добавления нового сотрудника '{employee.LastName.Value} {employee.FirstName.Value} {employee.MiddleName.Value}'");
        }
        catch (OperationCanceledException e)
        {
            logger.LogError(
                e,
                "Операция создания нового сотрудника '{LastName} {FirstName} {MiddleName}' была отменена",
                employee.LastName.Value,
                employee.FirstName.Value,
                employee.MiddleName.Value);

            return CommonErrors.OperationCancelled("add.employee.was.canceled");
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Ошибка добавления нового сотрудника '{LastName} {FirstName} {MiddleName}'",
                employee.LastName.Value,
                employee.FirstName.Value,
                employee.MiddleName.Value);

            return CommonErrors.Db(
                "add.employee.to.db.exception",
                $"Ошибка добавления нового сотрудника '{employee.LastName.Value} {employee.FirstName.Value} {employee.MiddleName.Value}'");
        }

        return employee.Id.Value;
    }

    public async Task<Result<Employee?, Error>> GetByAsync(
        Expression<Func<Employee, bool>> expression,
        CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .Employees
                .FirstOrDefaultAsync(expression, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения сотрудника была отменена");
            return CommonErrors.OperationCancelled("get.employee.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении сотрудника");
            return CommonErrors.Db(
                "get.employee.from.db.exception",
                "Ошибка при получении сотрудника");
        }
    }

    public async Task<Result<IReadOnlyList<Employee>, Error>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .Employees
                .ToListAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения всех сотрудников была отменена");
            return CommonErrors.OperationCancelled("get.employees.operation.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении всех сотрудников");
            return CommonErrors.Db(
                "get.employees.from.db.exception",
                "Ошибка при получении всех сотрудников");
        }
    }
}