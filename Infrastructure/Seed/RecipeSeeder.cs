using DishCraft.Domain.Model;

namespace DishCraft.Infrastructure.Seed;

public static class RecipeSeeder
{
    public static List<Recipe> GetRecipes(
        Dictionary<string, int> tagMap,
        Dictionary<string, int> allergenMap)
    {
        return
        [
            new Recipe
            {
              Name = "Dish Craft",
              CreatedAt = DateTime.UtcNow,
              CreatedBy = "DishCraft",
              DifficultyId = 1
            },
            
            new Recipe
            {
                Name = "Steak",
                DifficultyId = 2,
                CreatedBy = "DishCraft",
                CreatedAt =  DateTime.UtcNow,
                Instructions = new List<Instruction>
                {
                    new() { StepsNumber = 1, Text = "Take steak out of fridge and let it rest" },
                    new() { StepsNumber = 2, Text = "Season generously with salt and pepper" },
                    new() { StepsNumber = 3, Text = "Sear in the pan for 2-3 minutes per side" }
                },
                RecipeTags = new List<RecipeTag>
                {
                    new() { TagId = tagMap["Dinner"] },
                    new() { TagId = tagMap["High Protein"] },
                },
                RecipeAllergens = new List<RecipeAllergen>
                {
                    new() { AllergenId = allergenMap["Mustard"] },
                }
            }
        ];
    }
}