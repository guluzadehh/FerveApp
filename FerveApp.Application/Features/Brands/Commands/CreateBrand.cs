using FerveApp.Domain.Entities;
using FerveApp.Domain.Repositories;
using FerveApp.Domain.ValueObjects;
using FluentValidation;
using SharedKernel;

namespace FerveApp.Application;

public class CreateBrandHandler : ICommandHandler<CreateBrandCommand, CreateBrandResponse>
{
    private readonly IBrandRepository _repository;

    public CreateBrandHandler(IBrandRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<CreateBrandResponse>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        // validate
        var brand = Brand.Create(request.Name);
        await _repository.CreateAsync(brand);
        return new CreateBrandResponse(brand.ToDto());
    }
}

public sealed record CreateBrandCommand(string Name) : ICommand<CreateBrandResponse>;

public sealed record CreateBrandResponse(BrandDto Brand);

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Brand name can't be null.")
            .MaximumLength(BrandName.MaxLength)
            .WithMessage("Max brand name length is 50 chars.");
    }
}