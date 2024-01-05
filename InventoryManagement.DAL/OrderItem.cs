using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.DAL;

public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; }
    public int OrderId { get; set; }

    public override string ToString()
    {
        return $"Order item ID: {OrderItemId}, Product ID: {ProductId}, Quantity: {Quantity}";
    }
}