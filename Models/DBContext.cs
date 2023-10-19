using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Liquor> Liquors { get; set; } = null!;
    public DbSet<Chocolate> Chocolates { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<Wine> Wines { get; set; } = null!;
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<Order> Order { get; set; } = null!;
}
