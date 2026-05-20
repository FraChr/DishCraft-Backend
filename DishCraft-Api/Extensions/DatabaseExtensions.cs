using DishCraft.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DishCraft_Api.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<Context>(options => 
            options.UseNpgsql(
                config.GetConnectionString("DefaultConnection")));
        
        return services;
    }
}