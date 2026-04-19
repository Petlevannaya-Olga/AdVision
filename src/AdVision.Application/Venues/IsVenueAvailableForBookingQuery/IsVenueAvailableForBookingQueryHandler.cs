using AdVision.Application.Repositories;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Venues.IsVenueAvailableForBookingQuery;

public sealed class IsVenueAvailableForBookingQueryHandler(IVenueRepository repository): IQueryHandler<bool, IsVenueAvailableForBookingQuery>
{
    public async Task<Result<bool, Errors>> Handle(IsVenueAvailableForBookingQuery query, CancellationToken cancellationToken)
    {
        var result = await repository.IsAvailableForBookingAsync(query.VenueId, query.DateFrom, query.DateTo, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }
        
        return result.Value;
    }
}