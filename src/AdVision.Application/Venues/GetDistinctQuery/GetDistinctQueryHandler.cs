using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Venues.GetDistinctQuery;

public sealed class GetDistinctQueryHandler(IVenueRepository repository)
    : IQueryHandler<IReadOnlyList<string>, GetDistinctQuery>
{
    public async Task<Result<IReadOnlyList<string>, Errors>> Handle(
        GetDistinctQuery query,
        CancellationToken cancellationToken)
    {
        var result = await repository.GetDistinctAsync(query.Selector, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }

        return Result.Success<IReadOnlyList<string>, Errors>(result.Value);
    }
}