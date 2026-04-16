namespace AdVision.Contracts;

public sealed record CreateDiscountDto(string Name, double Percent, decimal MinTotal);