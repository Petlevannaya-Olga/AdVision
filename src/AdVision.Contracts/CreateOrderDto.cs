namespace AdVision.Contracts;

public sealed record CreateOrderDto(
    Guid ContractId,
    List<CreateOrderItemDto> Items);