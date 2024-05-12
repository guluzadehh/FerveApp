using SharedKernel;
using FerveApp.Domain.ValueObjects;

namespace FerveApp.Domain.Errors;

public static class BrandErrors
{
    public static Error NotFound(Slug slug) => Error.NotFound("Brands.NotFound", $"Brand {slug.Value} was not found");
}
