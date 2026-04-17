using Shared;

namespace AdVision.Application.SharedErrors;

public static class CustomerErrors
{
    public static Error CustomerPhoneConflict(string phone) => CommonErrors.Conflict(
        "customer.phone.conflict",
        $"Клиент с номером телефона '{phone}' уже существует");
    
    public static Error NotFound(Guid customerId) => CommonErrors.NotFound(
        "customer.not.found",
        $"Клиент с id '{customerId}' не найден");
}