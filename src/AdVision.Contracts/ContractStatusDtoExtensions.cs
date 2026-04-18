namespace AdVision.Contracts;

public static class ContractStatusDtoExtensions
{
    public static string ToDisplay(this ContractStatusDto status) => status switch
    {
        ContractStatusDto.Draft => "Черновик",
        ContractStatusDto.Active => "Активный",
        ContractStatusDto.Signed => "Подписан",
        ContractStatusDto.Completed => "Завершен",
        ContractStatusDto.Cancelled => "Отменен",
        _ => status.ToString()
    };
}