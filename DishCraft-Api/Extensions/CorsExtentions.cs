namespace DishCraft_Api.Extensions;

public static class CorsExtentions
{
    public static IServiceCollection AddVueCors(
        this IServiceCollection services,
        IConfiguration config)
    {
        
        var allowedOrigins = config.GetSection("Cors:AllowedOrigins")
            .Get<string[]>() ?? [];
        
        services.AddCors(options =>
        {
            options.AddPolicy("AllowVueApp", policy =>
            {
                policy.WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        
        return services;
    }
}