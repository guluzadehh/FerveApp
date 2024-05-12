using FerveApp.Domain.Errors;
using FerveApp.Domain.Repositories;
using SharedKernel;
using FerveApp.Domain.ValueObjects;

namespace FerveApp.Application;

public class GetBrandBySlug : IQueryHandler<GetBrandBySlugQuery, GetBrandBySlugResponse>
{
    private readonly IBrandRepository _repository;

    public GetBrandBySlug(IBrandRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetBrandBySlugResponse>> Handle(GetBrandBySlugQuery request, CancellationToken cancellationToken)
    {
        var slug = new Slug(request.Slug, false);
        var brand = await _repository.GetBySlugAsync(slug);

        if (brand is null)
        {
            return BrandErrors.NotFound(slug);
        }

        return new GetBrandBySlugResponse(brand.ToDto());
    }
}

public sealed record GetBrandBySlugQuery(string Slug) : IQuery<GetBrandBySlugResponse>; // validate with fluentvalidation

public sealed record GetBrandBySlugResponse(BrandDto Brand);