using DishCraft.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DishCraft.Infrastructure;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }
    
    public DbSet<Allergen> Allergens { get; set; }
    public DbSet<Difficulty>  Difficulties { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Nutrition> Nutrition { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeAllergen> RecipeAllergens { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public DbSet<RecipeNutrition> RecipeNutritions { get; set; }
    public DbSet<RecipeTag> RecipeTags { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Unit> Units { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<RecipeAllergen>()
            .HasKey(x => new { x.AllergenId, x.RecipeId });
        
        modelBuilder.Entity<RecipeNutrition>()
            .HasKey(x => new { x.RecipeId, x.NutritionId });
        
        modelBuilder.Entity<RecipeTag>()
            .HasKey(x => new { x.RecipeId, x.TagId });
        
        modelBuilder.Entity<RecipeIngredient>()
            .HasKey(x => new { x.RecipeId, x.IngredientId });
    }
}