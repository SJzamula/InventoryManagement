using System.ComponentModel.DataAnnotations;
namespace InventoryManagement.DAL;

public class Order
{
    [Key]
    public int OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public void AddProduct(Product product, int quantity)
    {
        var orderItem = OrderItems.FirstOrDefault(c => c.ProductId == product.ProductId);
        if (orderItem != null)
        {
            orderItem.Quantity += quantity;
        }
        else
        {
            orderItem = new OrderItem
            {
                ProductId = product.ProductId,
                Quantity = quantity
            };
            OrderItems.Add(orderItem);
        }
    }

    public override string ToString()
    {
        return $"ID: {OrderId}, Status: {Status}";
    }
}