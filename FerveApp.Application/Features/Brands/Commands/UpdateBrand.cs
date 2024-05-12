using FerveApp.Domain.Errors;
using FerveApp.Domain.Repositories;
using FerveApp.Domain.ValueObjects;
using FluentValidation;
using SharedKernel;

namespace FerveApp.Application;

public class UpdateBrandHandler : ICommandHandler<UpdateBrandCommand, UpdateBrandResponse>
{
    private readonly IBrandRepository _repository;

    public UpdateBrandHandler(IBrandRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<UpdateBrandResponse>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        //validate
        var currSlug = new Slug(request.Slug, false);
        var brand = await _repository.GetBySlugAsync(currSlug);

        if (brand is null)
        {
            return BrandErrors.NotFound(currSlug);
        }

        brand.UpdateName(request.Name);
        await _repository.UpdateAsync(brand);

        return new UpdateBrandResponse(brand.ToDto());
    }
}

public sealed record UpdateBrandCommand(string Slug, string Name) : ICommand<UpdateBrandResponse>;

public sealed record UpdateBrandResponse(BrandDto Brand);

public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Brand name can't be null.")
            .MaximumLength(BrandName.MaxLength)
            .WithMessage("Max brand name length is 50 chars.");
    }
}