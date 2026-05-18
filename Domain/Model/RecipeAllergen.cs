namespace DishCraft.Domain.Model;

public class RecipeAllergen
{
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    
    public int AllergenId { get; set; }
    public Allergen Allergen { get; set; }

}