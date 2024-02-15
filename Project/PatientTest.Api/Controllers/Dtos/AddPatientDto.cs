using System.ComponentModel.DataAnnotations;
using PatientTest.Models;

namespace PatientTest.Controllers.Dtos;

public record AddPatientDto(bool Active, Gender Gender, DateTime Birthdate, AddPatientDetailsDto Name)
{
    public static Patient ToModel(AddPatientDto dto)
    {
        return new Patient()
        {
            Gender = dto.Gender,
            Active = dto.Active,
            Birthdate = dto.Birthdate,
            Name = new PatientDetails()
            {
                Family = dto.Name.Family,
                Use = dto.Name.Use,
                Given = dto.Name.Given.Select(x => new PatientDetailsGiven { Name = x }).ToList(),
            },
        };
    }
}

public record AddPatientDetailsDto(string Use, [Required] string Family, List<string> Given);