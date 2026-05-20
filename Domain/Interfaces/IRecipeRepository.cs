using DishCraft.Domain.Model;

namespace DishCraft.Domain.Interfaces;

public interface IRecipeRepository
{
    Task<Recipe> GetByIdAsync(int id);
    Task<List<Recipe>> GetAllAsync();
    
    Task AddAsync(Recipe recipe);
    void UpdateAsync(Recipe recipe);
    void DeleteAsync(Recipe recipe);
}