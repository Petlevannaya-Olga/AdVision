using Shared;

namespace AdVision.Application.SharedErrors;

public static class CustomerDiscountErrors
{
    public static Error AlreadyAssigned(Guid customerId, Guid discountId) => CommonErrors.Conflict(
        "customer.discount.already.assigned",
        $"Скидка '{discountId}' уже назначена клиенту '{customerId}'");
}