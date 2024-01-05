namespace InventoryManagement.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryContext _context;
        private IRepository<Product> _products;
        private IRepository<Order> _orders;
        private IRepository<PurchaseQueueItem> _purchaseQueue;

        public UnitOfWork(InventoryContext context)
        {
            _context = context;
        }

        public IRepository<Product> Products
        {
            get
            {
                return _products ?? (_products = new Repository<Product>(_context));
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                return _orders ?? (_orders = new Repository<Order>(_context));
            }
        }

        public IRepository<PurchaseQueueItem> PurchaseQueue
        {
            get
            {
                return _purchaseQueue ?? (_purchaseQueue = new Repository<PurchaseQueueItem>(_context));
            }
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}