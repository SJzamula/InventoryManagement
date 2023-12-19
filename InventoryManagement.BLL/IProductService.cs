// BLL/IProductService.cs
using InventoryManagement.DAL;

namespace InventoryManagement.BLL
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
