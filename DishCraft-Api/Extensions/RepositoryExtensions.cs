using DishCraft.Domain.Interfaces;
using DishCraft.Infrastructure.Repositories;

namespace DishCraft_Api.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        
        return services;
    }
}