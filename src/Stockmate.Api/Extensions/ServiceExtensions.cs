using Stockmate.Application.Interfaces;
using Stockmate.Application.Services;
using Stockmate.Domain.Entities;
using Stockmate.Domain.Interfaces.Repositories;
using Stockmate.Domain.Validations;
using Stockmate.Infrastructure.Repositories;

namespace Stockmate.Api.Extensions;

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
