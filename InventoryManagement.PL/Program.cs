using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.BLL; // Namespace for BLL
using InventoryManagement.DAL;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static void Main(string[] args)
    {

        var services = new ServiceCollection();
        ConfigureServices(services);
        services.AddDbContext<InventoryContext>(options =>
            options.UseInMemoryDatabase("InventoryDatabase"));
        var serviceProvider = services.BuildServiceProvider();

        var app = serviceProvider.GetService<App>();
        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddSingleton<ICurrentOrderService, CurrentOrderService>();
        services.AddScoped<ConsoleUI>();
        services.AddScoped<App>();

        services.AddDbContext<InventoryContext>(options =>
            options.UseInMemoryDatabase("InventoryDatabase"));

    }
}
