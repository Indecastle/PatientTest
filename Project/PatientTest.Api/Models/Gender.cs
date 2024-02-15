using PatientTest.Models.Types;

namespace PatientTest.Models;

public sealed class Gender : EnumValue<string>
{
    public Gender(string value) : base(value)
    {
    }

    protected Gender()
    {
    }

    public static readonly Gender Male = new() { Value = "Male" };
    public static readonly Gender Female = new() { Value = "Female" };
    public static readonly Gender Other = new() { Value = "Other" };
    public static readonly Gender Unknown = new() { Value = "Unknown" };
    

    protected override HashSet<string> PossibleValues { get; } = new()
    {
        Male!,
        Female!,
        Other!,
        Unknown!,
    };

    public static implicit operator string?(Gender? Gender)
    {
        return Gender?.Value;
    }

    public static implicit operator Gender?(string? Gender)
    {
        return Gender != null ? new Gender(Gender) : null;
    }

    public static bool operator ==(Gender? left, Gender? right)
    {
        return EqualOperator(left, right);
    }

    public static bool operator !=(Gender? left, Gender? right)
    {
        return NotEqualOperator(left, right);
    }
}