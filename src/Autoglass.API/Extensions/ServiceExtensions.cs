using Autoglass.Domain.Interfaces.Repositories;
using Autoglass.Infrastructure.Repositories;

namespace Autoglass.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<ISupplierRepository, SupplierRepository>();
    }
}
