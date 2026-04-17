using AdVision.Application.SharedErrors;
using AdVision.Domain;
using AdVision.Domain.Employees;
using AdVision.Domain.Positions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Employees.CreateEmployeeCommand;

public sealed class CreateEmployeeCommandHandler(
    IEmployeeRepository employeeRepository,
    IPositionRepository positionRepository,
    ILogger<CreateEmployeeCommandHandler> logger) : ICommandHandler<Guid, CreateEmployeeCommand>
{
    public async Task<Result<Guid, Errors>> Handle(
        CreateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        var lastNameResult = PersonName.Create(command.Dto.LastName);
        if (lastNameResult.IsFailure)
        {
            logger.LogError("Ошибка при создании сотрудника: {Errors}", lastNameResult.Error.ToErrors());
            return lastNameResult.Error.ToErrors();
        }

        var firstNameResult = PersonName.Create(command.Dto.FirstName);
        if (firstNameResult.IsFailure)
        {
            logger.LogError("Ошибка при создании сотрудника: {Errors}", firstNameResult.Error.ToErrors());
            return firstNameResult.Error.ToErrors();
        }

        var middleNameResult = PersonName.Create(command.Dto.MiddleName);
        if (middleNameResult.IsFailure)
        {
            logger.LogError("Ошибка при создании сотрудника: {Errors}", middleNameResult.Error.ToErrors());
            return middleNameResult.Error.ToErrors();
        }

        var addressResult = EmployeeAddress.Create(command.Dto.Address);
        if (addressResult.IsFailure)
        {
            logger.LogError("Ошибка при создании сотрудника: {Errors}", addressResult.Error.ToErrors());
            return addressResult.Error.ToErrors();
        }

        var passportResult = Passport.Create(
            command.Dto.PassportSeries,
            command.Dto.PassportNumber);

        if (passportResult.IsFailure)
        {
            logger.LogError("Ошибка при создании сотрудника: {Errors}", passportResult.Error.ToErrors());
            return passportResult.Error.ToErrors();
        }

        var phoneNumberResult = PhoneNumber.Create(command.Dto.PhoneNumber);
        if (phoneNumberResult.IsFailure)
        {
            logger.LogError("Ошибка при создании сотрудника: {Errors}", phoneNumberResult.Error.ToErrors());
            return phoneNumberResult.Error.ToErrors();
        }

        var positionId = new PositionId(command.Dto.PositionId);

        var positionResult = await positionRepository.GetByAsync(
            x => x.Id == positionId,
            cancellationToken);

        if (positionResult.IsFailure)
        {
            return positionResult.Error.ToErrors();
        }

        if (positionResult.Value is null)
        {
            var error = CommonErrors.NotFound(
                "position.not.found",
                "Указанная должность не найдена");

            logger.LogError("Не найдена должность с id = {PositionId}", command.Dto.PositionId);
            return error.ToErrors();
        }

        var phone = phoneNumberResult.Value.Value;
        var passportSeries = passportResult.Value.Series;
        var passportNumber = passportResult.Value.Number;

        var phoneConflictResult = await employeeRepository.GetByAsync(
            x => x.PhoneNumber.Value == phone,
            cancellationToken);

        if (phoneConflictResult.IsFailure)
        {
            return phoneConflictResult.Error.ToErrors();
        }

        if (phoneConflictResult.Value is not null)
        {
            var errors = EmployeeErrors.PhoneNumberConflict(command.Dto.PhoneNumber).ToErrors();

            logger.LogError(
                "Нельзя добавить сотрудника с номером телефона '{PhoneNumber}', т.к. он уже существует",
                command.Dto.PhoneNumber);

            return errors;
        }

        var passportConflictResult = await employeeRepository.GetByAsync(
            x => x.Passport.Series == passportSeries &&
                 x.Passport.Number == passportNumber,
            cancellationToken);

        if (passportConflictResult.IsFailure)
        {
            return passportConflictResult.Error.ToErrors();
        }

        if (passportConflictResult.Value is not null)
        {
            var errors = EmployeeErrors.PassportConflict(
                command.Dto.PassportSeries,
                command.Dto.PassportNumber).ToErrors();

            logger.LogError(
                "Нельзя добавить сотрудника с паспортом '{PassportSeries} {PassportNumber}', т.к. он уже существует",
                command.Dto.PassportSeries,
                command.Dto.PassportNumber);

            return errors;
        }

        var employee = new Employee(
            lastNameResult.Value,
            firstNameResult.Value,
            middleNameResult.Value,
            addressResult.Value,
            passportResult.Value,
            phoneNumberResult.Value,
            positionId);

        var addResult = await employeeRepository.AddAsync(employee, cancellationToken);

        if (addResult.IsFailure)
        {
            return addResult.Error.ToErrors();
        }

        logger.LogInformation("Создан новый сотрудник с id = {EmployeeId}", employee.Id.Value);

        return employee.Id.Value;
    }
}