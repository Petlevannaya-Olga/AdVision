using AdVision.Application.Repositories;
using AdVision.Contracts;
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
        var result = await orderRepository.GetPagedAsync(
            query.Page,
            query.PageSize,
            null,
            null,
            null,
            null,
            null,
            null,
            DateOnly.MinValue,
            DateOnly.MaxValue,
            DateOnly.MinValue,
            DateOnly.MaxValue,
            null,
            false,
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