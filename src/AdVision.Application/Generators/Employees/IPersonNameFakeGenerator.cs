using AdVision.Domain.Employees;
using Bogus.DataSets;

namespace AdVision.Application.Generators.Employees;

public interface IPersonNameFakeGenerator
{
    Name.Gender GenerateGender();
    PersonName GenerateFirstName(Name.Gender gender);
    PersonName GenerateLastName(Name.Gender gender);
    PersonName GenerateMiddleName(Name.Gender gender);
}