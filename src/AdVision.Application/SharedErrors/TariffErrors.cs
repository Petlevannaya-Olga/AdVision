using Shared;

namespace AdVision.Application.SharedErrors;

public static class TariffErrors
{
    public static Error NotFound(Guid id) =>
        CommonErrors.NotFound(
            "tariff.not.found",
            $"Тариф с id '{id}' не найден");
}