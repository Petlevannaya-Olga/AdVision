using AdVision.Contracts;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Venues.GetVenuesQuery;

public sealed class GetVenuesQueryHandler(IVenueRepository repository)
    : IQueryHandler<IReadOnlyList<VenueDto>, GetVenuesQuery>
{
    public async Task<Result<IReadOnlyList<VenueDto>, Errors>> Handle(
        GetVenuesQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(
            query.Page,
            query.Size,
            query.Filter,
            cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }

        return result.Value
            .Select(v => new VenueDto(
                v.Name.Value,
                v.Type.Name.Value,
                v.Address.Region,
                v.Address.District,
                v.Address.City,
                v.Address.Street,
                v.Address.House,
                v.Address.Latitude,
                v.Address.Longitude,
                v.Size.Width,
                v.Size.Height,
                v.Rating.Value,
                v.Description.Value))
            .ToList();
    }
}