using InventoryManagement.DAL;

public class ProductTests
{
    [Fact]
    public void IsInStorage_QuantityNotZero_ShouldReturnTrue()
    {
        // Arrange
        var product = new Product {Name = "Test Product", Price = 10, Image = "./images/asus_90NR0GG4-M005T0.png", Description = "description", Quantity = 5 };

        // Act
        var result = product.IsInStorage;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ToString_ValidData_ShouldReturnCorrectFormat()
    {
        // Arrange
        var product = new Product
        {
            ProductId = 1,
            Name = "Test",
            Image = "./images/asus_90NR0GG4-M005T0.png",
            Price = 100m,
            Quantity = 20,
            Description = "Test Product"
        };

        // Act
        var result = product.ToString();

        // Assert
        var expectedString = "ID: 1, Name: Test, Price: 100, Quantity: 20, Description: Test Product";
        Assert.Equal(expectedString, result);
    }
}
