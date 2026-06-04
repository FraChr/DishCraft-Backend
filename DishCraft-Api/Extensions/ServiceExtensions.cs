using Service.Interfaces;
using Service.Services;

namespace DishCraft_Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IRecipeService, RecipeService>();
        services.AddScoped<ILookupService, LookupService>();
        return services;
    }
}