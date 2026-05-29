namespace Service.Dtos;

public class RecipeViewDto
{
    /*public int Id { get; set; }*/
    public string Name { get; set; }
    public string Uuid { get; set; }
    public string Difficulty { get; set; }
    public string Slug { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }

    public List<InstructionDto> Instructions { get; set; }
    public List<string> Tags { get; set; }
    public List<string> Ingredients { get; set; }
    public List<string> Allergens { get; set; }
}