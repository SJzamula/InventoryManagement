namespace InventoryManagement.DAL;

public interface IUnitOfWork : IDisposable
{
    IRepository<Product> Products { get; }
    IRepository<PurchaseQueueItem> PurchaseQueue { get; }
    IRepository<Order> Orders { get; }

    int Complete();
    void Dispose();
}