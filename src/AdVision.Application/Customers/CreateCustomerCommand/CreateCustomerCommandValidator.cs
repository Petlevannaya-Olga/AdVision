using AdVision.Domain;
using FluentValidation;
using Shared.Extensions;

namespace AdVision.Application.Customers.CreateCustomerCommand;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Dto.LastName)
            .MustBeValueObject(PersonName.Create);
        
        RuleFor(x => x.Dto.FirstName)
            .MustBeValueObject(PersonName.Create);
        
        RuleFor(x => x.Dto.MiddleName)
            .MustBeValueObject(PersonName.Create);
        
        RuleFor(x => x.Dto.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);
    }
}