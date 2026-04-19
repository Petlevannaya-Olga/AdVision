using AdVision.Contracts;
using Shared.Abstractions;

namespace AdVision.Application.Orders.GetOrdersQuery;

public sealed record GetOrdersQuery(
    int Page,
    int PageSize,
    string? OrderId,
    Guid? CustomerId,
    Guid? EmployeeId,
    OrderStatusDto? Status,
    decimal? TotalAmountFrom,
    decimal? TotalAmountTo,
    DateOnly StartDateFrom,
    DateOnly StartDateTo,
    DateOnly EndDateFrom,
    DateOnly EndDateTo,
    string? OrderBy,
    bool Descending) : IQuery;