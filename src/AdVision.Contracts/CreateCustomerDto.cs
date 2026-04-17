namespace AdVision.Contracts;

public record CreateCustomerDto(
    string LastName,
    string FirstName,
    string MiddleName,
    string PhoneNumber);