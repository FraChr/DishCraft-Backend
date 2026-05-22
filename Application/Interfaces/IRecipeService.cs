using DishCraft.Domain.Model;
using Service.Dtos;

namespace Service.Interfaces;

public interface IRecipeService
{
    Task<List<RecipeViewDto>> GetAllRecipes();
    Task<RecipeViewDto> GetRecipe(int id);
    Task<RecipeViewDto> GetRecipeBySlug(string slug);
}