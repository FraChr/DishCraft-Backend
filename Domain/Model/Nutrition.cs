namespace DishCraft.Domain.Model;

public class Nutrition
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    
    public int UnitId { get; set; }
    public Unit Unit { get; set; }

    public ICollection<RecipeNutrition> RecipeNutritions { get; set; }
}