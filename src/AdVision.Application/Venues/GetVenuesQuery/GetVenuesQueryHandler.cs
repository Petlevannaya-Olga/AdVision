using AdVision.Contracts;
using AdVision.Domain.Venues;
using AdVision.Infrastructure;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Venues.GetVenuesQuery;

public sealed class GetVenuesQueryHandler(IVenueRepository repository)
    : IQueryHandler<PagedResult<VenueDto>, GetVenuesQuery>
{
    public async Task<Result<PagedResult<VenueDto>, Errors>> Handle(
        GetVenuesQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(
            query.Page,
            query.Size,
            query.Filter,
            query.OrderBy,
            query.OrderByDescending,
            cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }

        var paged = result.Value;

        var items = paged.Items
            .Select(Map)
            .ToList();

        return new PagedResult<VenueDto>(
            items,
            paged.Page,
            paged.PageSize,
            paged.TotalCount);
    }

    private static VenueDto Map(Venue venue)
    {
        return new VenueDto(
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
    }
}