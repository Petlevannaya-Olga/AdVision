using AdVision.Domain.Venues;
using AdVision.Domain.VenueTypes;
using FluentValidation;
using Shared.Extensions;

namespace AdVision.Application.Venues.CreateVenueCommand;

public class CreateVenueCommandValidator : AbstractValidator<CreateVenueCommand>
{
    public CreateVenueCommandValidator()
    {
        RuleFor(x => x.Dto.Name)
            .MustBeValueObject(VenueName.Create);

        RuleFor(x => x.Dto.Address)
            .MustBeValueObject(VenueAddress.Create);

        RuleFor(x => x.Dto.Size)
            .MustBeValueObject(x => VenueSize.Create(x.Width, x.Height));

        RuleFor(x => x.Dto.Rating)
            .MustBeValueObject(VenueRating.Create);

        RuleFor(x => x.Dto.Description)
            .MustBeValueObject(VenueDescription.Create);

        RuleFor(x => x.Dto.Type.Id)
            .Must(id => id != Guid.Empty);

        RuleFor(x => x.Dto.Type.Name)
            .MustBeValueObject(VenueTypeName.Create);
    }
}