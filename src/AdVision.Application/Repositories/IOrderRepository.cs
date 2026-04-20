using System.Linq.Expressions;
using AdVision.Contracts;
using AdVision.Domain.Customers;
using AdVision.Domain.Employees;
using AdVision.Domain.Orders;
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Application.Repositories;

public interface IOrderRepository
{
    Task<Result<Guid, Error>> AddAsync(Order order, CancellationToken cancellationToken);

    Task<Result<Order?, Error>> GetByAsync(
        Expression<Func<Order, bool>> expression,
        CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<Order>, Error>> GetAllAsync(CancellationToken cancellationToken);

    Task<Result<PagedResult<Order>, Error>> GetPagedAsync(
        int page,
        int pageSize,
        string? contractNumber,
        CustomerId? customerId,
        EmployeeId? employeeId,
        OrderStatus? status,
        DateOnly startDateFrom,
        DateOnly startDateTo,
        DateOnly endDateFrom,
        DateOnly endDateTo,
        string? orderBy,
        bool descending,
        CancellationToken cancellationToken);

    Task<Result<OrderFilterBoundsDto?, Error>> GetFilterBoundsAsync(CancellationToken cancellationToken);
    
    Task<Result<IReadOnlyList<OrderStatusDto>, Error>> GetDistinctStatusesAsync(
        CancellationToken cancellationToken);
    
    Task<Result<IReadOnlyList<EmployeeOrderDto>, Error>> GetDistinctEmployeesAsync(
        CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<CustomerDto>, Error>> GetDistinctCustomersAsync(
        CancellationToken cancellationToken);
    
    Task<Result<OrderDateBoundsDto?, Error>> GetDateBoundsAsync(
        CancellationToken cancellationToken);
}