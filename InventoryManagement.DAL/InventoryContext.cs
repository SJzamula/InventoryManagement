using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.DAL
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        // DbSet для інших моделей...

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the precision for the Price property in the Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price).HasPrecision(18, 2); // Example precision and scale
            });

            // Additional model configurations...
        }
    }
}
