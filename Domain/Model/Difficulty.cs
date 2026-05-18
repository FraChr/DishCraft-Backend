namespace DishCraft.Domain.Model;

public class Difficulty
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<Recipe> Recipes { get; set; }
}