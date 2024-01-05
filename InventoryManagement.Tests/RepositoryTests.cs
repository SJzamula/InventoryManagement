using InventoryManagement.DAL;
using Microsoft.EntityFrameworkCore;

public class RepositoryTests
{
    private DbContextOptions<InventoryContext> _options;

    public RepositoryTests()
    {
        _options = new DbContextOptionsBuilder<InventoryContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public void Add_ShouldAddEntity()
    {
        using (var context = new InventoryContext(_options))
        {
            var repository = new Repository<Product>(context);
            var product = new Product {Name = "Test Product", Price = 10, Image = "./images/asus_90NR0GG4-M005T0.png", Description = "description", Quantity = 5};

            repository.Add(product);
            context.SaveChanges();

            Assert.Equal(1, context.Products.Count());
            Assert.Equal("Test Product", context.Products.First().Name);
        }
    }

    [Fact]
    public void GetById_ShouldReturnEntity()
    {
        using (var context = new InventoryContext(_options))
        {
            var repository = new Repository<Product>(context);
            var product = new Product {Name = "Test Product", Price = 10, Image = "./images/asus_90NR0GG4-M005T0.png", Description = "description", Quantity = 5};
            context.Products.Add(product);
            context.SaveChanges();

            var retrievedProduct = repository.GetById(product.ProductId);

            Assert.Equal(product.ProductId, retrievedProduct.ProductId);
        }
    }

    // Аналогічні тести можна написати для методів GetAll і Remove
}
