namespace AdVision.Contracts;

public record EmployeeDto(
    Guid Id,
    string LastName,
    string FirstName,
    string MiddleName,
    string Address,
    string PassportSeries,
    string PassportNumber,
    string PhoneNumber,
    Guid PositionId)
{
    public string FullName => $"{LastName} {FirstName} {MiddleName}";
}