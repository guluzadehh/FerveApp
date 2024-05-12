using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using SharedKernel;

namespace FerveApp.Domain.ValueObjects;

public class BrandName : ValueObject
{
    public const int MaxLength = 50;

    [SetsRequiredMembers]
    public BrandName(string name)
    {
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.LengthOutOfRange(name, minLength: 1, maxLength: MaxLength);

        Value = name;
    }

    public required string Value { get; init; }

    public override string ToString()
    {
        return Value;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
