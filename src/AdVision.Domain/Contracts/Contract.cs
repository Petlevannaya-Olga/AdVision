using AdVision.Domain.Customers;
using AdVision.Domain.Employees;

namespace AdVision.Domain.Contracts;

public sealed class Contract
{
    /// <summary>
    /// Идентификатор, PK
    /// </summary>
    public ContractId Id { get; private set; }

    /// <summary>
    /// Номер договора
    /// </summary>
    public ContractNumber Number { get; private set; }

    /// <summary>
    /// Идентификатор заказчика
    /// </summary>
    public CustomerId CustomerId { get; private set; }

    /// <summary>
    /// Идентификатор сотрудника
    /// </summary>
    public EmployeeId EmployeeId { get; private set; }

    /// <summary>
    /// Период действия договора
    /// </summary>
    public DateInterval DateInterval { get; private set; }

    /// <summary>
    /// Статус договора
    /// </summary>
    public ContractStatus Status { get; private set; }

    /// <summary>
    /// Дата подписания
    /// </summary>
    public DateOnly? SignedDate { get; private set; }

    public Customer Customer { get; private set; } = null!;
    public Employee Employee { get; private set; } = null!;

    public Contract(
        ContractNumber number,
        CustomerId customerId,
        EmployeeId employeeId,
        DateInterval dateInterval,
        ContractStatus status,
        DateOnly? signedDate)
    {
        Id = new ContractId(Guid.NewGuid());
        Number = number;
        CustomerId = customerId;
        EmployeeId = employeeId;
        DateInterval = dateInterval;
        Status = status;
        SignedDate = signedDate;
    }

    // EF Core
    private Contract()
    {
    }
}