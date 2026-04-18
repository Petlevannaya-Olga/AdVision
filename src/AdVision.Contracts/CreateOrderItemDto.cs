namespace AdVision.Contracts;

public sealed record CreateOrderItemDto(
    Guid TariffId,
    DateOnly StartDate,
    DateOnly EndDate);