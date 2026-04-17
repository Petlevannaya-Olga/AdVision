using System.Linq.Expressions;
using AdVision.Domain.Customers;
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Application;

public interface ICustomerRepository
{
    Task<Result<Guid, Error>> AddAsync(Customer customer, CancellationToken cancellationToken);

    Task<Result<Customer?, Error>> GetByAsync(
        Expression<Func<Customer, bool>> expression,
        CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<Customer>, Error>> GetAllAsync(CancellationToken cancellationToken);
}