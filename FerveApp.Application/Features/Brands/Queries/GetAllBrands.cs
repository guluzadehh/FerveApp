using SharedKernel;
using FerveApp.Domain.Repositories;

namespace FerveApp.Application;

public class GetAllBrandsHandler : IQueryHandler<GetAllBrandsQuery, GetAllBrandsResponse>
{
    private readonly IBrandRepository _repository;

    public GetAllBrandsHandler(IBrandRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetAllBrandsResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brands = await _repository.GetAllAsync();
        return new GetAllBrandsResponse(
            brands.Select(b => b.ToDto()).ToList()
        );
    }
}

public sealed record GetAllBrandsQuery() : IQuery<GetAllBrandsResponse>;

public sealed record GetAllBrandsResponse(List<BrandDto> Brands);