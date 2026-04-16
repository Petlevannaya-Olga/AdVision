using Shared;

namespace AdVision.Application.SharedErrors;

public static class EmployeeErrors
{
    public static Error PhoneNumberConflict(string phoneNumber) => CommonErrors.Conflict(
        "employee.phone.number.conflict",
        $"Сотрудник с номером телефона '{phoneNumber}' уже существует");

    public static Error PassportConflict(string passportSeries, string passportNumber) => CommonErrors.Conflict(
        "employee.passport.conflict",
        $"Сотрудник с паспортом '{passportSeries} {passportNumber}' уже существует");
}