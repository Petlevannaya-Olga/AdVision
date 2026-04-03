using AdVision.Contracts;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.VenueTypes.GetAllVenueTypesQuery;

public sealed class GetAllVenueTypesQueryHandler(IVenueTypeRepository repository)
    : IQueryHandler<Result<IReadOnlyList<VenueTypeDto>, Error>, GetAllVenueTypesQuery>
{
    public async Task<Result<IReadOnlyList<VenueTypeDto>, Error>> Handle(GetAllVenueTypesQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAllAsync(cancellationToken);

        if (result.IsFailure)
        {
            return result.Error;
        }

        return result
            .Value
            .Select(vt => new VenueTypeDto(vt.Name.Value))
            .ToList();
    }
}