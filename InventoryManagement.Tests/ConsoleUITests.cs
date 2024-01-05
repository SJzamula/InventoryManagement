// using InventoryManagement.BLL;
// using InventoryManagement.DAL;
// using InventoryManagement.PL;

// using Moq;

// public class ConsoleUITests
// {
//     [Fact]
//     public void DisplayProducts_ShouldShowProducts()
//     {
//         // Arrange
//         var mockProductService = new Mock<IProductService>();
//         var consoleUI = new ConsoleUI();
//         var products = new List<Product> { new Product { Name = "Test Product", Price = 10 } };
//         mockProductService.Setup(service => service.GetAllProducts()).Returns(products);

//         // Act
//         consoleUI.DisplayProducts(products);

//         // Перевірка виводу в консоль вимагає перенаправлення Console.Out або використання інтерфейсу для консолі.
//     }
// }
