using InventoryManagement.DAL;

public class OrderItemTests
{
    [Fact]
    public void SetProperties_ValidData_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var orderItem = new OrderItem();
        var productId = 1;
        var quantity = 5;
        var orderId = 10;

        // Act
        orderItem.ProductId = productId;
        orderItem.Quantity = quantity;
        orderItem.OrderId = orderId;

        // Assert
        Assert.Equal(productId, orderItem.ProductId);
        Assert.Equal(quantity, orderItem.Quantity);
        Assert.Equal(orderId, orderItem.OrderId);
    }

    [Fact]
    public void ToString_ValidData_ShouldReturnCorrectFormat()
    {
        // Arrange
        var orderItem = new OrderItem
        {
            OrderItemId = 1,
            ProductId = 2,
            Quantity = 3
        };

        // Act
        var result = orderItem.ToString();

        // Assert
        var expectedString = "Order item ID: 1, Product ID: 2, Quantity: 3";
        Assert.Equal(expectedString, result);
    }
}
