using InventoryManagement.DAL;

namespace InventoryManagement.BLL;
public class CurrentOrderService : ICurrentOrderService
{
    private Order _currentOrder = new Order();

    public void AddProduct(Product product, int quantity)
    {
        _currentOrder.AddProduct(product, quantity);
    }

    public void ResetOrder()
    {
        _currentOrder = new Order();
    }

    public Order GetOrder()
    {
        return _currentOrder;
    }
}
