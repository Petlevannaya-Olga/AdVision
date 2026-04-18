using System.Linq.Expressions;
using AdVision.Domain.Discounts;
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Application.Repositories;

public interface IDiscountRepository
{
    Task<Result<Guid, Error>> AddAsync(Discount discount, CancellationToken cancellationToken);

    Task<Result<Discount?, Error>> GetByAsync(
        Expression<Func<Discount, bool>> expression,
        CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<Discount>, Error>> GetAllAsync(CancellationToken cancellationToken);
}