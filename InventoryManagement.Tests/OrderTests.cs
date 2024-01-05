using InventoryManagement.DAL;

public class OrderTests
{
    [Fact]
    public void AddProduct_NewProduct_ShouldAddToOrderItems()
    {
        // Arrange
        var order = new Order();
        var product = new Product {ProductId = 1, Name = "Test Product", Price = 10, Image = "./images/asus_90NR0GG4-M005T0.png", Description = "description", Quantity = 5 };

        // Act
        order.AddProduct(product, 1);

        // Assert
        Assert.Single(order.OrderItems);
        Assert.Contains(order.OrderItems, item => item.ProductId == 1 && item.Quantity == 1);
    }

    [Fact]
    public void AddProduct_ExistingProduct_ShouldIncreaseQuantity()
    {
        // Arrange
        var order = new Order();
        var product = new Product { ProductId = 1, Name = "Test Product", Price = 10, Image = "./images/asus_90NR0GG4-M005T0.png", Description = "description", Quantity = 5 };
        order.AddProduct(product, 1);

        // Act
        order.AddProduct(product, 2);

        // Assert
        Assert.Single(order.OrderItems);
        Assert.Contains(order.OrderItems, item => item.ProductId == 1 && item.Quantity == 3);
    }
}
