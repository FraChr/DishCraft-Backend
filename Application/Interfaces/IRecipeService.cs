using DishCraft.Domain.Model;
using Service.Dtos;
using Service.Filters;

namespace Service.Interfaces;

public interface IRecipeService
{
    /*Task<List<RecipeViewDto>> GetAllRecipes();*/
    Task<RecipeViewDto> GetRecipe(int id);
    Task<RecipeViewDto> GetRecipeBySlug(string slug);
    Task<List<RecipeViewDto>> GetRecipes(RecipeFilter filter);
}