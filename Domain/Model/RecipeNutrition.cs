namespace DishCraft.Domain.Model;

public class RecipeNutrition
{
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    
    public int NutritionId { get; set; }
    public Nutrition Nutrition { get; set; }
}