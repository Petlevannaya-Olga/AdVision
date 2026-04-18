using AdVision.Contracts;
using Shared.Abstractions;

namespace AdVision.Application.Contracts.GetContractsQuery;

public sealed record GetContractsQuery(
    int Page,
    int PageSize,
    string? Number,
    Guid? CustomerId,
    Guid? EmployeeId,
    ContractStatusDto? Status,
    DateOnly StartDateFrom,
    DateOnly StartDateTo,
    DateOnly EndDateFrom,
    DateOnly EndDateTo,
    DateOnly SignedDateFrom,
    DateOnly SignedDateTo,
    string? OrderBy,
    bool Descending) : IQuery;