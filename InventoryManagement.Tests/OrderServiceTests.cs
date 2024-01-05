using InventoryManagement.BLL;
using InventoryManagement.DAL;
using Moq;

public class OrderServiceTests
{
    [Fact]
    public void AddOrder_ShouldAddOrder()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockOrderRepository = new Mock<IRepository<Order>>();
        mockUnitOfWork.Setup(uow => uow.Orders).Returns(mockOrderRepository.Object);
        var orderService = new OrderService(mockUnitOfWork.Object);
        var order = new Order();

        // Act
        orderService.AddOrder(order);

        // Assert
        mockOrderRepository.Verify(repo => repo.Add(order), Times.Once);
        mockUnitOfWork.Verify(uow => uow.Complete(), Times.Once);
    }

    [Fact]
    public void AddProductToOrder_ShouldAddProduct()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var order = new Order();
        var product = new Product { ProductId = 1, Name = "Test Product", Price = 10, Image = "./images/asus_90NR0GG4-M005T0.png", Description = "description", Quantity = 5 };
        mockUnitOfWork.Setup(uow => uow.Orders.GetById(1)).Returns(order);
        var orderService = new OrderService(mockUnitOfWork.Object);

        // Act
        orderService.AddProductToOrder(product, 5, 1);

        // Assert
        Assert.Contains(order.OrderItems, oi => oi.ProductId == product.ProductId && oi.Quantity == 5);
        mockUnitOfWork.Verify(uow => uow.Complete(), Times.Once);
    }
}
