using Shared;

namespace AdVision.Application.SharedErrors;

public static class DiscountErrors
{
    public static Error DiscountNameConflict(string name) => CommonErrors.Conflict(
        "discount.name.conflict",
        $"Скидка с названием '{name}' уже существует");
}