using RPGMarket.Domain.Entities;

namespace RPGMarket.Application.Interfaces;

public interface IUsersRepository
{
    Task<IEnumerable<Users>> GetAllAsync();
}
