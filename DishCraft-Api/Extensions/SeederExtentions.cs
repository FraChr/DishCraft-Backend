using DishCraft.Infrastructure.Seed;

namespace DishCraft_Api.Extensions;

public static class SeederExtentions
{
    public static IServiceCollection AddSeeders(this IServiceCollection services)
    {
        services.AddScoped<DbSeeder>();
        services.AddScoped<JsonSeeder>();
        return services;
    }
}