using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.BLL; // Namespace for BLL
using InventoryManagement.DAL;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"ConnectionStrings:InventoryDBConnectionString", "Server=InventoryManagementServer;Database=InventoryManagementDB;User Id=256;Password=hello;"}
                })
            .Build();
        // Set up Dependency Injection
        var services = new ServiceCollection();
        ConfigureServices(services, builder);
        services.AddSingleton<IConfiguration>(builder);
        services.AddDbContext<InventoryContext>(options =>
            options.UseSqlServer(builder.GetConnectionString("InventoryDBConnectionString")));
        var serviceProvider = services.BuildServiceProvider();

        // Run the application
        var app = serviceProvider.GetService<App>();
        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Register your services, repositories, and context here
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ConsoleUI>();
        services.AddScoped<App>();

        // Add the DbContext configuration
        services.AddDbContext<InventoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("InventoryDBConnectionString")));

        // ... Add other services and interfaces as needed
    }
}
