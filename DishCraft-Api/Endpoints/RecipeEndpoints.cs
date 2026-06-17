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
        });

        group.MapGet("/{slug}", async (string slug, IRecipeService service) =>
        {
            var result = await service.GetRecipeBySlug(slug);
            return Results.Ok(result);
        });
    }
}