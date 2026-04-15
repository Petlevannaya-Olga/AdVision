using AdVision.Application.SharedErrors;
using AdVision.Domain.Positions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Positions.CreatePositionCommand;

public sealed class CreatePositionCommandHandler(
    IPositionRepository positionRepository,
    ILogger<CreatePositionCommandHandler> logger) : ICommandHandler<Guid, CreatePositionCommand>
{
    public async Task<Result<Guid, Errors>> Handle(CreatePositionCommand command, CancellationToken cancellationToken)
    {
        var nameResult = PositionName.Create(command.Dto.Name);

        if (nameResult.IsFailure)
        {
            logger.LogError("Ошибка при создании новой позиции: {Errors}", nameResult.Error.ToErrors());
            return nameResult.Error.ToErrors();
        }

        var getResult = await positionRepository
            .GetByAsync(x => x.Name == nameResult.Value, cancellationToken);

        if (getResult.IsFailure)
        {
            return getResult.Error.ToErrors();
        }

        if (getResult.Value is not null)
        {
            var errors = PositionErrors.PositionNameConflict(command.Dto.Name).ToErrors();

            logger.LogError(
                "Нельзя добавить позицию с названием '{PositionName}', т.к. она уже существует",
                command.Dto.Name);

            return errors;
        }

        var newPosition = new Position(nameResult.Value);

        var addResult = await positionRepository.AddAsync(newPosition, cancellationToken);

        if (addResult.IsFailure)
        {
            return addResult.Error.ToErrors();
        }

        logger.LogInformation("Создана новая позиция с id = {PositionId}", newPosition.Id.Value);

        return newPosition.Id.Value;
    }
}