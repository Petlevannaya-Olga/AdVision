using AdVision.Domain.Positions;

namespace AdVision.Domain.Employees;

public sealed class Employee
{
    /// <summary>
    /// Идентификатор, PK
    /// </summary>
    public EmployeeId Id { get; private set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public PersonName LastName { get; private set; }

    /// <summary>
    /// Имя
    /// </summary>
    public PersonName FirstName { get; private set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public PersonName MiddleName { get; private set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public EmployeeAddress Address { get; private set; }

    /// <summary>
    /// Паспорт
    /// </summary>
    public Passport Passport { get; private set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public PhoneNumber PhoneNumber { get; private set; }

    /// <summary>
    /// Идентификатор должности
    /// </summary>
    public PositionId PositionId { get; private set; }

    public Employee(
        PersonName lastName,
        PersonName firstName,
        PersonName middleName,
        EmployeeAddress address,
        Passport passport,
        PhoneNumber phoneNumber,
        PositionId positionId)
    {
        Id = new EmployeeId(Guid.NewGuid());
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        Address = address;
        Passport = passport;
        PhoneNumber = phoneNumber;
        PositionId = positionId;
    }

    // EF Core
    private Employee()
    {
    }
}