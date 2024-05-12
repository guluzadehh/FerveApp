using FerveApp.Domain.Entities;
using FerveApp.Domain.ValueObjects;

namespace FerveApp.Domain.Repositories;

public interface IBrandRepository
{
    Task<IEnumerable<Brand>> GetAllAsync();
    Task<Brand?> GetByIdAsync(Guid id);
    Task<Brand?> GetBySlugAsync(Slug slug);
    Task CreateAsync(Brand brand);
    Task UpdateAsync(Brand brand);
    Task Delete(Brand brand);
}
