using Shared;

namespace AdVision.Application.SharedErrors;

public static class CustomerErrors
{
    public static Error CustomerPhoneConflict(string phone) => CommonErrors.Conflict(
        "customer.phone.conflict",
        $"Клиент с номером телефона '{phone}' уже существует");
}