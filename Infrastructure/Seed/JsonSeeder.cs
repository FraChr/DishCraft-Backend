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

    public async Task SeedAsync<TEntity>(
        string fileName,
        DbSet<TEntity> dbSet,
        Func<TEntity, string> keySelector)
        where TEntity : class
    {
        
        var path = Path.Combine(AppContext.BaseDirectory, fileName);
        var json = await File.ReadAllTextAsync(path);
        var items = JsonSerializer.Deserialize<List<TEntity>>(json) ?? [];
        
        await SaveNewEntities(dbSet, items, keySelector);
        

        /*if (keySelector == null)
        {
            dbSet.AddRange(items);
            await _context.SaveChangesAsync();
            return;
        }*/
        
        /*
        var exitingKeys = await dbSet
            .Select(x => keySelector(x))
            .ToListAsync();

        var newItems = items
            .Where(i => !exitingKeys.Contains(keySelector(i)))
            .ToList();
            */
        
        /*dbSet.AddRange(newItems);*/
    }

    public async Task SeedAsync<TDto, TEntity>(
        string fileName,
        DbSet<TEntity> dbSet,
        Func<TDto, TEntity> map,
        Func<TEntity, string> keySelector)
        where TEntity : class
    {
        var path = Path.Combine(AppContext.BaseDirectory, fileName);
        var json = await File.ReadAllTextAsync(path);

        var dtos = JsonSerializer.Deserialize<List<TDto>>(json) ?? [];
        
        var entities = dtos.Select(map).ToList();
        
        await SaveNewEntities(dbSet, entities, keySelector);
        
    }
    
    private async Task SaveNewEntities<TEntity>(
        DbSet<TEntity> dbSet,
        List<TEntity> entities,
        Func<TEntity, string> keySelector)
        where TEntity : class
    {
        var existingKeys = await dbSet
            .Select(x => keySelector(x))
            .ToListAsync();
        
        var newItems = entities
            .Where(x => !existingKeys.Contains(keySelector(x)))
            .ToList();
        
        dbSet.AddRange(newItems);
        await _context.SaveChangesAsync();
    }
}