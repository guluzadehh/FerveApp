using FerveApp.Domain.Entities;

namespace FerveApp.Application;

public sealed record BrandDto(
    string Id,
    string Name,
    string Slug
);


public static class MapExtension
{
    public static BrandDto ToDto(this Brand brand)
    {
        return new BrandDto(brand.Id.ToString(), brand.Name.Value, brand.Slug.Value);
    }
}