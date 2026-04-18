using Shared;

namespace AdVision.Application.SharedErrors;

public static class ContractErrors
{
    public static Error ContractNumberConflict(string number) => CommonErrors.Conflict(
        "contract.number.conflict",
        $"Договор с номером '{number}' уже существует");
    
    public static Error NotFound(Guid contractId) =>
        CommonErrors.NotFound(
            "contract.not.found",
            $"Договор с id '{contractId}' не найден");
}