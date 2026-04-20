namespace AdVision.Contracts;

public sealed record EmployeeOrderDto(
    Guid Id,
    string LastName,
    string FirstName,
    string MiddleName)
{
    public string FullName => $"{LastName} {FirstName} {MiddleName}";
}