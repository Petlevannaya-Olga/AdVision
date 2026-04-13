using AdVision.Domain.Venues;
using AdVision.Domain.VenueTypes;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Venues.CreateVenueCommand;

public class CreateVenueCommandHandler(
    IVenueRepository repository,
    ILogger<CreateVenueCommandHandler> logger) : ICommandHandler<Guid, CreateVenueCommand>
{
    public async Task<Result<Guid, Errors>> Handle(CreateVenueCommand command, CancellationToken cancellationToken)
    {
        var venueNameResult = VenueName.Create(command.Dto.Name);

        if (venueNameResult.IsFailure)
        {
            logger.LogError("Ошибка при создании новой площадки: {Errors}", venueNameResult.Error.ToErrors());
            return venueNameResult.Error.ToErrors();
        }

        var existingVenue = await repository.GetByAsync(v =>
                v.Name == venueNameResult.Value,
            cancellationToken);

        if (existingVenue.IsFailure)
        {
            logger.LogError("Не удалось получить площадку: {Error}", existingVenue.Error);
            return existingVenue.Error.ToErrors();
        }

        if (existingVenue.Value is not null)
        {
            logger.LogError(
                "Площадка с названием {VenueName} и координатами {Latitude}, {Longitude} уже существует",
                existingVenue.Value.Name,
                existingVenue.Value.Address.Latitude,
                existingVenue.Value.Address.Longitude);
            return CommonErrors
                .Conflict("venue.is.conflict", "Площадка уже существует")
                .ToErrors();
        }


        var venueTypeId = new VenueTypeId(command.Dto.Type.Id);

        var venueAddressResult = VenueAddress.Create(command.Dto.Address);

        if (venueAddressResult.IsFailure)
        {
            logger.LogError("Ошибка при создании новой площадки: {Errors}", venueAddressResult.Error.ToErrors());
            return venueAddressResult.Error.ToErrors();
        }

        var venueSizeResult = VenueSize.Create(command.Dto.Size.Width, command.Dto.Size.Height);

        if (venueSizeResult.IsFailure)
        {
            logger.LogError("Ошибка при создании новой площадки: {Errors}", venueSizeResult.Error.ToErrors());
            return venueSizeResult.Error.ToErrors();
        }

        var venueRatingResult = VenueRating.Create(command.Dto.Rating);
        if (venueRatingResult.IsFailure)
        {
            logger.LogError("Ошибка при создании новой площадки: {Errors}", venueRatingResult.Error.ToErrors());
            return venueRatingResult.Error.ToErrors();
        }

        var venueDescriptionResult = VenueDescription.Create(command.Dto.Description);
        if (venueDescriptionResult.IsFailure)
        {
            logger.LogError("Ошибка при создании новой площадки: {Errors}", venueDescriptionResult.Error.ToErrors());
            return venueDescriptionResult.Error.ToErrors();
        }

        var venueTypeNameResult = VenueTypeName.Create(command.Dto.Name);
        if (venueTypeNameResult.IsFailure)
        {
            logger.LogError("Ошибка при создании новой площадки: {Errors}", venueTypeNameResult.Error.ToErrors());
            return venueTypeNameResult.Error.ToErrors();
        }

        var newVenue = new Venue(
            name: venueNameResult.Value,
            venueTypeId: venueTypeId,
            address: venueAddressResult.Value,
            size: venueSizeResult.Value,
            rating: venueRatingResult.Value,
            description: venueDescriptionResult.Value
        )
        {
            Type = new VenueType(venueTypeNameResult.Value)
        };

        var result = await repository.AddAsync(newVenue, cancellationToken);

        if (!result.IsFailure)
            return result.Value;

        logger.LogError("Ошибка при создании новой площадки: {Errors}", result.Error.ToErrors());
        return result.Error.ToErrors();
    }
}