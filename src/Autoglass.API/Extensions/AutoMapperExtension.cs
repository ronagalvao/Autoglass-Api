namespace Autoglass.API.Extensions;

public static class AutoMapperExtension
{
    public static IServiceCollection AddMappers(this IServiceCollection services) =>
        services
            .AddAutoMapper(typeof(Program));
}
