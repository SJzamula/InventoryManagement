using InventoryManagement.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class InventoryContextFactory : IDesignTimeDbContextFactory<InventoryContext>
{
    public InventoryContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<InventoryContext>();
        optionsBuilder.UseInMemoryDatabase("InventoryDatabase");

        return new InventoryContext(optionsBuilder.Options);
    }
}
