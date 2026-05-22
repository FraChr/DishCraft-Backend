using System.Text.Json;
using DishCraft.Domain.Model;
using Microsoft.EntityFrameworkCore;

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
        
        await _context.SaveChangesAsync();
        
        await SeedRecipeAsync();
        
        await _context.SaveChangesAsync();
    }

    private async Task SeedRecipeAsync()
    {
        if (await _context.Recipes.AnyAsync())
            return;

        var tagMap = await _context.Tags.ToDictionaryAsync(x => x.Name, x => x.Id);
        var allergenMap = await _context.Allergens.ToDictionaryAsync(x => x.Name, x => x.Id);
        
        var recipes = RecipeSeeder.GetRecipes(tagMap, allergenMap);
        
        await _context.Recipes.AddRangeAsync(recipes);
    }
}