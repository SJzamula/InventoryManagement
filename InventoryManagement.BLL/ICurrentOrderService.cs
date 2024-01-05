using InventoryManagement.DAL;

namespace InventoryManagement.BLL
{
    public interface ICurrentOrderService
    {
        void AddProduct(Product product, int quantity);
        void ResetOrder();
        Order GetOrder();
    }
}
