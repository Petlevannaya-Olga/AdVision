namespace AdVision.Contracts;

public record ContractDto(
    Guid Id,
    string Number,
    Guid CustomerId,
    string CustomerFullName,
    Guid EmployeeId,
    string EmployeeFullName,
    DateOnly StartDate,
    DateOnly EndDate,
    ContractStatusDto Status,
    DateOnly? SignedDate);