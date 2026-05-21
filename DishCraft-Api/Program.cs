using DishCraft_Api.Endpoints;
using DishCraft_Api.Extensions;
using DishCraft.Domain.Model;
using DishCraft.Infrastructure;
using DishCraft.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

namespace DishCraft_Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services
            .AddDatabase(builder.Configuration)
            .AddRepositories()
            .AddServices()
            .AddSeeders();

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
                        DifficultyId = 1,
                    },
                    new Recipe
                    {
                        Name = "Steak",
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "DishCraft",
                        DifficultyId = 2,
                        
                        Instructions = new List<Instruction>
                        {
                            new()
                            {
                                StepsNumber = 1,
                                Text = "Take steak out of fridge and let it rest"
                            },
                            new()
                            {
                                StepsNumber = 2,
                                Text = "Season generously with salt and pepper"
                            },
                            new()
                            {
                                StepsNumber = 3,
                                Text = "Sear in the pan for 2-3 minutes per side"
                            }
                        },
                        
                        RecipeTags = new List<RecipeTag>
                        {
                            new() { TagId = tag1.Id, },
                            new() { TagId = tag2.Id, }
                        },
                        
                        RecipeAllergens = new List<RecipeAllergen>
                        {
                            new() { AllergenId = allergen.Id, },
                        },
                        
                    }
                );
                
                db.SaveChanges();
            }
        }
        
        RecipeEndpoints.Map(app);
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.Run();
    }
}