using AdVision.Application.Repositories;
using AdVision.Contracts;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.OrderItems.GetOrderItemsByIdQuery;

public sealed class GetOrderItemsByOrderIdQueryHandler(
    IOrderItemRepository orderItemRepository)
    : IQueryHandler<IReadOnlyList<OrderItemDto>, GetOrderItemsByOrderIdQuery>
{
    public async Task<Result<IReadOnlyList<OrderItemDto>, Errors>> Handle(
        GetOrderItemsByOrderIdQuery query,
        CancellationToken cancellationToken)
    {
        var result = await orderItemRepository.GetByOrderIdAsync(
            query.OrderId,
            cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }

        return Result.Success<IReadOnlyList<OrderItemDto>, Errors>(result.Value);
    }
}