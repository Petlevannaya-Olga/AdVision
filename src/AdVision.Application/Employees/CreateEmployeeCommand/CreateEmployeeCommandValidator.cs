using AdVision.Domain.Employees;
using FluentValidation;
using Shared.Extensions;

namespace AdVision.Application.Employees.CreateEmployeeCommand;

public sealed class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Dto.LastName)
            .MustBeValueObject(PersonName.Create);

        RuleFor(x => x.Dto.FirstName)
            .MustBeValueObject(PersonName.Create);

        RuleFor(x => x.Dto.MiddleName)
            .MustBeValueObject(PersonName.Create);

        RuleFor(x => x.Dto.Address)
            .MustBeValueObject(EmployeeAddress.Create);

        RuleFor(x => x.Dto.PassportSeries)
            .MustBeValueObject(PassportSeries.Create);

        RuleFor(x => x.Dto.PassportNumber)
            .MustBeValueObject(PassportNumber.Create);

        RuleFor(x => x.Dto.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);

        RuleFor(x => x.Dto.PositionId)
            .NotEmpty()
            .WithMessage("Необходимо выбрать должность");
    }
}