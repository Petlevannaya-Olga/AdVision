namespace AdVision.Domain.Customers;

public sealed class Customer
{
    public CustomerId Id { get; private set; }

    public PersonName LastName { get; private set; }
    public PersonName FirstName { get; private set; }
    public PersonName MiddleName { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }

    public Customer(
        PersonName lastName,
        PersonName firstName,
        PersonName middleName,
        PhoneNumber phoneNumber
        )
    {
        Id = new CustomerId(Guid.NewGuid());
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        PhoneNumber = phoneNumber;
    }

    // EF Core
    private Customer()
    {
    }
}