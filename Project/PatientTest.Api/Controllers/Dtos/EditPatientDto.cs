using System.ComponentModel.DataAnnotations;
using PatientTest.Models;

namespace PatientTest.Controllers.Dtos;

public record EditPatientDto(Guid Id, bool Active, Gender Gender, EditPatientDetailsDto Name, DateTimeOffset Birthdate)
{
    public void Edit(Patient model)
    {
        model.Active = Active;
        model.Birthdate = Birthdate;
        model.Gender = Gender;
        model.Name.Family = Name.Family;
        model.Name.Use = Name.Use;
        model.Name.Given = Name.Given.Select(x => new PatientDetailsGiven { Name = x }).ToList();
    }
}

public record EditPatientDetailsDto(string Use, [Required] string Family, List<string> Given);