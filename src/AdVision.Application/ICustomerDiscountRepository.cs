using System.Linq.Expressions;
using AdVision.Domain.CustomerDiscounts;
using AdVision.Domain.Customers;
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Application;

public interface ICustomerDiscountRepository
{
    Task<Result<Guid, Error>> AddAsync(CustomerDiscount entity, CancellationToken ct);

    Task<Result<CustomerDiscount?, Error>> GetByAsync(
        Expression<Func<CustomerDiscount, bool>> expression,
        CancellationToken ct);

    Task<Result<IReadOnlyList<CustomerDiscount>, Error>> GetAllAsync(CancellationToken ct);
    
    Task<Result<IReadOnlyList<CustomerDiscount>, Error>> GetByCustomerIdAsync(
        CustomerId customerId,
        CancellationToken cancellationToken);
}