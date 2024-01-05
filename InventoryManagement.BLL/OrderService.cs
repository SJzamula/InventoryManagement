using InventoryManagement.DAL;

namespace InventoryManagement.BLL;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddOrder(Order order)
    {
        // Add the new order to the repository
        _unitOfWork.Orders.Add(order);

        // Save changes to the in-memory database
        _unitOfWork.Complete();
    }

    public void AddProductToOrder(Product product, int quantity, int orderId)
    {
        // Retrieve the order from the database
        var order = _unitOfWork.Orders.GetById(orderId);
        if (order != null)
        {
            order.AddProduct(product, quantity);
        }
        else
        {
            throw new ArgumentException("Parameter orderId is not correct");
        }

        // Зберігаємо зміни в базі даних
        _unitOfWork.Complete();
    }

    public IEnumerable<OrderItem> GetOrderItems(int id)
    {
        // Retrieve the order from the database
        var order = _unitOfWork.Orders.GetById(id);
        if (order != null)
        {
            return order.OrderItems;
        }
        throw new ArgumentException("Parameter id is not correct");
    }

    public IEnumerable<Order> GetAllOrders()
    {
        return _unitOfWork.Orders.GetAll();
    }

    public void SaveOrderItems(int orderId, List<OrderItem> orderItems)
    {
        // Retrieve the order from the database
        var order = _unitOfWork.Orders.GetById(orderId);

        if (order == null)
        {
            order = new Order();
            _unitOfWork.Orders.Add(order);
            // This assumes that OrderItems is a property on the Order entity
            order.OrderItems = orderItems;
        }
        else
        {
            // If the order exists, update its items
            order.OrderItems.Clear();
            foreach (var item in orderItems)
            {
                order.OrderItems.Add(item);
            }
        }

        // Save changes in the unit of work
        _unitOfWork.Complete();
    }
}
