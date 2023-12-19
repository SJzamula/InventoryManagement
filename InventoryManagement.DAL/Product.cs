using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.DAL
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        // Інші властивості...
    }
}