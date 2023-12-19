using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.DAL; // Namespace for DAL
using InventoryManagement.BLL; // Namespace for BLL

public static class BLLDependencyResolver
{
    public static void ConfigureDependencies(IServiceCollection services)
    {
        // DAL dependencies
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        // DbContext configuration
        services.AddDbContext<InventoryContext>(options => 
            options.UseSqlServer("InventoryDBConnectionString"));
        
        // BLL dependencies
        services.AddScoped<IProductService, ProductService>();

        // ... Add other services and interfaces as needed
    }
}
