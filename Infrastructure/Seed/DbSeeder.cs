using System.Text.Json;
using DishCraft.Domain.Model;
using DishCraft.Domain.Utility;
using Microsoft.EntityFrameworkCore;
using Service.Dtos;

namespace DishCraft.Infrastructure.Seed;

public class DbSeeder
{
    private readonly JsonSeeder _seeder;
    private readonly Context _context;

    public DbSeeder(Context context, JsonSeeder seeder)
    {
        _context = context;
        _seeder = seeder;
    }

    public async Task SeedAsync()
    {
        await SeedLookups();
        await SeedRecipes();
    }

    private async Task SeedLookups()
    {
        await _seeder.SeedAsync<Unit>(
            "Seed/Units.json",
            _context.Units,
            x => x.Code);

        await _seeder.SeedAsync<Allergen>(
            "Seed/Allergens.json",
            _context.Allergens,
            x => x.Name);
        
        await _seeder.SeedAsync<Difficulty>(
            "Seed/Difficulty.json",
            _context.Difficulties,
            x => x.Name);
        
        await _seeder.SeedAsync<Ingredient>(
            "Seed/Ingredients.json",
            _context.Ingredients,
            x => x.Name);
        
        await _seeder.SeedAsync<Tag>(
            "Seed/Tags.json",
            _context.Tags,
            x => x.Name);
    }

    private async Task SeedRecipes()
    {
        var tagMap = await _context.Tags.ToDictionaryAsync(x => x.Name, x => x.Id);
        var allergenMap = await _context.Allergens.ToDictionaryAsync(x => x.Name, x => x.Id);
        var ingredientMap = await _context.Ingredients.ToDictionaryAsync(x => 
                x.Name, x => x.Id, 
            StringComparer.OrdinalIgnoreCase);
        
        var unitMap = await _context.Units.ToDictionaryAsync(x => x.Code, x => x.Id);
        
        var existingSlug = new HashSet<string>();

        await _seeder.SeedAsync<RecipeSeedDto, Recipe>(
            "Seed/Recipes.json",
            _context.Recipes,
            dto => MapRecipe(dto, tagMap, allergenMap, ingredientMap, unitMap, existingSlug),
            x => x.Name);

    }

    private Recipe MapRecipe(
        RecipeSeedDto dto,
        Dictionary<string, int> tagMap,
        Dictionary<string, int> allergenMap,
        Dictionary<string, int> ingredientMap,
        Dictionary<string, int> unitMap,
        HashSet<string> existingSlug)
    {

        var baseSlug = SlugGenerator.Slugify(dto.Name);
        var slug = baseSlug;
        const int uuidCharLength = 10;

        while (existingSlug.Contains(slug))
        {
            slug = $"{baseSlug}-{Guid.NewGuid().ToString()[..uuidCharLength]}";
        }
        
        existingSlug.Add(slug);

    return new Recipe
        {
            Name = dto.Name,
            Uuid = Guid.NewGuid().ToString(),
            Slug = slug,
            DifficultyId = dto.DifficultyId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "DishCraft",

            Instructions = dto.Instructions.Select(i => new Instruction
            {
                StepsNumber = i.StepNumber,
                Text = i.Text
            }).ToList(),
            
            RecipeIngredients = dto.Ingredients.Select(x =>
            {
                if(!ingredientMap.TryGetValue(x.Ingredient, out var ingredientId))
                    throw new InvalidOperationException($"Seed error: Ingredient '{x.Ingredient}' does not exist");
                
                if(!unitMap.TryGetValue(x.Unit, out var unitId))
                    throw new InvalidOperationException($"Seed error: Unit '{x.Unit}' does not exist");
                
                return new RecipeIngredient
                {
                    IngredientId = ingredientId,
                    UnitId = unitId,
                    Amount = x.Amount
                };
            }).ToList(),

            RecipeTags = dto.Tags.Select(x =>
            {
                if (!tagMap.TryGetValue(x, out var tagId))
                    throw new InvalidOperationException($"Seed error: Tag '{x}' does not exist");
                
                return new RecipeTag{ TagId = tagId };
            }).ToList(),

            RecipeAllergens = dto.Allergens.Select(x =>
            {
                if (!allergenMap.TryGetValue(x, out var allergenId))
                    throw new InvalidOperationException($"Seed error: Allergen '{x}' does not exist");
                
                return new RecipeAllergen{ AllergenId = allergenId };
            }).ToList()
        };
    }
}