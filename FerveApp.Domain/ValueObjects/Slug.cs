using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using SharedKernel;
using Slugify;

namespace FerveApp.Domain.ValueObjects;

public class Slug : ValueObject
{
    public const int MaxLength = 50;

    [SetsRequiredMembers]
    public Slug(string value, bool convert = true)
    {
        var slug = value;

        if (convert)
        {
            var helper = new SlugHelper();
            slug = helper.GenerateSlug(value);
        }

        if (slug.Length > MaxLength)
        {
            slug = slug[..(MaxLength + 1)];
        }

        Guard.Against.NullOrWhiteSpace(slug);
        Guard.Against.LengthOutOfRange(value, minLength: 1, maxLength: MaxLength);

        Value = slug;
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
