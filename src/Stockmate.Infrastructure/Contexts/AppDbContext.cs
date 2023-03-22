using Microsoft.EntityFrameworkCore;
using Stockmate.Domain.Entities;

namespace Stockmate.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Products = Set<Product>();
        Suppliers = Set<Supplier>();
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (modelBuilder is null)
        {
            throw new ArgumentNullException(nameof(modelBuilder));
        }
    }
}
