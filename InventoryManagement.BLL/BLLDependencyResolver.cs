using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.DAL;
using InventoryManagement.BLL;

public static class BLLDependencyResolver
{
    public static void ConfigureDependencies(IServiceCollection services)
    {
        // DAL dependencies
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // DbContext configuration
        services.AddDbContext<InventoryContext>(options =>
            options.UseInMemoryDatabase("InventoryDatabase"));

        // BLL dependencies
        services.AddScoped<IProductService, ProductService>();
    }
}
