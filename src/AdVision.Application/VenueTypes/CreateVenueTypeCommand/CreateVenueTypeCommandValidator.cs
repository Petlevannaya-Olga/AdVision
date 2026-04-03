using AdVision.Domain.VenueTypes;
using FluentValidation;
using Shared.Extensions;

namespace AdVision.Application.VenueTypes.CreateVenueTypeCommand;

public class CreateVenueTypeCommandValidator : AbstractValidator<CreateVenueTypeCommand>
{
    public CreateVenueTypeCommandValidator()
    {
        RuleFor(x => x.Dto.Name)
            .MustBeValueObject(VenueTypeName.Create);
    }
}