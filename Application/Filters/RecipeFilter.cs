using System.Security.AccessControl;

namespace Service.Filters;

public class RecipeFilter
{
    public string[]? Tags { get; set; }
    public string? Difficulty { get; set; }
    public string[]? Allergens { get; set; }
    public string? SearchTerm { get; set; }
}