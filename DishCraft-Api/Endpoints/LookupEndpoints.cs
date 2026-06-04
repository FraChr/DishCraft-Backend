using Microsoft.AspNetCore.Components.Web;
using Service.Interfaces;

namespace DishCraft_Api.Endpoints;

public static class LookupEndpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/lookup");

        group.MapGet("/difficulties", async (ILookupService service) =>
        {
            try
            {
                var result = await service.GetDifficulties();
                return Results.Ok(result);
            }
            catch (Exception error)
            {
                return Results.Problem(
                    title: "Failed to retrive difficulties!",
                    detail: error.Message,
                    statusCode: 500
                    );

            }
        });

        group.MapGet("/allergens", async (ILookupService service) =>
        {
            try
            {
                var result = await service.GetAllergens();
                return Results.Ok(result);
            }
            catch (ApplicationException error)
            {
                return Results.Problem(
                    title: "Failed to retrive allergens!",
                    detail: error.Message,
                    statusCode: 500
                    );
            }
        });

        group.MapGet("/tags", async (ILookupService service) =>
        {
            try
            {
                var result = await service.GetTags();
                return Results.Ok(result);
            }
            catch (Exception error)
            {
                return Results.Problem(
                    title: "Failed to retrive tags!",
                    detail: error.Message,
                    statusCode: 500
                    );
            }
        });
    }
}