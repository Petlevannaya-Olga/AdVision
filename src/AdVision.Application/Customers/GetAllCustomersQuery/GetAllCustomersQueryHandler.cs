using AdVision.Application.Repositories;
using AdVision.Contracts;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Customers.GetAllCustomersQuery;

public sealed class GetAllCustomersQueryHandler(ICustomerRepository repository)
    : IQueryHandler<IReadOnlyList<CustomerDto>, GetAllCustomersQuery>
{
    public async Task<Result<IReadOnlyList<CustomerDto>, Errors>> Handle(
        GetAllCustomersQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAllAsync(cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }

        return result.Value
            .Select(c => new CustomerDto(
                c.Id.Value,
                c.LastName.Value,
                c.FirstName.Value,
                c.MiddleName.Value,
                c.PhoneNumber.Value))
            .ToList();
    }
}