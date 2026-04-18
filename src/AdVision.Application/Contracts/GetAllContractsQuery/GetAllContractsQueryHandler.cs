using AdVision.Application.Repositories;
using AdVision.Contracts;
using AdVision.Domain.Contracts;
using AdVision.Domain.Customers;
using AdVision.Domain.Employees;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Contracts.GetAllContractsQuery;

public sealed class GetContractsQueryHandler(
    IContractRepository repository)
    : IQueryHandler<PagedResult<ContractDto>, GetContractsQuery.GetContractsQuery>
{
    public async Task<Result<PagedResult<ContractDto>, Errors>> Handle(
        GetContractsQuery.GetContractsQuery query,
        CancellationToken cancellationToken = default)
    {
        ContractStatus? status = query.Status.HasValue
            ? MapStatus(query.Status.Value)
            : null;

        var result = await repository.GetPagedAsync(
            query.Page,
            query.PageSize,
            query.Number,
            query.CustomerId.HasValue ? new CustomerId(query.CustomerId.Value) : null,
            query.EmployeeId.HasValue ? new EmployeeId(query.EmployeeId.Value) : null,
            status,
            query.StartDateFrom,
            query.StartDateTo,
            query.EndDateFrom,
            query.EndDateTo,
            query.SignedDateFrom,
            query.SignedDateTo,
            query.OrderBy,
            query.Descending,
            cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }

        var pagedContracts = result.Value;

        var dtoItems = pagedContracts.Items
            .Select(Map)
            .ToList();

        return new PagedResult<ContractDto>(
            dtoItems,
            pagedContracts.Page,
            pagedContracts.PageSize,
            pagedContracts.TotalCount);
    }

    private static ContractDto Map(Contract contract)
    {
        var customerFullName =
            $"{contract.Customer.LastName.Value} {contract.Customer.FirstName.Value} {contract.Customer.MiddleName.Value}";

        var employeeFullName =
            $"{contract.Employee.LastName.Value} {contract.Employee.FirstName.Value} {contract.Employee.MiddleName.Value}";

        return new ContractDto(
            contract.Id.Value,
            contract.Number.Value,
            contract.CustomerId.Value,
            customerFullName,
            contract.EmployeeId.Value,
            employeeFullName,
            contract.DateInterval.StartDate,
            contract.DateInterval.EndDate,
            MapStatus(contract.Status),
            contract.SignedDate);
    }

    private static ContractStatusDto MapStatus(ContractStatus status)
    {
        return status switch
        {
            ContractStatus.Draft => ContractStatusDto.Draft,
            ContractStatus.Active => ContractStatusDto.Active,
            ContractStatus.Signed => ContractStatusDto.Signed,
            ContractStatus.Completed => ContractStatusDto.Completed,
            ContractStatus.Cancelled => ContractStatusDto.Cancelled,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }

    private static ContractStatus MapStatus(ContractStatusDto status)
    {
        return status switch
        {
            ContractStatusDto.Draft => ContractStatus.Draft,
            ContractStatusDto.Active => ContractStatus.Active,
            ContractStatusDto.Signed => ContractStatus.Signed,
            ContractStatusDto.Completed => ContractStatus.Completed,
            ContractStatusDto.Cancelled => ContractStatus.Cancelled,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}