namespace AdVision.Contracts;

public record ContractDateBoundsDto(
    DateOnly StartDateMin,
    DateOnly StartDateMax,
    DateOnly EndDateMin,
    DateOnly EndDateMax,
    DateOnly? SignedDateMin,
    DateOnly? SignedDateMax);