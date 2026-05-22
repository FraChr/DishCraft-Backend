namespace DishCraft.Domain.Model;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public int DifficultyId { get; set; }
    public Difficulty Difficulty { get; set; }
    
    public ICollection<RecipeAllergen> RecipeAllergens { get; set; }

    public ICollection<Instruction> Instructions { get; set; }
    
    public ICollection<RecipeTag> RecipeTags { get; set; }
    
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; }

    public ICollection<RecipeNutrition> RecipeNutritions { get; set; }
}