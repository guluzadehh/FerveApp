using FerveApp.Domain.Errors;
using FerveApp.Domain.Repositories;
using FerveApp.Domain.ValueObjects;
using SharedKernel;

namespace FerveApp.Application;

public class DeleteBrandCommandHandler : ICommandHandler<DeleteBrandCommand>
{
    private readonly IBrandRepository _repository;

    public DeleteBrandCommandHandler(IBrandRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var slug = new Slug(request.Slug, false);
        var brand = await _repository.GetBySlugAsync(slug);

        if (brand is null)
        {
            return BrandErrors.NotFound(slug);
        }

        await _repository.Delete(brand);

        return Result.Success();
    }
}

public sealed record DeleteBrandCommand(string Slug) : ICommand;