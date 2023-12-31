using API.Enums;
using API.Models.Authentication;
using API.Models.Items;
using API.Models.Orders;
using API.Models.Suppliers;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class SharedContext : DbContext
{
    public SharedContext(DbContextOptions<SharedContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomEnum>().HasData(
            new CustomEnum { Id = 1, EnumType = EnumType.SuitableFor, Key = "Poultry", Value = "Fjerkræ" },
            new CustomEnum { Id = 2, EnumType = EnumType.SuitableFor, Key = "Seafood", Value = "Skaldyr" },
            new CustomEnum { Id = 3, EnumType = EnumType.SuitableFor, Key = "RedMeat", Value = "Oksekød" },
            new CustomEnum { Id = 4, EnumType = EnumType.SuitableFor, Key = "Pork", Value = "Svinekød" },
            new CustomEnum { Id = 5, EnumType = EnumType.SuitableFor, Key = "SpicyFood", Value = "Stærk mad" },
            new CustomEnum { Id = 6, EnumType = EnumType.SuitableFor, Key = "Cheese", Value = "Ost" },
            new CustomEnum { Id = 7, EnumType = EnumType.SuitableFor, Key = "Pasta", Value = "Pasta" },
            new CustomEnum { Id = 8, EnumType = EnumType.SuitableFor, Key = "Pizza", Value = "Pizza" },
            new CustomEnum { Id = 9, EnumType = EnumType.SuitableFor, Key = "Vegetarian", Value = "Vegetar" },
            new CustomEnum { Id = 10, EnumType = EnumType.SuitableFor, Key = "Salad", Value = "Salat" },
            new CustomEnum { Id = 11, EnumType = EnumType.SuitableFor, Key = "Dessert", Value = "Dessert" },

            new CustomEnum { Id = 12, EnumType = EnumType.WineType, Key = "RedWine", Value = "Rødvin" },
            new CustomEnum { Id = 13, EnumType = EnumType.WineType, Key = "WhiteWine", Value = "Hvidvin" },
            new CustomEnum { Id = 14, EnumType = EnumType.WineType, Key = "RoseWine", Value = "Rosévin" },

            new CustomEnum { Id = 15, EnumType = EnumType.LiqourType, Key = "Whiskey", Value = "Whiskey" },
            new CustomEnum { Id = 16, EnumType = EnumType.LiqourType, Key = "Vodka", Value = "Vodka" },
            new CustomEnum { Id = 17, EnumType = EnumType.LiqourType, Key = "Gin", Value = "Gin" },
            new CustomEnum { Id = 18, EnumType = EnumType.LiqourType, Key = "Rum", Value = "Rom" },
            new CustomEnum { Id = 19, EnumType = EnumType.LiqourType, Key = "Tequila", Value = "Tequila" },
            new CustomEnum { Id = 20, EnumType = EnumType.LiqourType, Key = "Liqueur", Value = "Likør" }
        );
    }


    public DbSet<Employee> Employees { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Liquor> Liquors { get; set; }
    public DbSet<DefaultItem> DefaultItems { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Wine> Wines { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<InboundOrder> InboundOrders { get; set; }
    public DbSet<SupplierItemRelation> SupplierItemRelations { get; set; }
    public DbSet<CustomEnum> CustomEnums { get; set; }
    public DbSet<ItemEnumRelation> ItemEnumRelations { get; set; }
}
