using System.Linq.Expressions;
using AdVision.Domain.Positions;
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Application;

public interface IPositionRepository
{
    Task<Result<Guid, Error>> AddAsync(Position position, CancellationToken cancellationToken);

    Task<Result<Position?, Error>> GetByAsync(
        Expression<Func<Position, bool>> expression,
        CancellationToken cancellationToken);
    
    Task<Result<IReadOnlyList<Position>, Error>> GetAllAsync(CancellationToken cancellationToken);
}