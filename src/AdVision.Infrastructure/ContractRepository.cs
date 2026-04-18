using System.Linq.Expressions;
using AdVision.Application;
using AdVision.Application.SharedErrors;
using AdVision.Contracts;
using AdVision.Domain.Contracts;
using AdVision.Domain.Customers;
using AdVision.Domain.Employees;
using CSharpFunctionalExtensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace AdVision.Infrastructure;

public class ContractRepository(
    ApplicationDbContext dbContext,
    ILogger<ContractRepository> logger) : IContractRepository
{
    public async Task<Result<Guid, Error>> AddAsync(Contract contract, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Contracts.AddAsync(contract, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.InnerException is SqliteException sqliteException)
        {
            if (sqliteException.SqliteErrorCode == 19 &&
                sqliteException.Message.Contains("contracts", StringComparison.InvariantCultureIgnoreCase))
            {
                return ContractErrors.ContractNumberConflict(contract.Number.Value);
            }

            logger.LogError(e, "Ошибка добавления нового договора '{ContractNumber}'", contract.Number.Value);

            return CommonErrors.Db(
                "add.contract.to.db.exception",
                $"Ошибка добавления нового договора '{contract.Number.Value}'");
        }
        catch (OperationCanceledException e)
        {
            logger.LogError(e, "Операция создания нового договора '{ContractNumber}' была отменена",
                contract.Number.Value);

            return CommonErrors.OperationCancelled("add.contract.was.canceled");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Ошибка добавления нового договора '{ContractNumber}'", contract.Number.Value);

            return CommonErrors.Db(
                "add.contract.to.db.exception",
                $"Ошибка добавления нового договора '{contract.Number.Value}'");
        }

        return contract.Id.Value;
    }

    public async Task<Result<Contract?, Error>> GetByAsync(
        Expression<Func<Contract, bool>> expression,
        CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .Contracts
                .Include(x => x.Customer)
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(expression, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения договора была отменена");
            return CommonErrors.OperationCancelled("get.contract.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении договора");
            return CommonErrors.Db(
                "get.contract.from.db.exception",
                "Ошибка при получении договора");
        }
    }

    public async Task<Result<IReadOnlyList<Contract>, Error>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .Contracts
                .Include(x => x.Customer)
                .Include(x => x.Employee)
                .ToListAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения всех договоров была отменена");
            return CommonErrors.OperationCancelled("get.contracts.operation.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении всех договоров");
            return CommonErrors.Db(
                "get.contracts.from.db.exception",
                "Ошибка при получении всех договоров");
        }
    }

    public async Task<Result<PagedResult<Contract>, Error>> GetPagedAsync(
        int page,
        int pageSize,
        string? number,
        CustomerId? customerId,
        EmployeeId? employeeId,
        ContractStatus? status,
        DateOnly startDateFrom,
        DateOnly startDateTo,
        DateOnly endDateFrom,
        DateOnly endDateTo,
        DateOnly signedDateFrom,
        DateOnly signedDateTo,
        string? orderBy,
        bool descending,
        CancellationToken cancellationToken)
    {
        try
        {
            IQueryable<Contract> query = dbContext.Contracts
                .Include(x => x.Customer)
                .Include(x => x.Employee);

            if (!string.IsNullOrWhiteSpace(number))
            {
                var pattern = $"%{number.Trim()}%";
                query = query.Where(x => EF.Functions.Like(x.Number.Value, pattern));
            }

            if (customerId is not null)
            {
                query = query.Where(x => x.CustomerId == customerId);
            }

            if (employeeId is not null)
            {
                query = query.Where(x => x.EmployeeId == employeeId);
            }

            if (status.HasValue)
            {
                query = query.Where(x => x.Status == status.Value);
            }

            query = query.Where(x =>
                x.DateInterval.StartDate >= startDateFrom &&
                x.DateInterval.StartDate <= startDateTo);

            query = query.Where(x =>
                x.DateInterval.EndDate >= endDateFrom &&
                x.DateInterval.EndDate <= endDateTo);

            query = query.Where(x =>
                x.SignedDate.HasValue &&
                x.SignedDate.Value >= signedDateFrom &&
                x.SignedDate.Value <= signedDateTo
                ||
                !x.SignedDate.HasValue && signedDateFrom == DateOnly.FromDateTime(DateTime.MinValue) &&
                signedDateTo == DateOnly.FromDateTime(DateTime.MaxValue));

            query = ApplySorting(query, orderBy, descending);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<Contract>(
                items,
                page,
                pageSize,
                totalCount);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения страницы договоров была отменена");
            return CommonErrors.OperationCancelled("get.contracts.page.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении страницы договоров");
            return CommonErrors.Db(
                "get.contracts.page.from.db.exception",
                "Ошибка при получении страницы договоров");
        }
    }

    private static IQueryable<Contract> ApplySorting(
        IQueryable<Contract> query,
        string? orderBy,
        bool descending)
    {
        return (orderBy, descending) switch
        {
            ("Номер договора", false) => query.OrderBy(x => x.Number),
            ("Номер договора", true) => query.OrderByDescending(x => x.Number),

            ("Исполнитель", false) => query
                .OrderBy(x => x.Employee.LastName)
                .ThenBy(x => x.Employee.FirstName)
                .ThenBy(x => x.Employee.MiddleName),

            ("Исполнитель", true) => query
                .OrderByDescending(x => x.Employee.LastName)
                .ThenByDescending(x => x.Employee.FirstName)
                .ThenByDescending(x => x.Employee.MiddleName),

            ("Заказчик", false) => query
                .OrderBy(x => x.Customer.LastName)
                .ThenBy(x => x.Customer.FirstName)
                .ThenBy(x => x.Customer.MiddleName),

            ("Заказчик", true) => query
                .OrderByDescending(x => x.Customer.LastName)
                .ThenByDescending(x => x.Customer.FirstName)
                .ThenByDescending(x => x.Customer.MiddleName),

            ("Дата начала", false) => query.OrderBy(x => x.DateInterval.StartDate),
            ("Дата начала", true) => query.OrderByDescending(x => x.DateInterval.StartDate),

            ("Дата окончания", false) => query.OrderBy(x => x.DateInterval.EndDate),
            ("Дата окончания", true) => query.OrderByDescending(x => x.DateInterval.EndDate),

            ("Дата подписания", false) => query.OrderBy(x => x.SignedDate),
            ("Дата подписания", true) => query.OrderByDescending(x => x.SignedDate),

            ("Статус", false) => query.OrderBy(x => x.Status),
            ("Статус", true) => query.OrderByDescending(x => x.Status),

            _ => query.OrderBy(x => x.Number)
        };
    }
    
    public async Task<Result<ContractDateBoundsDto?, Error>> GetDateBoundsAsync(CancellationToken cancellationToken)
    {
        try
        {
            var contracts = await dbContext.Contracts
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (contracts.Count == 0)
            {
                return Result.Success<ContractDateBoundsDto?, Error>(null);
            }

            var startDateMin = contracts.Min(x => x.DateInterval.StartDate);
            var startDateMax = contracts.Max(x => x.DateInterval.StartDate);

            var endDateMin = contracts.Min(x => x.DateInterval.EndDate);
            var endDateMax = contracts.Max(x => x.DateInterval.EndDate);

            var signedDates = contracts
                .Where(x => x.SignedDate.HasValue)
                .Select(x => x.SignedDate!.Value)
                .ToList();

            DateOnly? signedDateMin = signedDates.Count > 0 ? signedDates.Min() : null;
            DateOnly? signedDateMax = signedDates.Count > 0 ? signedDates.Max() : null;

            return new ContractDateBoundsDto(
                startDateMin,
                startDateMax,
                endDateMin,
                endDateMax,
                signedDateMin,
                signedDateMax);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения границ дат договоров была отменена");
            return CommonErrors.OperationCancelled("get.contract.date.bounds.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении границ дат договоров");
            return CommonErrors.Db(
                "get.contract.date.bounds.from.db.exception",
                "Ошибка при получении границ дат договоров");
        }
    }
}