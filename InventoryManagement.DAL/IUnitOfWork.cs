namespace InventoryManagement.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }

        int Complete();
    }
}