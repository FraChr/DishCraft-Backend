using DishCraft.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace DishCraft.Infrastructure.Repositories;

public class LookupRepository : ILookupRepository
{
    private readonly Context _context;

    public LookupRepository(Context context)
    {
        _context = context;
    }
    
    public async Task<List<int>> GetTagIdByName(IEnumerable<string> names)
    {
        var normalized = names
            .Select(x => x.ToLower())
            .ToList();

        return await _context.Tags
            .Where(x => normalized.Contains(x.Name.ToLower()))
            .Select(x => x.Id)
            .ToListAsync();
    }

    public async Task<int> GetDifficultyIdByName(string name)
    {
        
        var normalized = name.ToLower();
        
        var id = await _context.Difficulties
            .Where(x => normalized.Contains(x.Name.ToLower()))
            .Select(x => (int?)x.Id)
            .FirstOrDefaultAsync();

        if (id is null)
            throw new Exception($"Difficulty not found: {name}");
        
        return id.Value;
    }

    public Task<List<Difficulty>> GetAllDifficultiesAsync()
    {
        return _context.Difficulties.ToListAsync();
    }

    public Task<List<Allergen>> GetAllAllergensAsync()
    {
        return  _context.Allergens.ToListAsync();
    }

    public Task<List<Tag>> GetAllTagsAsync()
    {
        return _context.Tags.ToListAsync();
    }
}