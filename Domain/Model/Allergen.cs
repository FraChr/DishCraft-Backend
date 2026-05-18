namespace DishCraft.Domain.Model;

public class Allergen
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<RecipeAllergen> RecipeAllergens { get; set; }
}