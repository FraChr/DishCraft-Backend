using Service.Dtos;

namespace Service.Interfaces;

public interface ILookupService
{
    Task<List<LookupDto>> GetDifficulties();
    Task<List<LookupDto>> GetAllergens();
    Task<List<LookupDto>> GetTags();
}