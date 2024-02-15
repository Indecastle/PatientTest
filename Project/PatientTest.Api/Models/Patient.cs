using System.ComponentModel.DataAnnotations;

namespace PatientTest.Models;

public class Patient
{
    public Guid Id { get; set; }
    public PatientDetails Name { get; set; }
    public Gender Gender { get; set; }
    public bool Active { get; set; }
    public DateTimeOffset Birthdate { get; set; }
}

public class PatientDetails
{
    public string Use { get; set; }
    [Required]
    public string Family { get; set; }
    public List<PatientDetailsGiven> Given { get; set; }
}

public class PatientDetailsGiven
{
    public string Name { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (PatientDetailsGiven)obj;

        return Name == other.Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}