using AdVision.Contracts;
using Shared.Abstractions;

namespace AdVision.Application.Orders.GetOrdersQuery;

public sealed record GetOrdersQuery(
    int Page,
    int PageSize,
    Guid? CustomerId,
    Guid? EmployeeId,
    OrderStatusDto? Status,
    DateOnly StartDateFrom,
    DateOnly StartDateTo,
    DateOnly EndDateFrom,
    DateOnly EndDateTo,
    string? OrderBy,
    bool Descending
    ) : IQuery;