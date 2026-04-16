using AdVision.Domain.Discounts;
using FluentValidation;
using Shared.Extensions;

namespace AdVision.Application.Discounts.CreateDiscountCommand;

public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
{
    public CreateDiscountCommandValidator()
    {
        RuleFor(x => x.Dto.Name)
            .MustBeValueObject(DiscountName.Create);

        RuleFor(x => x.Dto.Percent)
            .MustBeValueObject(DiscountPercent.Create);

        RuleFor(x => x.Dto.MinTotal)
            .MustBeValueObject(DiscountMinTotal.Create);
    }
}