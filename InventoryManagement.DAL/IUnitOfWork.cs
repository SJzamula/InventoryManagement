namespace InventoryManagement.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        // ... Other repositories as properties

        int Complete();
    }
}