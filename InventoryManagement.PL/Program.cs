using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.BLL; // Namespace for BLL
using InventoryManagement.DAL;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static void Main(string[] args)
    {

        // Set up Dependency Injection
        var services = new ServiceCollection();
        ConfigureServices(services);
        services.AddDbContext<InventoryContext>(options =>
            options.UseInMemoryDatabase("InventoryDatabase"));
        var serviceProvider = services.BuildServiceProvider();

        // Run the application
        var app = serviceProvider.GetService<App>();
        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Register your services, repositories, and context here
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ConsoleUI>();
        services.AddScoped<App>();

        // Add the DbContext configuration
        services.AddDbContext<InventoryContext>(options =>
            options.UseInMemoryDatabase("InventoryDatabase"));

        // ... Add other services and interfaces as needed
    }
}
