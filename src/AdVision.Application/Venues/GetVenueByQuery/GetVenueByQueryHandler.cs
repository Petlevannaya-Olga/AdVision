using AdVision.Application.Repositories;
using AdVision.Contracts;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Venues.GetVenueByQuery;

public sealed class GetVenueByQueryHandler(IVenueRepository repository) : IQueryHandler<VenueDto, GetVenueByQuery>
{
    public async Task<Result<VenueDto, Errors>> Handle(GetVenueByQuery query, CancellationToken cancellationToken)
    {
        var result = await repository.GetByAsync(query.Expression, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }
        
        var venue = result.Value;

        if (venue is null)
        {
            return CommonErrors
                .NotFound("venue.was.not.found", "Площадка не найдена")
                .ToErrors();
        }

        var venueDto = new VenueDto(
            venue.Id.Value,
            venue.Name.Value, 
            venue.Type.Name.Value,
            venue.Address.Region,
            venue.Address.District,
            venue.Address.City,
            venue.Address.Street,
            venue.Address.House,
            venue.Address.Latitude,
            venue.Address.Longitude,
            venue.Size.Width,
            venue.Size.Height,
            venue.Rating.Value,
            venue.Description.Value);

        return venueDto;
    }
}