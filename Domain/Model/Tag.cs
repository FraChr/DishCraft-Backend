namespace DishCraft.Domain.Model;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<RecipeTag> RecipeTags { get; set; }
}