using AdVision.Contracts;
using AdVision.Domain;
using AdVision.Domain.Contracts;
using FluentValidation;
using Shared.Extensions;

namespace AdVision.Application.Contracts.CreateContractCommand;

public sealed class CreateContractCommandValidator : AbstractValidator<CreateContractCommand>
{
    public CreateContractCommandValidator()
    {
        RuleFor(x => x.Dto.Number)
            .MustBeValueObject(ContractNumber.Create);

        RuleFor(x => x.Dto.CustomerId)
            .NotEmpty();

        RuleFor(x => x.Dto.EmployeeId)
            .NotEmpty();

        RuleFor(x => x.Dto)
            .Must(x => DateInterval.Create(x.StartDate, x.EndDate).IsSuccess)
            .WithMessage("Дата начала не может быть больше даты окончания");

        RuleFor(x => x.Dto.Status)
            .IsInEnum();

        RuleFor(x => x.Dto)
            .Must(x => BeValidSignedDate(x.Status, x.SignedDate))
            .WithMessage("Для подписанного договора необходимо указать дату подписания");
    }

    private static bool BeValidSignedDate(ContractStatusDto status, DateOnly? signedDate)
    {
        if (status == ContractStatusDto.Signed)
        {
            return signedDate.HasValue;
        }

        return true;
    }
}