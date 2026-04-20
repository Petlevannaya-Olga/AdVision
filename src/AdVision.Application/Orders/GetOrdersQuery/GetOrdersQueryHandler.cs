using AdVision.Application.Repositories;
using AdVision.Contracts;
using AdVision.Domain.Customers;
using AdVision.Domain.Employees;
using AdVision.Domain.Orders;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Orders.GetOrdersQuery;

public sealed class GetOrdersQueryHandler(
    IOrderRepository orderRepository)
    : IQueryHandler<PagedResult<OrderDto>, GetOrdersQuery>
{
    public async Task<Result<PagedResult<OrderDto>, Errors>> Handle(
        GetOrdersQuery query,
        CancellationToken cancellationToken)
    {
        CustomerId? customerId = query.CustomerId.HasValue
            ? new CustomerId(query.CustomerId.Value)
            : null;

        EmployeeId? employeeId = query.EmployeeId.HasValue
            ? new EmployeeId(query.EmployeeId.Value)
            : null;

        OrderStatus? status = query.Status.HasValue
            ? MapStatus(query.Status.Value)
            : null;

        var result = await orderRepository.GetPagedAsync(
            query.Page,
            query.PageSize,
            customerId,
            employeeId,
            status,
            query.StartDateFrom,
            query.StartDateTo,
            query.EndDateFrom,
            query.EndDateTo,
            query.OrderBy,
            query.Descending,
            cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }

        var items = result.Value.Items
            .Select(x => new OrderDto(
                x.Id.Value,
                x.Contract.Number.Value,
                BuildEmployeeName(x),
                BuildCustomerName(x),
                x.TotalAmount.Value,
                x.Items.Count > 0 ? x.Items.Min(i => i.Period.StartDate) : null,
                x.Items.Count > 0 ? x.Items.Max(i => i.Period.EndDate) : null,
                MapStatus(x.Status)))
            .ToList();

        return new PagedResult<OrderDto>(
            items,
            result.Value.Page,
            result.Value.PageSize,
            result.Value.TotalCount);
    }

    private static string BuildEmployeeName(Order x)
    {
        var e = x.Contract.Employee;
        return $"{e.LastName.Value} {e.FirstName.Value} {e.MiddleName.Value}";
    }

    private static string BuildCustomerName(Order x)
    {
        var c = x.Contract.Customer;
        return $"{c.LastName.Value} {c.FirstName.Value} {c.MiddleName.Value}";
    }

    private static OrderStatus MapStatus(OrderStatusDto status)
    {
        return status switch
        {
            OrderStatusDto.Planned => OrderStatus.Planned,
            OrderStatusDto.InProgress => OrderStatus.InProgress,
            OrderStatusDto.Completed => OrderStatus.Completed,
            OrderStatusDto.Cancelled => OrderStatus.Cancelled,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }

    private static OrderStatusDto MapStatus(OrderStatus status)
    {
        return status switch
        {
            OrderStatus.Planned => OrderStatusDto.Planned,
            OrderStatus.InProgress => OrderStatusDto.InProgress,
            OrderStatus.Completed => OrderStatusDto.Completed,
            OrderStatus.Cancelled => OrderStatusDto.Cancelled,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}