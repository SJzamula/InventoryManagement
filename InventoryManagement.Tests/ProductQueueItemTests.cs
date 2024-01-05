using InventoryManagement.DAL;

public class PurchaseQueueItemTests
{
    [Fact]
    public void SetProperties_ValidData_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var queueItem = new PurchaseQueueItem();
        var queueItemId = 1;
        var productId = 2;
        var quantity = 5;
        var orderId = 3;

        // Act
        queueItem.QueueItemId = queueItemId;
        queueItem.ProductId = productId;
        queueItem.Quantity = quantity;
        queueItem.OrderId = orderId;

        // Assert
        Assert.Equal(queueItemId, queueItem.QueueItemId);
        Assert.Equal(productId, queueItem.ProductId);
        Assert.Equal(quantity, queueItem.Quantity);
        Assert.Equal(orderId, queueItem.OrderId);
    }

    [Fact]
    public void ToString_ValidData_ShouldReturnCorrectFormat()
    {
        // Arrange
        var queueItem = new PurchaseQueueItem
        {
            QueueItemId = 1,
            ProductId = 2,
            Quantity = 5,
            OrderId = 3
        };

        // Act
        var result = queueItem.ToString();

        // Assert
        var expectedString = "ID: 1, product ID: 2, quantity: 5, order ID: 3";
        Assert.Equal(expectedString, result);
    }
}
