using System.Data;
using Dapper;
using FerveApp.Domain.Entities;
using FerveApp.Domain.Repositories;
using FerveApp.Domain.ValueObjects;
using FerveApp.Infrastructure.Dtos;

namespace FerveApp.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly IRepositoryContext _context;

    public BrandRepository(IRepositoryContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Brand brand)
    {
        const string query =
            @"INSERT INTO 
                dbo.Brands(Id, Name, Slug)
            VALUES
                (@Id, @Name, @Slug);";

        using IDbConnection connection = _context.GetConnection();

        await connection.ExecuteAsync(
            query,
            new { Id = brand.Id.ToString(), Name = brand.Name.Value, Slug = brand.Slug.Value });
    }

    public async Task Delete(Brand brand)
    {
        const string query =
            @"DELETE FROM 
                dbo.Brands 
            WHERE 
                dbo.Brands.Id = @Id";

        using IDbConnection connection = _context.GetConnection();
        await connection.ExecuteAsync(query, new { Id = brand.Id.ToString() });
    }

    public async Task<IEnumerable<Brand>> GetAllAsync()
    {
        const string query = "SELECT * FROM dbo.Brands;";

        using IDbConnection connection = _context.GetConnection();
        IEnumerable<BrandDto> brandDtos = await connection.QueryAsync<BrandDto>(query);

        return brandDtos.Select(b => Brand.Create(b.Id, b.Name, b.Slug));
    }

    public async Task<Brand?> GetByIdAsync(Guid id)
    {
        const string query = "SELECT * FROM dbo.Brands WHERE dbo.Brands.Id = @Id;";

        using IDbConnection connection = _context.GetConnection();
        BrandDto? brandDto = await connection.QuerySingleOrDefaultAsync<BrandDto>(query, new { Id = id });

        if (brandDto is null) return null;

        return Brand.Create(brandDto.Id, brandDto.Name, brandDto.Slug);
    }

    public async Task<Brand?> GetBySlugAsync(Slug slug)
    {
        const string query = "SELECT * FROM dbo.Brands WHERE dbo.Brands.Slug = @Slug;";

        using IDbConnection connection = _context.GetConnection();
        BrandDto? brandDto = await connection.QuerySingleOrDefaultAsync<BrandDto>(query, new { Slug = slug.Value });

        if (brandDto is null) return null;

        return Brand.Create(brandDto.Id, brandDto.Name, brandDto.Slug);
    }

    public async Task UpdateAsync(Brand brand)
    {
        const string query =
            @"UPDATE 
                dbo.Brands 
            SET 
                dbo.Brands.Name = @Name, 
                dbo.Brands.Slug = @Slug
            WHERE
                dbo.Brands.Id = @Id;";

        using IDbConnection connection = _context.GetConnection();
        await connection.ExecuteAsync(query, new { Id = brand.Id, Name = brand.Name.Value, Slug = brand.Slug.Value });
    }
}
