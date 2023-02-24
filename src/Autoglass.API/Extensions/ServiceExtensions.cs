using Autoglass.Application.Interfaces;
using Autoglass.Application.Services;
using Autoglass.Domain.Entities;
using Autoglass.Domain.Interfaces.Repositories;
using Autoglass.Domain.Validations;
using Autoglass.Infrastructure.Repositories;

namespace Autoglass.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddScoped<IProductService, ProductService>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<ProductValidation>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<ISupplierRepository, SupplierRepository>();
}
