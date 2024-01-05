using System.Globalization;
using InventoryManagement.BLL;
using InventoryManagement.DAL;
public class ConsoleUI
{
    public void DisplayMenu()
    {
        Console.WriteLine("1: List all products");
        Console.WriteLine("2: Add a product");
        Console.WriteLine("3: Set a product quantity");
        Console.WriteLine("4: List all orders");
        Console.WriteLine("5: Create new order");
        Console.WriteLine("6: Process order");
        Console.WriteLine("7: List purchase queue");
        // More menu options
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
            Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}, Description: {product.Description}");
        }
    }

    public Product CaptureProductInfo()
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

        Console.WriteLine("Enter product image path:");
        string image = Console.ReadLine();

        Console.WriteLine("Enter product description:");
        string desc = Console.ReadLine();

        // Тут можна додати логіку для зчитування додаткових полів продукту, якщо потрібно

        return new Product { Name = name, Price = price, Image = image, Description = desc };
    }

    public void SetProductQuantity(IProductService productService)
    {
        Console.WriteLine("Enter product id:");
        int id;
        var culture = CultureInfo.InvariantCulture;

        while (!Int32.TryParse(Console.ReadLine(), NumberStyles.Any, culture, out id))
        {
            Console.WriteLine("Invalid input for product id. Please enter a valid integer number:");
        }

        Console.WriteLine("Enter new product quantity:");
        int quantity;

        while (!Int32.TryParse(Console.ReadLine(), NumberStyles.Any, culture, out quantity))
        {
            Console.WriteLine("Invalid input for product quantity. Please enter a valid integer number:");
        }

        productService.SetProductQuantity(id, quantity);
        Console.WriteLine("Try to process purchase queue...");
        productService.TryToProcessPurchaseQueue(id);
        var product = productService.GetProductById(id);
        if (product.Quantity < quantity)
        {
            Console.WriteLine("Puchase queue was changed!");
        }
        else
        {
            Console.WriteLine("Puchase queue was not changed!");
        }
    }

    public void DisplayOrders(IEnumerable<Order> orders)
    {
        foreach (var order in orders)
        {
            Console.WriteLine($"{order}");
            foreach (var orderItem in order.OrderItems)
            {
                Console.WriteLine($"    * {orderItem}");
            }
        }
    }

    public void CreateNewOrder(IOrderService orderService, IProductService productService,
        ICurrentOrderService currentOrderService)
    {
        Console.WriteLine("Enter products count:");
        int productsCount;
        var culture = CultureInfo.InvariantCulture;

        while (!Int32.TryParse(Console.ReadLine(), NumberStyles.Any, culture, out productsCount))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer number:");
        }

        for (int i = 0; i < productsCount; i++)
        {
            Console.WriteLine("Enter product id:");
            int id;

            while (!Int32.TryParse(Console.ReadLine(), NumberStyles.Any, culture, out id))
            {
                Console.WriteLine("Invalid input for product id. Please enter a valid integer number:");
            }

            var product = productService.GetProductById(id);

            Console.WriteLine("Enter new product quantity:");
            int quantity;

            while (!Int32.TryParse(Console.ReadLine(), NumberStyles.Any, culture, out quantity))
            {
                Console.WriteLine("Invalid input for product quantity. Please enter a valid integer number:");
            }

            currentOrderService.AddProduct(product, quantity);
        }
        orderService.AddOrder(currentOrderService.GetOrder());
        currentOrderService.ResetOrder();
    }

    public int GetOrderId()
    {
        Console.WriteLine("Enter order id:");
        int id;
        var culture = CultureInfo.InvariantCulture;

        while (!Int32.TryParse(Console.ReadLine(), NumberStyles.Any, culture, out id))
        {
            Console.WriteLine("Invalid input for order id. Please enter a valid integer number:");
        }

        return id;
    }

    public void DisplayPurchaseQueue(IEnumerable<PurchaseQueueItem> purchaseQueue)
    {
        foreach (var purchaseItem in purchaseQueue)
        {
            Console.WriteLine(purchaseItem);
        }
    }
}
