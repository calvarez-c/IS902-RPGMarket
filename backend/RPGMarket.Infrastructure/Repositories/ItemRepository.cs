using Microsoft.EntityFrameworkCore;
using RPGMarket.Application.Interfaces;
using RPGMarket.Domain.Entities;
using RPGMarket.Infrastructure.Data;

namespace RPGMarket.Infrastructure.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly RPGMarketDbContext _context;

    public ItemRepository(RPGMarketDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Item>> GetAllAsync() =>
        await _context.Items.ToListAsync();

    public async Task<Item?> GetByIdAsync(int id) =>
        await _context.Items.FindAsync(id);

    public async Task<Item> AddAsync(Item item)
    {
        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task UpdateAsync(Item item)
    {
        _context.Items.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item != null)
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
