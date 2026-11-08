using Microsoft.EntityFrameworkCore;
using RPGMarket.Application.Interfaces;
using RPGMarket.Domain.Entities;
using RPGMarket.Infrastructure.Data;

namespace RPGMarket.Infrastructure.Repositories;

// Implementamos la interfaz (La finca obedece a la receta)
public class UserRepository: IUsersRepository
{
    private readonly RPGMarketDbContext _context;

    // El constructor recibe el contexto de EF Core
    public UserRepository(RPGMarketDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Users>> GetAllAsync()
    {
        return await _context.UsersGlobales.ToListAsync(); // Consulta real a BD (Magia SQL)
    }
}
