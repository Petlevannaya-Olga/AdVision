using Shared.Abstractions;

namespace AdVision.Application.OrderItems.GetOrderItemsByIdQuery;

public sealed record GetOrderItemsByOrderIdQuery(Guid OrderId) : IQuery;