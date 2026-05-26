using DishCraft.Domain.Interfaces;
using DishCraft.Domain.Model;
using Service.Dtos;
using Service.Filters;
using Service.Interfaces;

namespace Service.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _repo;
    private readonly ILookupRepository _lookup;

    public RecipeService(IRecipeRepository repo, ILookupRepository lookup)
    {
        _repo = repo;
        _lookup = lookup;
    }

    public async Task<List<RecipeViewDto>> GetRecipes(RecipeFilter filter)
    {
        var repoFilter = new RecipeRepoFilter();
        
        if(!string.IsNullOrWhiteSpace(filter.Difficulty))
            repoFilter.DifficultyId = await _lookup.GetDifficultyIdByName(filter.Difficulty);
        
        if(!string.IsNullOrWhiteSpace(filter.Tag)) 
            repoFilter.TagId = await _lookup.GetTagIdByName(filter.Tag);
        
        var recipes = await _repo.GetFilteredAsync(repoFilter);
        
        return recipes.Select(BaseDto).ToList();
    }

    public async Task<RecipeViewDto?> GetRecipe(int id)
    {
        var recipe = await _repo.GetByIdAsync(id);
        return BaseDto(recipe);
    }

    public async Task<RecipeViewDto> GetRecipeBySlug(string slug)
    {
        var recipe = await _repo.GetBySlugAsync(slug);
        return BaseDto(recipe);
    }


    private RecipeViewDto BaseDto(Recipe recipe)
    {
        return new RecipeViewDto
        {
            /*Id = recipe.Id,*/
            Name = recipe.Name,
            CreatedBy =  recipe.CreatedBy,
            CreatedAt = recipe.CreatedAt,
            Instructions = recipe.Instructions
                .OrderBy(t => t.StepsNumber)
                .Select(t => new InstructionDto
                {
                    StepNumber = t.StepsNumber,
                    Text = t.Text,
                }).ToList(),
            Difficulty = recipe.Difficulty.Name,
            Tags = recipe.RecipeTags
                .Select(t => t.Tag.Name)
                .ToList(),
            Allergens = recipe.RecipeAllergens
                .Select(t => t.Allergen.Name)
                .ToList(),
            Ingredients = recipe.RecipeIngredients
                .Select(t => t.Ingredient.Name)
                .ToList()
        };
    }
}