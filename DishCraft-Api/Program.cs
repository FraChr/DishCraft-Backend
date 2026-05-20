using DishCraft.Domain.Interfaces;
using DishCraft.Domain.Model;
using DishCraft.Infrastructure;
using DishCraft.Infrastructure.Repositories;
using DishCraft.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Service.Services;

namespace DishCraft_Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<Context>(options => 
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection")));
        
        builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();

        builder.Services.AddScoped<RecipeService>();
        builder.Services.AddScoped<DbSeeder>();
        builder.Services.AddScoped<JsonSeeder>();

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<Context>();
            await db.Database.MigrateAsync();

            var seeder = scope.ServiceProvider
                .GetRequiredService<DbSeeder>();
            
            await seeder.SeedAsync();


            if (!db.Recipes.Any())
            {
                var tag1 = db.Tags.First(t => t.Name == "Dinner");
                var tag2 = db.Tags.First(t => t.Name == "High Protein");
                
                var allergen = db.Allergens.First(a => a.Name == "Mustard");
                
                
                db.Recipes.AddRange(
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
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "DishCraft",
                        DifficultyId = 2,
                        
                        RecipeTags = new List<RecipeTag>
                        {
                            new RecipeTag { TagId = tag1.Id, },
                            new RecipeTag { TagId = tag2.Id, }
                        },
                        
                        RecipeAllergens = new List<RecipeAllergen>
                        {
                            new RecipeAllergen { AllergenId = allergen.Id, },
                        },
                        
                    }
                );
                
                db.SaveChanges();
            }
        }
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapGet("/recipes/{id}", async (int id, RecipeService service) =>
        {
            var result = await service.GetRecipe(id);
            
            return result is null
                ? Results.NotFound()
                : Results.Ok(result);
        });

        app.MapGet("/recipes", async (RecipeService service) =>
        {
            var result = await service.GetAllRecipes();
            
            return Results.Ok(result);
        });
        
        app.Run();
    }
}