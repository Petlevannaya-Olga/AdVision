using AdVision.Application.Repositories;
using AdVision.Application.SharedErrors;
using AdVision.Domain.CustomerDiscounts;
using AdVision.Domain.Customers;
using AdVision.Domain.Discounts;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.CustomerDiscounts.AssignDiscountToCustomerCommand;

public sealed class AssignDiscountToCustomerCommandHandler(
    ICustomerDiscountRepository customerDiscountRepository,
    ICustomerRepository customerRepository,
    IDiscountRepository discountRepository,
    ILogger<AssignDiscountToCustomerCommandHandler> logger)
    : ICommandHandler<Guid, AssignDiscountToCustomerCommand>
{
    public async Task<Result<Guid, Errors>> Handle(
        AssignDiscountToCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var customerId = new CustomerId(command.CustomerId);
        var discountId = new DiscountId(command.DiscountId);

        var customerResult = await customerRepository.GetByAsync(
            x => x.Id == customerId,
            cancellationToken);

        if (customerResult.IsFailure)
        {
            return customerResult.Error.ToErrors();
        }

        if (customerResult.Value is null)
        {
            var errors = CustomerErrors.NotFound(command.CustomerId).ToErrors();

            logger.LogError(
                "Нельзя назначить скидку. Клиент с id '{CustomerId}' не найден",
                command.CustomerId);

            return errors;
        }

        var discountResult = await discountRepository.GetByAsync(
            x => x.Id == discountId,
            cancellationToken);

        if (discountResult.IsFailure)
        {
            return discountResult.Error.ToErrors();
        }

        if (discountResult.Value is null)
        {
            var errors = DiscountErrors.NotFound(command.DiscountId).ToErrors();

            logger.LogError(
                "Нельзя назначить скидку. Скидка с id '{DiscountId}' не найдена",
                command.DiscountId);

            return errors;
        }

        var existingLinkResult = await customerDiscountRepository.GetByAsync(
            x => x.CustomerId == customerId && x.DiscountId == discountId,
            cancellationToken);

        if (existingLinkResult.IsFailure)
        {
            return existingLinkResult.Error.ToErrors();
        }

        if (existingLinkResult.Value is not null)
        {
            var errors = CustomerDiscountErrors.AlreadyAssigned(command.CustomerId, command.DiscountId).ToErrors();

            logger.LogError(
                "Скидка с id '{DiscountId}' уже назначена клиенту с id '{CustomerId}'",
                command.DiscountId,
                command.CustomerId);

            return errors;
        }

        var customerDiscount = new CustomerDiscount(customerId, discountId);

        var addResult = await customerDiscountRepository.AddAsync(customerDiscount, cancellationToken);

        if (addResult.IsFailure)
        {
            return addResult.Error.ToErrors();
        }

        logger.LogInformation(
            "Клиенту с id = {CustomerId} назначена скидка с id = {DiscountId}",
            command.CustomerId,
            command.DiscountId);

        return customerDiscount.Id.Value;
    }
}