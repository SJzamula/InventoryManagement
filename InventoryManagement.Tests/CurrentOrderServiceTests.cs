using InventoryManagement.BLL;
using InventoryManagement.DAL;

public class CurrentOrderServiceTests
{
    [Fact]
    public void AddProduct_ShouldAddProductToOrder()
    {
        // Arrange
        var service = new CurrentOrderService();
        var product = new Product { ProductId = 1, Name = "Test Product", Price = 10, Image = "./images/asus_90NR0GG4-M005T0.png", Description = "description", Quantity = 5 };

        // Act
        service.AddProduct(product, 5);

        // Assert
        var order = service.GetOrder();
        var orderItem = order.OrderItems.First();
        Assert.Equal(product.ProductId, orderItem.ProductId);
        Assert.Equal(5, orderItem.Quantity);
    }

    [Fact]
    public void ResetOrder_ShouldClearCurrentOrder()
    {
        // Arrange
        var service = new CurrentOrderService();
        service.AddProduct(new Product { ProductId = 1, Name = "Test Product", Price = 10, Image = "./images/asus_90NR0GG4-M005T0.png", Description = "description", Quantity = 5  }, 5);

        // Act
        service.ResetOrder();

        // Assert
        var order = service.GetOrder();
        Assert.Empty(order.OrderItems);
    }

    [Fact]
    public void GetOrder_ShouldReturnCurrentOrder()
    {
        // Arrange
        var service = new CurrentOrderService();
        service.AddProduct(new Product { ProductId = 1, Name = "Test Product", Price = 10, Image = "./images/asus_90NR0GG4-M005T0.png", Description = "description", Quantity = 5 }, 5);

        // Act
        var order = service.GetOrder();

        // Assert
        Assert.NotNull(order);
        Assert.Single(order.OrderItems);
    }
}
