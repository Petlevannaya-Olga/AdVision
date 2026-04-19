namespace AdVision.Contracts;

public sealed record OrderDto(
    Guid Id,
    string ContractNumber,
    string EmployeeName,
    string CustomerName,
    decimal TotalAmount,
    DateOnly? StartDate,
    DateOnly? EndDate,
    OrderStatusDto Status);