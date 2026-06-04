using DishCraft.Domain.Model;
using Service.Dtos;

namespace Service.Interfaces;

public interface ILookupRepository
{
    Task<List<int>> GetTagIdByName(IEnumerable<string> names);
    Task<int> GetDifficultyIdByName(string name);
    
    Task<List<Difficulty>> GetAllDifficultiesAsync();
    Task<List<Allergen>> GetAllAllergensAsync();
    Task<List<Tag>> GetAllTagsAsync();
}