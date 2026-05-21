using Service.Interfaces;

namespace DishCraft_Api.Endpoints;

public static class RecipeEndpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/recipes");

        group.MapGet("/", async (IRecipeService service) =>
        {
            var result = await service.GetAllRecipes();
            return Results.Ok(result);
        });

        group.MapGet("/{id}", async (int id, IRecipeService service) =>
        {
            var result =  await service.GetRecipe(id);
            return Results.Ok(result);
        });
    }

}