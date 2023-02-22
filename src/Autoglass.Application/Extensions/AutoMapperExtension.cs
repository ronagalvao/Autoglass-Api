using Autoglass.Application.Dtos;
using Autoglass.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Autoglass.Application.Extensions
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection Services { get; private set; }

        public static void Configure()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<ProductDto, Product>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            Services.AddSingleton(mapper);
        }
    }
}
