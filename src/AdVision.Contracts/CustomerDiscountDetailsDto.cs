namespace AdVision.Contracts;

public record CustomerDiscountDetailsDto(
    Guid CustomerDiscountId,
    Guid CustomerId,
    Guid DiscountId,
    string DiscountName,
    decimal Percent,
    decimal MinTotal);