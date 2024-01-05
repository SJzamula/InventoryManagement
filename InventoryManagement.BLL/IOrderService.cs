using  InventoryManagement.DAL;

namespace InventoryManagement.BLL;

public interface IOrderService
{
    void AddOrder(Order order);
    void AddProductToOrder(Product product, int quantity, int orderId);

    IEnumerable<OrderItem> GetOrderItems(int id);

    IEnumerable<Order> GetAllOrders();

    void SaveOrderItems(int orderId, List<OrderItem> orderItems);
}
