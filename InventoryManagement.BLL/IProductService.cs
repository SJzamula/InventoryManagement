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
        void SetProductQuantity(int id, int quantity);
        void AddToPurchaseQueue(int productId, int quantity, int orderId);
        IEnumerable<PurchaseQueueItem> GetPurchaseQueue();
        void ProcessOrder(int orderId);
        void TryToProcessPurchaseQueue(int productId);
        
    }
}
