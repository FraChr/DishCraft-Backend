using DishCraft_Api.Endpoints;
using DishCraft_Api.Extensions;
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
            .AddVueCors()
            .AddDatabase(builder.Configuration)
            .AddRepositories()
            .AddServices()
            .AddSeeders();

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();
        
        app.UseCors("AllowVueApp");

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<Context>();
            await db.Database.MigrateAsync();

            var seeder = scope.ServiceProvider
                .GetRequiredService<DbSeeder>();
            
            await seeder.SeedAsync();
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