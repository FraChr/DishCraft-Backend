using Service.Dtos;
using Service.Interfaces;

namespace Service.Services;

public class LookupService : ILookupService
{
    private readonly ILookupRepository _lookupRepo;


    public LookupService(ILookupRepository lookupRepo)
    {
        _lookupRepo = lookupRepo;
    }

    public async Task<List<LookupDto>> GetDifficulties()
    {
        var items = await _lookupRepo.GetAllDifficultiesAsync();
        return items.Select(x => new LookupDto
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }

    public async Task<List<LookupDto>> GetAllergens()
    {
        var items = await _lookupRepo.GetAllAllergensAsync();
        return items.Select(x => new LookupDto
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }

    public async Task<List<LookupDto>> GetTags()
    {
        var items = await _lookupRepo.GetAllTagsAsync();
        return items.Select(x => new LookupDto
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }
}