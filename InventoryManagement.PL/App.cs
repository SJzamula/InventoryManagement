using InventoryManagement.BLL; // Namespace for BLL
using InventoryManagement.DAL;
using System;

public class App
{
    private readonly IProductService _productService;
    private readonly ConsoleUI _consoleUI;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderService _orderService;
    private readonly ICurrentOrderService _currentOrderService;

    Repository<Product> listOfProducts;

    public App(IProductService productService, ConsoleUI consoleUI, IUnitOfWork unitOfWork,
        IOrderService orderService, ICurrentOrderService currentOrderService)
    {
        _productService = productService;
        _consoleUI = consoleUI;
        _unitOfWork = unitOfWork;
        _orderService = orderService;
        _currentOrderService = currentOrderService;
    }

    public void Run()
    {
        bool exit = false;
        _productService.CreateProduct(new Product { Name = "Lenovo IdeaPad Slim 3", Price = 18999, Image = "images/lenovo_82XQ009HRA.jpg", Description = "15.6 IPS (1920x1080) Full HD,  AMD Ryzen 5 7520U (2.8 - 4.3) / RAM 16 / SSD 512 / AMD Radeon 610M Graphics / Wi-Fi / Bluetooth", Quantity = 5 });
        while (!exit)
        {
            _consoleUI.DisplayMenu();
            var input = _consoleUI.GetUserInput();
            switch (input)
            {
                case "1":
                    var products = _productService.GetAllProducts();
                    _consoleUI.DisplayProducts(products);
                    break;
                case "2":
                    try
                    {
                        var newProduct = _consoleUI.CaptureProductInfo();
                        _productService.CreateProduct(newProduct);
                        _unitOfWork.Complete(); // Inserts the new product into the database and sets ProductId
                        Console.WriteLine("Product added successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;
                case "3":
                    try
                    {
                        _consoleUI.SetProductQuantity(_productService);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;
                case "4":
                    _consoleUI.DisplayOrders(_orderService.GetAllOrders());
                    break;
                case "5":
                    try
                    {
                        _consoleUI.CreateNewOrder(_orderService, _productService,
                            _currentOrderService);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;
                case "6":
                    try
                    {
                        int orderId = _consoleUI.GetOrderId();
                        _productService.ProcessOrder(orderId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;
                case "7":
                    _consoleUI.DisplayPurchaseQueue(_productService.GetPurchaseQueue());
                    break;
                case "exit":
                    exit = true;
                    break;
            }
        }
    }
}
