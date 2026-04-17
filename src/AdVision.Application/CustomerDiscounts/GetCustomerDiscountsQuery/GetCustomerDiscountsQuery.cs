using Shared.Abstractions;

namespace AdVision.Application.CustomerDiscounts.GetCustomerDiscountsQuery;

public sealed record GetCustomerDiscountsQuery(Guid CustomerId) : IQuery;