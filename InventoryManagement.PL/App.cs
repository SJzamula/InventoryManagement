using InventoryManagement.BLL; // Namespace for BLL
using InventoryManagement.DAL;
using System;

public class App
{
    private readonly IProductService _productService;
    private readonly ConsoleUI _consoleUI;
    private readonly IUnitOfWork _unitOfWork;

    Repository<Product> listOfProducts;

    public App(IProductService productService, ConsoleUI consoleUI, IUnitOfWork unitOfWork)
    {
        _productService = productService;
        _consoleUI = consoleUI;
        _unitOfWork = unitOfWork;
    }

    public void Run()
    {
        bool exit = false;
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
                case "exit":
                    exit = true;
                    break;
            }
        }
    }
}
