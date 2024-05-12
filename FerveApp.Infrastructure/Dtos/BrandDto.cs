namespace FerveApp.Infrastructure.Dtos;

public sealed class BrandDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Slug { get; init; }
}