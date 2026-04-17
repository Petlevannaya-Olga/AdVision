using AdVision.Application.SharedErrors;
using AdVision.Contracts;
using AdVision.Domain.Customers;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.CustomerDiscounts.GetCustomerDiscountsQuery;

public sealed class GetCustomerDiscountsQueryHandler(
    ICustomerRepository customerRepository,
    ICustomerDiscountRepository customerDiscountRepository,
    IDiscountRepository discountRepository)
    : IQueryHandler<IReadOnlyList<CustomerDiscountDetailsDto>, GetCustomerDiscountsQuery>
{
    public async Task<Result<IReadOnlyList<CustomerDiscountDetailsDto>, Errors>> Handle(
        GetCustomerDiscountsQuery query,
        CancellationToken cancellationToken = default)
    {
        var customerId = new CustomerId(query.CustomerId);

        var customerResult = await customerRepository.GetByAsync(
            x => x.Id == customerId,
            cancellationToken);

        if (customerResult.IsFailure)
        {
            return customerResult.Error.ToErrors();
        }

        if (customerResult.Value is null)
        {
            return CustomerErrors.NotFound(query.CustomerId).ToErrors();
        }

        var customerDiscountsResult = await customerDiscountRepository.GetByCustomerIdAsync(
            customerId,
            cancellationToken);

        if (customerDiscountsResult.IsFailure)
        {
            return customerDiscountsResult.Error.ToErrors();
        }

        var discountsResult = await discountRepository.GetAllAsync(cancellationToken);

        if (discountsResult.IsFailure)
        {
            return discountsResult.Error.ToErrors();
        }

        var discountsMap = discountsResult.Value.ToDictionary(x => x.Id);

        var result = customerDiscountsResult.Value
            .Where(cd => discountsMap.ContainsKey(cd.DiscountId))
            .Select(cd =>
            {
                var discount = discountsMap[cd.DiscountId];

                return new CustomerDiscountDetailsDto(
                    cd.Id.Value,
                    cd.CustomerId.Value,
                    cd.DiscountId.Value,
                    discount.Name.Value,
                    discount.Percent.Value,
                    discount.MinTotal.Value);
            })
            .ToList();

        return result;
    }
}