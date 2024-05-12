using SharedKernel;
using FerveApp.Domain.ValueObjects;

namespace FerveApp.Domain.Entities;

public class Brand : Entity
{
    private Brand(
        Guid id,
        BrandName name,
        Slug slug)
        : base(id)
    {
        Name = name;
        Slug = slug;
    }

    public BrandName Name { get; private set; }
    public Slug Slug { get; private set; }

    public void UpdateName(string name)
    {
        Name = new BrandName(name);
        Slug = new Slug(name);
    }

    public static Brand Create(string name)
    {
        return new Brand(
            Guid.NewGuid(),
            new BrandName(name),
            new Slug(name)
        );
    }

    public static Brand Create(Guid id, string name, string slug)
    {
        return new Brand(
            id,
            new BrandName(name),
            new Slug(slug, false)
        );
    }
}
