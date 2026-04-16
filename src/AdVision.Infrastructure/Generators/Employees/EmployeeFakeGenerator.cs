using AdVision.Application.Generators.Employees;
using AdVision.Domain.Employees;
using AdVision.Domain.Positions;

namespace AdVision.Infrastructure.Generators.Employees;

public class EmployeeFakeGenerator(
    IPersonNameFakeGenerator personNameFakeGenerator,
    IEmployeeAddressFakeGenerator addressFakeGenerator,
    IPassportFakeGenerator passportFakeGenerator,
    IPhoneNumberFakeGenerator phoneFakeGenerator) : IEmployeeFakeGenerator
{
    public Employee Generate()
    {
        var gender = personNameFakeGenerator.GenerateGender();

        var lastName = personNameFakeGenerator.GenerateLastName(gender);
        var firstName = personNameFakeGenerator.GenerateFirstName(gender);
        var middleName = personNameFakeGenerator.GenerateMiddleName(gender);

        return new Employee(
            lastName: lastName,
            firstName: firstName,
            middleName: middleName,
            address: addressFakeGenerator.Generate(),
            passport: passportFakeGenerator.Generate(),
            phoneNumber: phoneFakeGenerator.Generate(),
            positionId: new PositionId(Guid.NewGuid()));
    }
}