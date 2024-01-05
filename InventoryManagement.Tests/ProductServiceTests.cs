using InventoryManagement.BLL;
using InventoryManagement.DAL;
using Moq;

//Name = "Test Product", Price = 10, Image = "./images/asus_90NR0GG4-M005T0.png", Description = "description", Quantity = 5 

public class ProductServiceTests
{
    [Fact]
    public void CreateProduct_AddsNewProduct()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockProductRepository = new Mock<IRepository<Product>>();
        mockUnitOfWork.Setup(uow => uow.Products).Returns(mockProductRepository.Object); // Важлива зміна тут
        var productService = new ProductService(mockUnitOfWork.Object);
        var product = new Product { Name = "Test Product", Price = 10, Image = "./images/asus_90NR0GG4-M005T0.png", Description = "description", Quantity = 5 };

        // Act
        productService.CreateProduct(product);

        // Assert
        mockProductRepository.Verify(repo => repo.Add(It.IsAny<Product>()), Times.Once); // Змінено на mockProductRepository
        mockUnitOfWork.Verify(uow => uow.Complete(), Times.Once);
    }

    [Fact]
    public void GetProductById_ReturnsProduct()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(uow => uow.Products.GetById(1)).Returns(new Product { Name = "Test Product", Price = 10, Image = "./images/asus_90NR0GG4-M005T0.png", Description = "description", Quantity = 5 });
        var productService = new ProductService(mockUnitOfWork.Object);

        var result = productService.GetProductById(1);

        Assert.NotNull(result);
    }

    [Fact]
    public void DeleteProduct_RemovesProduct()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var product = new Product { ProductId = 1, Name = "Test Product", Price = 10, Image = "./images/asus_90NR0GG4-M005T0.png", Description = "description", Quantity = 5 };
        mockUnitOfWork.Setup(uow => uow.Products.GetById(1)).Returns(product);
        var productService = new ProductService(mockUnitOfWork.Object);

        productService.DeleteProduct(1);

        mockUnitOfWork.Verify(uow => uow.Products.Remove(product), Times.Once);
        mockUnitOfWork.Verify(uow => uow.Complete(), Times.Once);
    }
}
