using FluentValidation;

namespace AdVision.Application.CustomerDiscounts.AssignDiscountToCustomerCommand;

public sealed class AssignDiscountToCustomerCommandValidator
    : AbstractValidator<AssignDiscountToCustomerCommand>
{
    public AssignDiscountToCustomerCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty();

        RuleFor(x => x.DiscountId)
            .NotEmpty();
    }
}