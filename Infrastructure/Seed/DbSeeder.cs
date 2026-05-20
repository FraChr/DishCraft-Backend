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


        /*var json = await File.ReadAllTextAsync("Seed/Units.json");

        var data = JsonSerializer.Deserialize<List<Unit>>(json);

        foreach (var item in data)
        {
            var existing = await _context.Units
                .FirstOrDefaultAsync(x => x.Code == item.Code);

            if (existing == null)
            {
                _context.Units.Add(item);
            }

            await _context.SaveChangesAsync();
        }*/
    }
}