namespace AdVision.Contracts;

public sealed record DiscountDto(Guid Id, string Name, decimal Percent, decimal MinTotal);