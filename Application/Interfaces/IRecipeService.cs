using DishCraft.Domain.Model;
using Service.Dtos;

namespace Service.Interfaces;

public interface IRecipeService
{
    Task<List<RecipeDto>> GetAllRecipes();
    Task<RecipeDto> GetRecipe(int id);
}