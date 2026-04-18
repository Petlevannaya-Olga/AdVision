using AdVision.Application.Repositories;
using AdVision.Contracts;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Employees.GetAllEmployeesQuery;

public sealed class GetAllEmployeesQueryHandler(IEmployeeRepository repository)
    : IQueryHandler<IReadOnlyList<EmployeeDto>, GetAllEmployeesQuery>
{
    public async Task<Result<IReadOnlyList<EmployeeDto>, Errors>> Handle(
        GetAllEmployeesQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAllAsync(cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }

        return result.Value
            .Select(x => new EmployeeDto(
                x.Id.Value,
                x.LastName.Value,
                x.FirstName.Value,
                x.MiddleName.Value,
                x.Address.Value,
                x.Passport.Series.Value,
                x.Passport.Number.Value,
                x.PhoneNumber.Value,
                x.PositionId.Value))
            .ToList();
    }
}