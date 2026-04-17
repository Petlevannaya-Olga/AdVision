using Shared.Abstractions;

namespace AdVision.Application.CustomerDiscounts.AssignDiscountToCustomerCommand;

public sealed record AssignDiscountToCustomerCommand(
    Guid CustomerId,
    Guid DiscountId) : IValidation;