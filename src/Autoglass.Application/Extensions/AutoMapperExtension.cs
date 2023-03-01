using Autoglass.Application.Mappings;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Autoglass.Application.Extensions;

public static class AutoMapperExtension
{
    public static void AddMappers(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));

        IMapper mapper = mapperConfig.CreateMapper();

        services.AddSingleton(mapper);
    }
}
