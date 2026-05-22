using Service.Filters;
using Service.Interfaces;

namespace DishCraft_Api.Endpoints;

public static class RecipeEndpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/recipes");

        group.MapGet("/", async (
            [AsParameters] RecipeFilter filter,
            IRecipeService service) =>
        {
            var result = await service.GetRecipes(filter);
            return Results.Ok(result);
            /*var result = await service.GetAllRecipes();
            return Results.Ok(result);*/
        });

        group.MapGet("/{id}", async (int id, IRecipeService service) =>
        {
            var result =  await service.GetRecipe(id);
            return Results.Ok(result);
        });

        group.MapGet("/slug/{slug}", async (string slug, IRecipeService service) =>
        {
            var result = await service.GetRecipeBySlug(slug);
            return Results.Ok(result);
        });
    }

}