using System.Diagnostics.CodeAnalysis;
using AdVision.Domain.VenueTypes;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.VenueTypes.CreateVenueTypeCommand;

public sealed class CreateVenueTypeCommandHandler(
    IVenueTypeRepository venueTypeRepository,
    ILogger<CreateVenueTypeCommandHandler> logger) : ICommandHandler<Guid, CreateVenueTypeCommand>
{
    public async Task<Result<Guid, Errors>> Handle(CreateVenueTypeCommand command, CancellationToken cancellationToken)
    {
        var venueTypeId = new VenueTypeId(Guid.NewGuid());

        var nameResult = VenueTypeName.Create(command.Dto.Name);

        var getResult = await venueTypeRepository
            .GetByAsync(x => x.Name == nameResult.Value, cancellationToken);

        if (getResult.IsFailure)
        {
            return getResult.Error.ToErrors();
        }

        if (getResult.Value is not null)
        {
            var errors = CreateVenueTypeCommandHandlerErrors.VenueTypeNameConflict(command.Dto.Name).ToErrors();
            logger.LogError(
                "Нельзя добавить тип площадки с названием '{VenueTypeName}', т.к. она уже существует",
                command.Dto.Name);
            return errors;
        }

        var newVenueType = new VenueType(nameResult.Value);

        var addResult = await venueTypeRepository.AddAsync(newVenueType, cancellationToken);

        if (addResult.IsFailure)
        {
            return addResult.Error.ToErrors();
        }

        logger.LogInformation("Создан новый тип площадки с id = {VenueTypeId}", venueTypeId);

        return venueTypeId.Value;
    }
    
    [ExcludeFromCodeCoverage]
    private static class CreateVenueTypeCommandHandlerErrors
    {
        public static Error VenueTypeNameConflict(string name)
        {
            return CommonErrors.Conflict(
                "venue.type.name.conflict",
                $"Тип площадки {name} уже существует");
        }
    }
}