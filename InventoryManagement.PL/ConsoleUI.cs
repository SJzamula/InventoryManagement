using System.Globalization;
using InventoryManagement.DAL;
public class ConsoleUI
{
    public void DisplayMenu()
    {
        Console.WriteLine("1: List all products");
        Console.WriteLine("2: Add a product");
        Console.WriteLine("Type 'exit' to close the application.");
    }

    public string GetUserInput()
    {
        return Console.ReadLine();
    }

    public void DisplayProducts(IEnumerable<Product> products)
    {
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price}");
        }
    }

    public Product  CaptureProductInfo()
    {
        Console.WriteLine("Enter product name:");
        string name = Console.ReadLine();

        Console.WriteLine("Enter product price:");
        decimal price;
        var culture = CultureInfo.InvariantCulture;

        while (!decimal.TryParse(Console.ReadLine(), NumberStyles.Any, culture, out price))
        {
            Console.WriteLine("Invalid input for price. Please enter a valid decimal number:");
        }


        return new Product { Name = name, Price = price };
    }
}
