namespace DishCraft.Domain.Model;

public class Unit
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
}