namespace AdVision.Contracts;

public sealed record CreateDiscountDto(string Name, decimal Percent, decimal MinTotal);