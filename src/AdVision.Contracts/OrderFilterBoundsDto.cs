namespace AdVision.Contracts;

public sealed record OrderFilterBoundsDto(
    decimal TotalAmountMin,
    decimal TotalAmountMax,
    DateOnly? StartDateMin,
    DateOnly? StartDateMax,
    DateOnly? EndDateMin,
    DateOnly? EndDateMax);