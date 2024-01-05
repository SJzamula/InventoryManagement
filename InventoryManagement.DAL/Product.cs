using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.DAL;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public required string Image { get; set; }
    public required string Description { get; set; }
    public int Quantity { get; set; }
    public bool IsInStorage { get { return Quantity != 0; } }

    public override string ToString()
    {
        return $"ID: {ProductId}, Name: {Name}, Price: {Price}, Quantity: {Quantity}, Description: {Description}";
    }
}