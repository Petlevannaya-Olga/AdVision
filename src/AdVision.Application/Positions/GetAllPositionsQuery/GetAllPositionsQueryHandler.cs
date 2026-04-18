using AdVision.Application.Repositories;
using AdVision.Contracts;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Positions.GetAllPositionsQuery;

public sealed class GetAllPositionsQueryHandler(IPositionRepository repository)
    : IQueryHandler<IReadOnlyList<PositionDto>, GetAllPositionsQuery>
{
    public async Task<Result<IReadOnlyList<PositionDto>, Errors>> Handle(
        GetAllPositionsQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAllAsync(cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }

        return result
            .Value
            .Select(p => new PositionDto(p.Id.Value, p.Name.Value))
            .ToList();
    }
}