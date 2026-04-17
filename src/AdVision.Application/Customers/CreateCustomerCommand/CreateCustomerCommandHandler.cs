using AdVision.Application.SharedErrors;
using AdVision.Domain;
using AdVision.Domain.Customers;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Customers.CreateCustomerCommand;

public sealed class CreateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    ILogger<CreateCustomerCommandHandler> logger)
    : ICommandHandler<Guid, CreateCustomerCommand>
{
    public async Task<Result<Guid, Errors>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var lastNameResult = PersonName.Create(command.Dto.LastName);

        if (lastNameResult.IsFailure)
        {
            logger.LogError("Ошибка при создании нового клиента: {Errors}", lastNameResult.Error.ToErrors());
            return lastNameResult.Error.ToErrors();
        }

        var firstNameResult = PersonName.Create(command.Dto.FirstName);

        if (firstNameResult.IsFailure)
        {
            logger.LogError("Ошибка при создании нового клиента: {Errors}", firstNameResult.Error.ToErrors());
            return firstNameResult.Error.ToErrors();
        }

        var middleNameResult = PersonName.Create(command.Dto.MiddleName);

        if (middleNameResult.IsFailure)
        {
            logger.LogError("Ошибка при создании нового клиента: {Errors}", middleNameResult.Error.ToErrors());
            return middleNameResult.Error.ToErrors();
        }

        var phoneNumberResult = PhoneNumber.Create(command.Dto.PhoneNumber);

        if (phoneNumberResult.IsFailure)
        {
            logger.LogError("Ошибка при создании нового клиента: {Errors}", phoneNumberResult.Error.ToErrors());
            return phoneNumberResult.Error.ToErrors();
        }

        var existingCustomerResult = await customerRepository.GetByAsync(
            x => x.PhoneNumber.Value == phoneNumberResult.Value.Value,
            cancellationToken);

        if (existingCustomerResult.IsFailure)
        {
            return existingCustomerResult.Error.ToErrors();
        }

        if (existingCustomerResult.Value is not null)
        {
            var errors = CustomerErrors.CustomerPhoneConflict(command.Dto.PhoneNumber).ToErrors();

            logger.LogError(
                "Нельзя добавить клиента с телефоном '{PhoneNumber}', т.к. он уже существует",
                command.Dto.PhoneNumber);

            return errors;
        }

        var customer = new Customer(
            lastNameResult.Value,
            firstNameResult.Value,
            middleNameResult.Value,
            phoneNumberResult.Value);

        var addResult = await customerRepository.AddAsync(customer, cancellationToken);

        if (addResult.IsFailure)
        {
            return addResult.Error.ToErrors();
        }

        logger.LogInformation("Создан новый клиент с id = {CustomerId}", customer.Id.Value);

        return customer.Id.Value;
    }
}