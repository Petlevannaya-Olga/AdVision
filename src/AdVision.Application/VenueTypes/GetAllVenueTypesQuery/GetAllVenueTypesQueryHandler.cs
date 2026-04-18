using AdVision.Application.Repositories;
using AdVision.Contracts;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.VenueTypes.GetAllVenueTypesQuery;

public sealed class GetAllVenueTypesQueryHandler(IVenueTypeRepository repository)
    : IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery>
{
    public async Task<Result<IReadOnlyList<VenueTypeDto>, Errors>> Handle(GetAllVenueTypesQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAllAsync(cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }

        return result
            .Value
            .Select(vt => new VenueTypeDto(vt.Id.Value, vt.Name.Value))
            .ToList();
    }
}