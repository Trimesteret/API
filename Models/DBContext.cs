using API.Models.Authentication;
using API.Models.Items;
using API.Models.Orders;
using API.Models.Suppliers;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {

    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Liquor> Liquors { get; set; }
    public DbSet<DefaultItem> Chocolates { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Wine> Wines { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Order> Order { get; set; }
}
