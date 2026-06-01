namespace Service.Dtos;

public class RecipeSeedDto
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public int DifficultyId { get; set; }

    public List<string> Tags { get; set; } = [];
    public List<string> Allergens { get; set; } = [];
    public List<InstructionDto> Instructions { get; set; } =  [];
    public List<IngredientDto> Ingredients { get; set; } = [];
}