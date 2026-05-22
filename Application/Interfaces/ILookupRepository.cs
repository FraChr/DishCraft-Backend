using Service.Dtos;

namespace Service.Interfaces;

public interface ILookupRepository
{
    Task<int?> GetTagIdByName(string name);
    Task<int?> GetDifficultyIdByName(string name);
}