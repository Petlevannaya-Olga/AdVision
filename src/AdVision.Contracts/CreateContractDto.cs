namespace AdVision.Contracts;

public record CreateContractDto(
    string Number,
    Guid CustomerId,
    Guid EmployeeId,
    DateOnly StartDate,
    DateOnly EndDate,
    ContractStatusDto Status,
    DateOnly? SignedDate);