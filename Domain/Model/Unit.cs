namespace DishCraft.Domain.Model;

public class Unit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
}