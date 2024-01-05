using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.DAL
{
    public class PurchaseQueueItem
    {
        [Key]
        public int QueueItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }

        public override string ToString()
        {
            return $"ID: {QueueItemId}, product ID: {ProductId}, quantity: {Quantity}, order ID: {OrderId}";
        }
    }
}