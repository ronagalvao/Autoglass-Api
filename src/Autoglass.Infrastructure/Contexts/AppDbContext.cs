using Microsoft.EntityFrameworkCore;
using Autoglass.Domain.Entities;

namespace Autoglass.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aqui pode ser definido o mapeamento das entidades e suas propriedades
            // Exemplo: modelBuilder.Entity<Produto>().Property(p => p.Nome).HasMaxLength(100);
            base.OnModelCreating(modelBuilder);
        }
    }
}
