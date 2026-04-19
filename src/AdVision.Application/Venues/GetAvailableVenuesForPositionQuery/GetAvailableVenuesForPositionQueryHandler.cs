using AdVision.Application.Repositories;
using AdVision.Contracts;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Venues.GetAvailableVenuesForPositionQuery;

public sealed class GetAvailableVenuesForPositionQueryHandler(
    IVenueRepository venueRepository)
    : IQueryHandler<IReadOnlyList<AvailableVenueForPositionDto>, GetAvailableVenuesForPositionQuery>
{
    public async Task<Result<IReadOnlyList<AvailableVenueForPositionDto>, Errors>> Handle(
        GetAvailableVenuesForPositionQuery query,
        CancellationToken cancellationToken)
    {
        var result = await venueRepository.GetAvailableForPositionAsync(
            query.Name,
            query.VenueTypeId,
            query.Region,
            query.District,
            query.City,
            query.Street,
            query.RatingFrom,
            query.RatingTo,
            query.PriceFrom,
            query.PriceTo,
            query.DateFrom,
            query.DateTo,
            cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }

        return Result.Success<IReadOnlyList<AvailableVenueForPositionDto>, Errors>(result.Value);
    }
}