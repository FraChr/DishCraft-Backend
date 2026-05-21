using DishCraft.Domain.Interfaces;
using DishCraft.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DishCraft.Infrastructure.Repositories;

public class RecipeRepository : IRecipeRepository
{
    
    private readonly Context _context;

    public RecipeRepository(Context context)
    {
        _context = context;
    }
    
    public async Task<Recipe> GetByIdAsync(int id)
    {
        return await BaseRecipeQuery()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<Recipe>> GetAllAsync()
    {
        return await BaseRecipeQuery()
            .ToListAsync();
    }

    public async Task AddAsync(Recipe recipe)
    {
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
    }

    public void UpdateAsync(Recipe recipe)
    {
        _context.Recipes.Update(recipe);
    }

    public void DeleteAsync(Recipe recipe)
    {
        _context.Recipes.Remove(recipe);
    }

    private IQueryable<Recipe> BaseRecipeQuery()
    {
        return _context.Recipes
            .Include(r => r.Difficulty)
            .Include(r => r.Instructions)
            .Include(r => r.RecipeTags)
                .ThenInclude(ra => ra.Tag)
            .Include(r => r.RecipeIngredients)
            .Include(r => r.RecipeAllergens)
                .ThenInclude(ra => ra.Allergen)
            .Include(r => r.RecipeNutritions)
                .ThenInclude(ra => ra.Nutrition);

    }
}