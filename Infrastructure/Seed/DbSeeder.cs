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
        
        await _seeder.SeedAsync<Tag>(
            "Seed/Tags.json",
            _context.Tags,
            x => x.Name);
    }

    private async Task SeedRecipes()
    {
        var tagMap = await _context.Tags.ToDictionaryAsync(x => x.Name, x => x.Id);
        var allergenMap = await _context.Allergens.ToDictionaryAsync(x => x.Name, x => x.Id);

        await _seeder.SeedAsync<RecipeSeedDto, Recipe>(
            "Seed/Recipes.json",
            _context.Recipes,
            dto => MapRecipe(dto, tagMap, allergenMap),
            x => x.Name);

    }

    private Recipe MapRecipe(
        RecipeSeedDto dto,
        Dictionary<string, int> tagMap,
        Dictionary<string, int> allergenMap)
    {
        return new Recipe
        {
            Name = dto.Name,
            Slug = SlugGenerator.Slugify(dto.Name),
            DifficultyId = dto.DifficultyId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "DishCraft",

            Instructions = dto.Instructions.Select(i => new Instruction
            {
                StepsNumber = i.StepNumber,
                Text = i.Text
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