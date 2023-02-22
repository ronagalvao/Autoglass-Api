using Autoglass.Application.Dtos;
using Autoglass.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Autoglass.Application.Extensions
{
    public static class AutoMapperExtension
    {
        public static void AddMappers(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<ProductDto, Product>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
