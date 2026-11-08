using Microsoft.EntityFrameworkCore;
using RPGMarket.Domain.Entities;

namespace RPGMarket.Infrastructure.Data;

public class RPGMarketDbContext : DbContext
{
    public RPGMarketDbContext(DbContextOptions<RPGMarketDbContext> options) : base(options) { }

    public DbSet<User> Users {get; set;}
    public DbSet<Item> Items {get; set;}
    public DbSet<Listing> Listings {get; set;} 
}
