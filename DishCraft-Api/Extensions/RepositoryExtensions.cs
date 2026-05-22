using DishCraft.Domain.Interfaces;
using DishCraft.Infrastructure.Repositories;
using Service.Interfaces;
using Service.Services;

namespace DishCraft_Api.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<ILookupRepository, LookupRepository>();
        return services;
    }
}