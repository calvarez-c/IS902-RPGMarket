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

        // 1. Configuramos el acceso a PostgreSQL
        services.AddDbContext<RPGMarketDbContext>(options =>
            options.UseNpgsql(connectionString));

        // 2. LA INYECCIÓN CLAVE (El contrato firmado por el Gerente):
        // "Cada vez que un Chef (Controlador) pida la Receta (IUsersRepository), 
        // entrégale los datos de la Finca PostgreSQL (UsersRepository)"
        services.AddScoped<IUsersRepository, UserRepository>();

        return services;
    }
}
