namespace AdVision.Contracts;

public record CustomerDiscountDto(
    Guid Id,
    Guid CustomerId,
    Guid DiscountId);