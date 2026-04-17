using AdVision.Domain.Customers;
using AdVision.Domain.Discounts;

namespace AdVision.Domain.CustomerDiscounts;

public sealed class CustomerDiscount
{
    public CustomerDiscountId Id { get; private set; }

    public CustomerId CustomerId { get; private set; }
    public DiscountId DiscountId { get; private set; }

    public CustomerDiscount(CustomerId customerId, DiscountId discountId)
    {
        Id = new CustomerDiscountId(Guid.NewGuid());
        CustomerId = customerId;
        DiscountId = discountId;
    }

    // EF Core
    private CustomerDiscount() { }
}