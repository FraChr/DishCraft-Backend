using DishCraft.Domain.Interfaces;
using DishCraft.Domain.Model;
using Service.Dtos;
using Service.Filters;
using Service.Interfaces;

namespace Service.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepo;
    private readonly ILookupRepository _lookupRepo;

    public RecipeService(IRecipeRepository recipeRepo, ILookupRepository lookupRepo)
    {
        _recipeRepo = recipeRepo;
        _lookupRepo = lookupRepo;
    }

    public async Task<List<RecipeViewDto>> GetRecipes(RecipeFilter filter)
    {
        var repoFilter = new RecipeRepoFilter();
        
        if(!string.IsNullOrWhiteSpace(filter.Difficulty))
            repoFilter.DifficultyId = await _lookupRepo.GetDifficultyIdByName(filter.Difficulty);
        
        
        if(filter.Tags?.Length > 0)
        {
            repoFilter.TagIds = await _lookupRepo.GetTagIdByName(filter.Tags);
        }
        
        if (filter.Allergens?.Length > 0)
        {
            /*repoFilter.ExcludedAllergenIds = await _lookupRepo.GetAllergenIdByName(filter.Allergens);*/
            
            var allergenIds = await _lookupRepo.GetAllergenIdByName(filter.Allergens);
            
            System.Console.WriteLine($"Allergens: {string.Join(", ", filter.Allergens)}");
            System.Console.WriteLine($"AllergenIds count: {allergenIds.Count}");
            System.Console.WriteLine($"AllergenIds: {string.Join(", ", allergenIds)}");
            
            repoFilter.ExcludedAllergenIds = allergenIds;
        }
        
        var recipes = await _recipeRepo.GetFilteredAsync(repoFilter);
        
        return recipes.Select(BaseDto).ToList();
    }

    public async Task<RecipeViewDto?> GetRecipe(int id)
    {
        var recipe = await _recipeRepo.GetByIdAsync(id);
        return BaseDto(recipe);
    }

    public async Task<RecipeViewDto> GetRecipeBySlug(string slug)
    {
        var recipe = await _recipeRepo.GetBySlugAsync(slug);
        return BaseDto(recipe);
    }


    private RecipeViewDto BaseDto(Recipe recipe)
    {
        return new RecipeViewDto
        {
            Name = recipe.Name,
            Uuid = recipe.Uuid,
            CreatedBy =  recipe.CreatedBy,
            CreatedAt = recipe.CreatedAt,
            Slug = recipe.Slug,
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
                .Select(t => new IngredientDto
                {
                    Ingredient = t.Ingredient.Name,
                    Unit = t.Unit.Code,
                    Amount = t.Amount
                }).ToList()
        };
    }
}