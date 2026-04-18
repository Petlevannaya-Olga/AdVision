using AdVision.Application.Repositories;
using AdVision.Application.SharedErrors;
using AdVision.Contracts;
using AdVision.Domain;
using AdVision.Domain.Contracts;
using AdVision.Domain.Orders;
using AdVision.Domain.Tariffs;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Orders.CreateOrderCommand;

public sealed class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IContractRepository contractRepository,
    ITariffRepository tariffRepository,
    ICustomerDiscountRepository customerDiscountRepository,
    ILogger<CreateOrderCommandHandler> logger)
    : ICommandHandler<Guid, CreateOrderCommand>
{
    public async Task<Result<Guid, Errors>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var contractId = new ContractId(command.Dto.ContractId);

        var contractResult = await contractRepository.GetByAsync(
            x => x.Id == contractId,
            cancellationToken);

        if (contractResult.IsFailure)
        {
            return contractResult.Error.ToErrors();
        }

        if (contractResult.Value is null)
        {
            var errors = ContractErrors.NotFound(command.Dto.ContractId).ToErrors();

            logger.LogError(
                "Нельзя создать заказ. Договор с id '{ContractId}' не найден",
                command.Dto.ContractId);

            return errors;
        }

        var contract = contractResult.Value;

        var orderItems = new List<(TariffId tariffId, Money price, DateInterval period)>();

        foreach (var itemDto in command.Dto.Items)
        {
            var tariffId = new TariffId(itemDto.TariffId);

            var tariffResult = await tariffRepository.GetByAsync(
                x => x.Id == tariffId,
                cancellationToken);

            if (tariffResult.IsFailure)
            {
                return tariffResult.Error.ToErrors();
            }

            if (tariffResult.Value is null)
            {
                var errors = TariffErrors.NotFound(itemDto.TariffId).ToErrors();

                logger.LogError(
                    "Нельзя создать заказ. Тариф с id '{TariffId}' не найден",
                    itemDto.TariffId);

                return errors;
            }

            var tariff = tariffResult.Value;

            var periodResult = DateInterval.Create(
                itemDto.StartDate,
                itemDto.EndDate);

            if (periodResult.IsFailure)
            {
                logger.LogError(
                    "Ошибка при создании позиции заказа: {Errors}",
                    periodResult.Error.ToErrors());

                return periodResult.Error.ToErrors();
            }

            orderItems.Add((
                tariff.Id,
                tariff.Price,
                periodResult.Value));
        }

        var totalAmount = Money.Zero();

        foreach (var item in orderItems)
        {
            totalAmount = totalAmount.Add(item.price);
        }

        var customerDiscountsResult = await customerDiscountRepository.GetByCustomerIdAsync(
            contract.CustomerId,
            cancellationToken);

        if (customerDiscountsResult.IsFailure)
        {
            return customerDiscountsResult.Error.ToErrors();
        }

        var maxDiscountPercent = customerDiscountsResult.Value
            .Where(x => totalAmount.Value >= x.Discount.MinTotal.Value)
            .Select(x => x.Discount.Percent.Value)
            .DefaultIfEmpty(0)
            .Max();

        var order = new Order(
            contract.Id,
            orderItems,
            maxDiscountPercent);

        var addResult = await orderRepository.AddAsync(order, cancellationToken);

        if (addResult.IsFailure)
        {
            return addResult.Error.ToErrors();
        }

        logger.LogInformation("Создан новый заказ с id = {OrderId}", order.Id.Value);

        return order.Id.Value;
    }
}