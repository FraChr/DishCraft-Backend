using DishCraft.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Service.Services;

public class LookupRepository : ILookupRepository
{
    private readonly Context _context;

    public LookupRepository(Context context)
    {
        _context = context;
    }
    
    public async Task<int?> GetTagIdByName(string name)
    {
        return await _context.Tags
            .Where(x => x.Name.ToLower() == name.ToLower())
            .Select(x => (int?)x.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<int?> GetDifficultyIdByName(string name)
    {
        return await _context.Difficulties
            .Where(x => x.Name.ToLower() == name.ToLower())
            .Select(x => (int?)x.Id)
            .FirstOrDefaultAsync();
    }
}