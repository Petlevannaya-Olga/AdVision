using AdVision.Application.Repositories;
using AdVision.Application.SharedErrors;
using AdVision.Contracts;
using AdVision.Domain;
using AdVision.Domain.Contracts;
using AdVision.Domain.Customers;
using AdVision.Domain.Employees;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Contracts.CreateContractCommand;

public sealed class CreateContractCommandHandler(
    IContractRepository contractRepository,
    ICustomerRepository customerRepository,
    IEmployeeRepository employeeRepository,
    ILogger<CreateContractCommandHandler> logger)
    : ICommandHandler<Guid, CreateContractCommand>
{
    public async Task<Result<Guid, Errors>> Handle(CreateContractCommand command, CancellationToken cancellationToken)
    {
        var numberResult = ContractNumber.Create(command.Dto.Number);

        if (numberResult.IsFailure)
        {
            logger.LogError("Ошибка при создании нового договора: {Errors}", numberResult.Error.ToErrors());
            return numberResult.Error.ToErrors();
        }

        var dateIntervalResult = DateInterval.Create(
            command.Dto.StartDate,
            command.Dto.EndDate);

        if (dateIntervalResult.IsFailure)
        {
            logger.LogError("Ошибка при создании нового договора: {Errors}", dateIntervalResult.Error.ToErrors());
            return dateIntervalResult.Error.ToErrors();
        }

        var customerId = new CustomerId(command.Dto.CustomerId);
        var employeeId = new EmployeeId(command.Dto.EmployeeId);

        var customerResult = await customerRepository.GetByAsync(
            x => x.Id == customerId,
            cancellationToken);

        if (customerResult.IsFailure)
        {
            return customerResult.Error.ToErrors();
        }

        if (customerResult.Value is null)
        {
            var errors = CustomerErrors.NotFound(command.Dto.CustomerId).ToErrors();

            logger.LogError(
                "Нельзя создать договор. Заказчик с id '{CustomerId}' не найден",
                command.Dto.CustomerId);

            return errors;
        }

        var employeeResult = await employeeRepository.GetByAsync(
            x => x.Id == employeeId,
            cancellationToken);

        if (employeeResult.IsFailure)
        {
            return employeeResult.Error.ToErrors();
        }

        if (employeeResult.Value is null)
        {
            var errors = EmployeeErrors.NotFound(command.Dto.EmployeeId).ToErrors();

            logger.LogError(
                "Нельзя создать договор. Сотрудник с id '{EmployeeId}' не найден",
                command.Dto.EmployeeId);

            return errors;
        }

        var existingContractResult = await contractRepository.GetByAsync(
            x => x.Number == numberResult.Value,
            cancellationToken);

        if (existingContractResult.IsFailure)
        {
            return existingContractResult.Error.ToErrors();
        }

        if (existingContractResult.Value is not null)
        {
            var errors = ContractErrors.ContractNumberConflict(command.Dto.Number).ToErrors();

            logger.LogError(
                "Нельзя создать договор с номером '{ContractNumber}', т.к. он уже существует",
                command.Dto.Number);

            return errors;
        }

        var status = MapStatus(command.Dto.Status);

        var contract = new Contract(
            numberResult.Value,
            customerId,
            employeeId,
            dateIntervalResult.Value,
            status,
            command.Dto.SignedDate);

        var addResult = await contractRepository.AddAsync(contract, cancellationToken);

        if (addResult.IsFailure)
        {
            return addResult.Error.ToErrors();
        }

        logger.LogInformation("Создан новый договор с id = {ContractId}", contract.Id.Value);

        return contract.Id.Value;
    }

    private static ContractStatus MapStatus(ContractStatusDto status)
    {
        return status switch
        {
            ContractStatusDto.Draft => ContractStatus.Draft,
            ContractStatusDto.Active => ContractStatus.Active,
            ContractStatusDto.Signed => ContractStatus.Signed,
            ContractStatusDto.Completed => ContractStatus.Completed,
            ContractStatusDto.Cancelled => ContractStatus.Cancelled,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}