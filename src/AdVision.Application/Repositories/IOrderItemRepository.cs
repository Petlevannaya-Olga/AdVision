using AdVision.Contracts;
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Application.Repositories;

public interface IOrderItemRepository
{
    Task<Result<IReadOnlyList<OrderItemDto>, Error>> GetByOrderIdAsync(
        Guid orderId,
        CancellationToken cancellationToken);
}