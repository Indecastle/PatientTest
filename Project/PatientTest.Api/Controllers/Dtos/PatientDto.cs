using System.ComponentModel.DataAnnotations;
using PatientTest.Models;

namespace PatientTest.Controllers.Dtos;

public record PatientDto(Guid Id, bool Active, Gender Gender, DateTimeOffset Birthdate, PatientDetailsDto Name)
{
    public static PatientDto ToDto(Patient model)
    {
        return new PatientDto(
            model.Id,
            model.Active,
            model.Gender,
            model.Birthdate,
            new PatientDetailsDto(
                model.Name.Use,
                model.Name.Family,
                model.Name.Given.Select(x => x.Name).ToList())
            );
    }
}

public record PatientDetailsDto(string Use, [Required] string Family, List<string> Given);