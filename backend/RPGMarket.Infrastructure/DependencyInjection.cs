using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RPGMarket.Application.Interfaces;
using RPGMarket.Infrastructure.Data;
using RPGMarket.Infrastructure.Repositories;

namespace RPGMarket.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<RPGMarketDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IItemRepository, ItemRepository>();
        return services;
    }
}
