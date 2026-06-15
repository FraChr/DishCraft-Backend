namespace Service.Filters;

public class RecipeRepoFilter
{
    public int? DifficultyId { get; set; }
    public List<int>? TagIds { get; set; }
    public List<int>? ExcludedAllergenIds { get; set; }
}