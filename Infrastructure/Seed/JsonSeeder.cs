using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace DishCraft.Infrastructure.Seed;

public class JsonSeeder
{
    private readonly Context _context;

    public JsonSeeder(Context context)
    {
        _context = context;
    }

    public async Task SeedAsync<T>(
        string fileName,
        DbSet<T> dbSet,
        Func<T, string>? keySelector = null)
        where T : class
    {
        
        var path = Path.Combine(AppContext.BaseDirectory, fileName);
        var json = await File.ReadAllTextAsync(path);
        var items = JsonSerializer.Deserialize<List<T>>(json) ?? [];

        if (keySelector == null)
        {
            dbSet.AddRange(items);
            await _context.SaveChangesAsync();
            return;
        }
        
        var exitingKeys = await dbSet
            .Select(x => keySelector(x))
            .ToListAsync();

        var newItems = items
            .Where(i => !exitingKeys.Contains(keySelector(i)))
            .ToList();
        
        dbSet.AddRange(newItems);
    }
}