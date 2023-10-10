using API.Models.Authentication;
using API.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    public DbSet<Wine> Wines { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
}