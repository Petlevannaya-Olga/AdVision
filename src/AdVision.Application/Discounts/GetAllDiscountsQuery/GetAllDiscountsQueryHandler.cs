using AdVision.Contracts;
using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace AdVision.Application.Discounts.GetAllDiscountsQuery;

public sealed class GetAllDiscountsQueryHandler(IDiscountRepository repository)
    : IQueryHandler<IReadOnlyList<DiscountDto>, GetAllDiscountsQuery>
{
    public async Task<Result<IReadOnlyList<DiscountDto>, Errors>> Handle(
        GetAllDiscountsQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAllAsync(cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToErrors();
        }

        return result
            .Value
            .Select(d => new DiscountDto(
                d.Id.Value,
                d.Name.Value,
                d.Percent.Value,
                d.MinTotal.Value))
            .ToList();
    }
}