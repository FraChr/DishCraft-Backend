using DishCraft.Domain.Interfaces;
using DishCraft.Domain.Model;
using Service.Dtos;

namespace Service.Services;

public class RecipeService
{
    private readonly IRecipeRepository _repo;

    public RecipeService(IRecipeRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<RecipeDto>> GetAllRecipes()
    {
        var recipes = await _repo.GetAllAsync();
        return recipes.Select(BaseDto).ToList();
    }

    public async Task<RecipeDto?> GetRecipe(int id)
    {
        var recipe = await _repo.GetByIdAsync(id);
        return BaseDto(recipe);
    }


    private RecipeDto BaseDto(Recipe recipe)
    {
        return new RecipeDto
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Difficulty = recipe.Difficulty.Name,
            Tags = recipe.RecipeTags
                .Select(t => t.Tag.Name)
                .ToList(),
            Allergens = recipe.RecipeAllergens
                .Select(t => t.Allergen.Name)
                .ToList(),
            Ingredients = recipe.RecipeIngredients
                .Select(t => t.Ingredient.Name)
                .ToList(),
        };
    }
}