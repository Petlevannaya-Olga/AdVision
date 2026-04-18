using AdVision.Application.Repositories;
using AdVision.Application.SharedErrors;
using AdVision.Domain.Discounts;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Discounts.CreateDiscountCommand;

public sealed class CreateDiscountCommandHandler(
    IDiscountRepository discountRepository,
    ILogger<CreateDiscountCommandHandler> logger) : ICommandHandler<Guid, CreateDiscountCommand>
{
    public async Task<Result<Guid, Errors>> Handle(CreateDiscountCommand command, CancellationToken cancellationToken)
    {
        var nameResult = DiscountName.Create(command.Dto.Name);
        if (nameResult.IsFailure)
        {
            logger.LogError("Ошибка при создании новой скидки: {Errors}", nameResult.Error.ToErrors());
            return nameResult.Error.ToErrors();
        }

        var percentResult = DiscountPercent.Create(command.Dto.Percent);
        if (percentResult.IsFailure)
        {
            logger.LogError("Ошибка при создании новой скидки: {Errors}", percentResult.Error.ToErrors());
            return percentResult.Error.ToErrors();
        }

        var minTotalResult = DiscountMinTotal.Create(command.Dto.MinTotal);
        if (minTotalResult.IsFailure)
        {
            logger.LogError("Ошибка при создании новой скидки: {Errors}", minTotalResult.Error.ToErrors());
            return minTotalResult.Error.ToErrors();
        }

        var getResult = await discountRepository
            .GetByAsync(x => x.Name == nameResult.Value, cancellationToken);

        if (getResult.IsFailure)
        {
            return getResult.Error.ToErrors();
        }

        if (getResult.Value is not null)
        {
            var errors = DiscountErrors.DiscountNameConflict(command.Dto.Name).ToErrors();

            logger.LogError(
                "Нельзя добавить скидку с названием '{DiscountName}', т.к. она уже существует",
                command.Dto.Name);

            return errors;
        }

        var newDiscount = new Discount(
            nameResult.Value,
            percentResult.Value,
            minTotalResult.Value);

        var addResult = await discountRepository.AddAsync(newDiscount, cancellationToken);

        if (addResult.IsFailure)
        {
            return addResult.Error.ToErrors();
        }

        logger.LogInformation("Создана новая скидка с id = {DiscountId}", newDiscount.Id.Value);

        return newDiscount.Id.Value;
    }
}