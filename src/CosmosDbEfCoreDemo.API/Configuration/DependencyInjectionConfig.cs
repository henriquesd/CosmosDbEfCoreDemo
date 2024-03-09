using CosmosDbEfCoreDemo.Domain.Interfaces;
using CosmosDbEfCoreDemo.Domain.Services;
using CosmosDbEfCoreDemo.Infrastructure.Context;
using CosmosDbEfCoreDemo.Infrastructure.Repositories;

namespace CosmosDbEfCoreDemo.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<CosmosDbContext>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}