namespace AdVision.Contracts;

public record CreateEmployeeDto(
    string LastName,
    string FirstName,
    string MiddleName,
    string Address,
    string PassportSeries,
    string PassportNumber,
    string PhoneNumber,
    Guid PositionId);