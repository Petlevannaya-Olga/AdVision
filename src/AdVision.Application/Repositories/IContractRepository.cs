using System.Linq.Expressions;
using AdVision.Contracts;
using AdVision.Domain.Contracts;
using AdVision.Domain.Customers;
using AdVision.Domain.Employees;
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Application.Repositories;

public interface IContractRepository
{
    Task<Result<Guid, Error>> AddAsync(Contract contract, CancellationToken cancellationToken);

    Task<Result<Contract?, Error>> GetByAsync(
        Expression<Func<Contract, bool>> expression,
        CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<Contract>, Error>> GetAllAsync(CancellationToken cancellationToken);

    Task<Result<PagedResult<Contract>, Error>> GetPagedAsync(
        int page,
        int pageSize,
        string? number,
        CustomerId? customerId,
        EmployeeId? employeeId,
        ContractStatus? status,
        DateOnly startDateFrom,
        DateOnly startDateTo,
        DateOnly endDateFrom,
        DateOnly endDateTo,
        DateOnly signedDateFrom,
        DateOnly signedDateTo,
        string? orderBy,
        bool descending,
        CancellationToken cancellationToken);
    
    Task<Result<ContractDateBoundsDto?, Error>> GetDateBoundsAsync(CancellationToken cancellationToken);
}