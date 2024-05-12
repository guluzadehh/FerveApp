using FerveApp.Domain.Repositories;
using FerveApp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FerveApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IRepositoryContext, RepositoryContext>();
        services.AddSingleton<IBrandRepository, BrandRepository>();
        return services;
    }
}
