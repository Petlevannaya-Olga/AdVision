using Shared;

namespace AdVision.Application.SharedErrors;

public static class DiscountErrors
{
    public static Error DiscountNameConflict(string name) => CommonErrors.Conflict(
        "discount.name.conflict",
        $"Скидка с названием '{name}' уже существует");
    
    public static Error NotFound(Guid discountId) => CommonErrors.NotFound(
        "discount.not.found",
        $"Скидка с id '{discountId}' не найдена");
}