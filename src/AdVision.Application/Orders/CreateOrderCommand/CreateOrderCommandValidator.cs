using AdVision.Domain;
using FluentValidation;

namespace AdVision.Application.Orders.CreateOrderCommand;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Dto.ContractId)
            .NotEmpty();

        RuleFor(x => x.Dto.Items)
            .NotNull()
            .Must(x => x.Count > 0)
            .WithMessage("Заказ должен содержать хотя бы одну позицию");

        RuleForEach(x => x.Dto.Items)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.TariffId)
                    .NotEmpty();

                item.RuleFor(x => x)
                    .Must(x => DateInterval.Create(x.StartDate, x.EndDate).IsSuccess)
                    .WithMessage("Дата начала не может быть больше даты окончания");
            });
    }
}