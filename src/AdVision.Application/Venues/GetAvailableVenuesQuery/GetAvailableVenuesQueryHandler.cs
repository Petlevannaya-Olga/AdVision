using AdVision.Application.Repositories;
using AdVision.Contracts;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Venues.GetAvailableVenuesQuery;

public sealed class GetAvailableVenuesQueryHandler(
    IVenueRepository venueRepository)
    : IQueryHandler<PagedResult<AvailableVenueDto>, GetAvailableVenuesQuery>
{
    public async Task<Result<PagedResult<AvailableVenueDto>, Errors>> Handle(
        Venues.GetAvailableVenuesQuery.GetAvailableVenuesQuery query,
        CancellationToken cancellationToken)
    {
        var result = await venueRepository.GetAvailableAsync(
            query.Page,
            query.PageSize,
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

        return Result.Success<PagedResult<AvailableVenueDto>, Errors>(result.Value);
    }
}