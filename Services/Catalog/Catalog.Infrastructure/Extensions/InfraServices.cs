using Catalog.Core.Repositories;
using Catalog.Core.UniteOfWorks;
using Catalog.Infrastructure.Repositories;
using Catalog.Infrastructure.UniteOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.Extensions
{
    public static class InfraServices
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductBrandRepository, ProductBrandRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IUniteOfWork, UniteOfWork>();

            return services;
        }
    }
}
