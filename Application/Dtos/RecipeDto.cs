namespace Service.Dtos;

public class RecipeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Difficulty { get; set; }

    public List<InstructionDto> Instructions { get; set; }
    public List<string> Tags { get; set; }
    public List<string> Ingredients { get; set; }
    public List<string> Allergens { get; set; }
}