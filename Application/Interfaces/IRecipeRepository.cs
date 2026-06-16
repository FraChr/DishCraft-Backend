using DishCraft.Domain.Model;
using Service.Filters;

namespace DishCraft.Domain.Interfaces;

public interface IRecipeRepository
{
    Task<Recipe> GetByIdAsync(int id);
    Task<Recipe> GetBySlugAsync(string slug);
    Task<List<Recipe>> GetFilteredAsync(RecipeRepoFilter repoFilter);
    
    Task AddAsync(Recipe recipe);
    void UpdateAsync(Recipe recipe);
    void DeleteAsync(Recipe recipe);
}