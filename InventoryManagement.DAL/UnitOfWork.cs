namespace InventoryManagement.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryContext _context;
        private IRepository<Product> _products;

        public UnitOfWork(InventoryContext context)
        {
            _context = context;
        }

        public IRepository<Product> Products
        {
            get
            {
                if (_products == null)
                    _products = new Repository<Product>(_context);

                return _products;
            }
        }

        public int Complete()
        {
            // Save changes across all repositories
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        // ... Implement additional repository properties
    }
}