using BFP.App.Core.Models.Types;

namespace PatientTest.Models.Types;

public abstract class EnumValue<TValue> : ValueObject, ISingleValueObject<TValue> where TValue : notnull
{
    protected abstract HashSet<TValue> PossibleValues { get; }

    public TValue Value { get; protected init; }

    protected EnumValue()
    {
    }

    protected EnumValue(TValue value)
    {
        if (!PossibleValues.Contains(value))
            throw new ArgumentException("Invalid EnumType");

        Value = value;
    }

    TValue ISingleValueObject<TValue>.Convert() => Value;

    public override string ToString() => Value.ToString()!;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}