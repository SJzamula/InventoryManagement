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
        _unitOfWork.Orders.Add(order);

        _unitOfWork.Complete();
    }

    public void AddProductToOrder(Product product, int quantity, int orderId)
    {
        var order = _unitOfWork.Orders.GetById(orderId);
        if (order != null)
        {
            order.AddProduct(product, quantity);
        }
        else
        {
            throw new ArgumentException("Parameter orderId is not correct");
        }

        _unitOfWork.Complete();
    }

    public IEnumerable<OrderItem> GetOrderItems(int id)
    {
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
        var order = _unitOfWork.Orders.GetById(orderId);

        if (order == null)
        {
            order = new Order();
            _unitOfWork.Orders.Add(order);
            order.OrderItems = orderItems;
        }
        else
        {
            order.OrderItems.Clear();
            foreach (var item in orderItems)
            {
                order.OrderItems.Add(item);
            }
        }

        _unitOfWork.Complete();
    }
}
