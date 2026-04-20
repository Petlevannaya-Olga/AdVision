namespace AdVision.Contracts;

public sealed record OrderDateBoundsDto(
    DateOnly? StartDateMin,
    DateOnly? StartDateMax,
    DateOnly? EndDateMin,
    DateOnly? EndDateMax);