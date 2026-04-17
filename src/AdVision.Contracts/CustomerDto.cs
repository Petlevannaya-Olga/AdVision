namespace AdVision.Contracts;

public record CustomerDto(
    Guid Id,
    string LastName,
    string FirstName,
    string MiddleName,
    string PhoneNumber);