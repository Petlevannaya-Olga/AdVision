using System.Linq.Expressions;
using AdVision.Application;
using AdVision.Application.Repositories;
using AdVision.Contracts;
using AdVision.Domain.Customers;
using AdVision.Domain.Employees;
using AdVision.Domain.Orders;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace AdVision.Infrastructure.Repositories;

public class OrderRepository(
    ApplicationDbContext dbContext,
    ILogger<OrderRepository> logger) : IOrderRepository
{
    public async Task<Result<Guid, Error>> AddAsync(Order order, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Orders.AddAsync(order, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (OperationCanceledException e)
        {
            logger.LogError(e, "Операция создания нового заказа была отменена");

            return CommonErrors.OperationCancelled("add.order.was.canceled");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Ошибка добавления нового заказа '{OrderId}'", order.Id.Value);

            return CommonErrors.Db(
                "add.order.to.db.exception",
                $"Ошибка добавления нового заказа '{order.Id.Value}'");
        }

        return order.Id.Value;
    }

    public async Task<Result<Order?, Error>> GetByAsync(
        Expression<Func<Order, bool>> expression,
        CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .Orders
                .Include(x => x.Items)
                .Include(x => x.Contract)
                .ThenInclude(x => x.Customer)
                .Include(x => x.Contract)
                .ThenInclude(x => x.Employee)
                .FirstOrDefaultAsync(expression, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения заказа была отменена");
            return CommonErrors.OperationCancelled("get.order.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении заказа");
            return CommonErrors.Db(
                "get.order.from.db.exception",
                "Ошибка при получении заказа");
        }
    }

    public async Task<Result<IReadOnlyList<Order>, Error>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .Orders
                .Include(x => x.Items)
                .Include(x => x.Contract)
                .ThenInclude(x => x.Customer)
                .Include(x => x.Contract)
                .ThenInclude(x => x.Employee)
                .ToListAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения всех заказов была отменена");
            return CommonErrors.OperationCancelled("get.orders.operation.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении всех заказов");
            return CommonErrors.Db(
                "get.orders.from.db.exception",
                "Ошибка при получении всех заказов");
        }
    }

    public async Task<Result<PagedResult<Order>, Error>> GetPagedAsync(
        int page,
        int pageSize,
        CustomerId? customerId,
        EmployeeId? employeeId,
        OrderStatus? status,
        DateOnly startDateFrom,
        DateOnly startDateTo,
        DateOnly endDateFrom,
        DateOnly endDateTo,
        string? orderBy,
        bool descending,
        CancellationToken cancellationToken)
    {
        try
        {
            IQueryable<Order> query = dbContext.Orders
                .Include(x => x.Contract)
                .ThenInclude(x => x.Customer)
                .Include(x => x.Contract)
                .ThenInclude(x => x.Employee)
                .Include(x => x.Items);

            if (customerId is not null)
            {
                query = query.Where(x => x.Contract.CustomerId == customerId);
            }

            if (employeeId is not null)
            {
                query = query.Where(x => x.Contract.EmployeeId == employeeId);
            }

            if (status.HasValue)
            {
                query = query.Where(x => x.Status == status.Value);
            }

            query = query.Where(x =>
                x.Items.Any() &&
                x.Items.Min(i => i.Period.StartDate) >= startDateFrom &&
                x.Items.Min(i => i.Period.StartDate) <= startDateTo);

            query = query.Where(x =>
                x.Items.Any() &&
                x.Items.Max(i => i.Period.EndDate) >= endDateFrom &&
                x.Items.Max(i => i.Period.EndDate) <= endDateTo);

            query = ApplySorting(query, orderBy, descending);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<Order>(
                items,
                page,
                pageSize,
                totalCount);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения страницы заказов была отменена");
            return CommonErrors.OperationCancelled("get.orders.page.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении страницы заказов");
            return CommonErrors.Db(
                "get.orders.page.from.db.exception",
                "Ошибка при получении страницы заказов");
        }
    }

    public async Task<Result<OrderFilterBoundsDto?, Error>> GetFilterBoundsAsync(CancellationToken cancellationToken)
    {
        try
        {
            var orders = await dbContext.Orders
                .Include(x => x.Items)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (orders.Count == 0)
            {
                return Result.Success<OrderFilterBoundsDto?, Error>(null);
            }

            var totalAmountMin = orders.Min(x => x.TotalAmount.Value);
            var totalAmountMax = orders.Max(x => x.TotalAmount.Value);

            var allItems = orders
                .SelectMany(x => x.Items)
                .ToList();

            DateOnly? startDateMin = allItems.Count > 0
                ? allItems.Min(x => x.Period.StartDate)
                : null;

            DateOnly? startDateMax = allItems.Count > 0
                ? allItems.Max(x => x.Period.StartDate)
                : null;

            DateOnly? endDateMin = allItems.Count > 0
                ? allItems.Min(x => x.Period.EndDate)
                : null;

            DateOnly? endDateMax = allItems.Count > 0
                ? allItems.Max(x => x.Period.EndDate)
                : null;

            return new OrderFilterBoundsDto(
                totalAmountMin,
                totalAmountMax,
                startDateMin,
                startDateMax,
                endDateMin,
                endDateMax);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения границ фильтров заказов была отменена");
            return CommonErrors.OperationCancelled("get.order.filter.bounds.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении границ фильтров заказов");
            return CommonErrors.Db(
                "get.order.filter.bounds.from.db.exception",
                "Ошибка при получении границ фильтров заказов");
        }
    }

    private static IQueryable<Order> ApplySorting(
        IQueryable<Order> query,
        string? orderBy,
        bool descending)
    {
        return (orderBy, descending) switch
        {
            ("Номер заказа", false) => query.OrderBy(x => x.Id),
            ("Номер заказа", true) => query.OrderByDescending(x => x.Id),

            ("Договор", false) => query.OrderBy(x => x.Contract.Number),
            ("Договор", true) => query.OrderByDescending(x => x.Contract.Number),

            ("Исполнитель", false) => query
                .OrderBy(x => x.Contract.Employee.LastName)
                .ThenBy(x => x.Contract.Employee.FirstName)
                .ThenBy(x => x.Contract.Employee.MiddleName),

            ("Исполнитель", true) => query
                .OrderByDescending(x => x.Contract.Employee.LastName)
                .ThenByDescending(x => x.Contract.Employee.FirstName)
                .ThenByDescending(x => x.Contract.Employee.MiddleName),

            ("Заказчик", false) => query
                .OrderBy(x => x.Contract.Customer.LastName)
                .ThenBy(x => x.Contract.Customer.FirstName)
                .ThenBy(x => x.Contract.Customer.MiddleName),

            ("Заказчик", true) => query
                .OrderByDescending(x => x.Contract.Customer.LastName)
                .ThenByDescending(x => x.Contract.Customer.FirstName)
                .ThenByDescending(x => x.Contract.Customer.MiddleName),

            ("Сумма", false) => query.OrderBy(x => x.TotalAmount.Value),
            ("Сумма", true) => query.OrderByDescending(x => x.TotalAmount.Value),

            ("Статус", false) => query.OrderBy(x => x.Status),
            ("Статус", true) => query.OrderByDescending(x => x.Status),

            ("Дата начала", false) => query.OrderBy(x => x.Items.Min(i => i.Period.StartDate)),
            ("Дата начала", true) => query.OrderByDescending(x => x.Items.Min(i => i.Period.StartDate)),

            ("Дата окончания", false) => query.OrderBy(x => x.Items.Max(i => i.Period.EndDate)),
            ("Дата окончания", true) => query.OrderByDescending(x => x.Items.Max(i => i.Period.EndDate)),

            _ => query.OrderBy(x => x.Id)
        };
    }
    
    public async Task<Result<IReadOnlyList<OrderStatusDto>, Error>> GetDistinctStatusesAsync(
        CancellationToken cancellationToken)
    {
        try
        {
            var statuses = await dbContext.Orders
                .AsNoTracking()
                .Select(x => x.Status)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync(cancellationToken);

            var result = statuses
                .Select(x => x switch
                {
                    OrderStatus.Planned => OrderStatusDto.Planned,
                    OrderStatus.InProgress => OrderStatusDto.InProgress,
                    OrderStatus.Completed => OrderStatusDto.Completed,
                    OrderStatus.Cancelled => OrderStatusDto.Cancelled,
                    _ => throw new ArgumentOutOfRangeException(nameof(x), x, null)
                })
                .ToList();

            return Result.Success<IReadOnlyList<OrderStatusDto>, Error>(result);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения статусов заказов была отменена");
            return CommonErrors.OperationCancelled("get.order.statuses.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка получения статусов заказов");
            return CommonErrors.Db(
                "get.order.statuses.from.db.exception",
                "Ошибка получения статусов заказов");
        }
    }
    
    public async Task<Result<IReadOnlyList<CustomerDto>, Error>> GetDistinctCustomersAsync(
        CancellationToken cancellationToken)
    {
        try
        {
            var customers = await dbContext.Orders
                .AsNoTracking()
                .Include(x => x.Contract)
                .ThenInclude(x => x.Customer)
                .Select(x => x.Contract.Customer)
                .Distinct()
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ThenBy(x => x.MiddleName)
                .Select(x => new CustomerDto(
                    x.Id.Value,
                    x.LastName.Value,
                    x.FirstName.Value,
                    x.MiddleName.Value,
                    $"{x.LastName.Value} {x.FirstName.Value} {x.MiddleName.Value}"))
                .ToListAsync(cancellationToken);

            return Result.Success<IReadOnlyList<CustomerDto>, Error>(customers);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения заказчиков из заказов была отменена");
            return CommonErrors.OperationCancelled("get.order.customers.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка получения заказчиков из заказов");
            return CommonErrors.Db(
                "get.order.customers.from.db.exception",
                "Ошибка получения заказчиков из заказов");
        }
    }
    
    public async Task<Result<IReadOnlyList<EmployeeOrderDto>, Error>> GetDistinctEmployeesAsync(
        CancellationToken cancellationToken)
    {
        try
        {
            var employees = await dbContext.Orders
                .AsNoTracking()
                .Include(x => x.Contract)
                .ThenInclude(x => x.Employee)
                .Select(x => x.Contract.Employee)
                .Distinct()
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ThenBy(x => x.MiddleName)
                .Select(x => new EmployeeOrderDto(
                    x.Id.Value,
                    x.LastName.Value,
                    x.FirstName.Value,
                    x.MiddleName.Value))
                .ToListAsync(cancellationToken);

            return Result.Success<IReadOnlyList<EmployeeOrderDto>, Error>(employees);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения исполнителей из заказов была отменена");
            return CommonErrors.OperationCancelled("get.order.employees.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка получения исполнителей из заказов");
            return CommonErrors.Db(
                "get.order.employees.from.db.exception",
                "Ошибка получения исполнителей из заказов");
        }
    }
    
    public async Task<Result<OrderDateBoundsDto?, Error>> GetDateBoundsAsync(
        CancellationToken cancellationToken)
    {
        try
        {
            var orderItems = await dbContext.OrderItems
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (orderItems.Count == 0)
            {
                return Result.Success<OrderDateBoundsDto?, Error>(null);
            }

            var startDateMin = orderItems.Min(x => x.Period.StartDate);
            var startDateMax = orderItems.Max(x => x.Period.StartDate);

            var endDateMin = orderItems.Min(x => x.Period.EndDate);
            var endDateMax = orderItems.Max(x => x.Period.EndDate);

            return Result.Success<OrderDateBoundsDto?, Error>(
                new OrderDateBoundsDto(
                    startDateMin,
                    startDateMax,
                    endDateMin,
                    endDateMax));
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения границ дат заказов была отменена");
            return CommonErrors.OperationCancelled("get.order.date.bounds.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении границ дат заказов");
            return CommonErrors.Db(
                "get.order.date.bounds.from.db.exception",
                "Ошибка при получении границ дат заказов");
        }
    }
}