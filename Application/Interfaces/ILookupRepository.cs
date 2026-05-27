using Service.Dtos;

namespace Service.Interfaces;

public interface ILookupRepository
{
    Task<List<int>> GetTagIdByName(IEnumerable<string> names);
    Task<int> GetDifficultyIdByName(string name);
}