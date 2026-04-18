using System.Linq.Expressions;
using AdVision.Domain.Employees;
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Application.Repositories;

public interface IEmployeeRepository
{
    Task<Result<Guid, Error>> AddAsync(Employee employee, CancellationToken cancellationToken);

    Task<Result<Employee?, Error>> GetByAsync(
        Expression<Func<Employee, bool>> expression,
        CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<Employee>, Error>> GetAllAsync(CancellationToken cancellationToken);
}