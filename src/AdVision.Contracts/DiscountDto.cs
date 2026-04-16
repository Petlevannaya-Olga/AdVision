namespace AdVision.Contracts;

public sealed record DiscountDto(Guid Id, string Name, double Percent, decimal MinTotal);