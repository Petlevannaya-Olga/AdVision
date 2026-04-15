using AdVision.Domain.Positions;
using FluentValidation;
using Shared.Extensions;

namespace AdVision.Application.Positions.CreatePositionCommand;

public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
{
    public CreatePositionCommandValidator()
    {
        RuleFor(x => x.Dto.Name)
            .MustBeValueObject(PositionName.Create);
    }
}