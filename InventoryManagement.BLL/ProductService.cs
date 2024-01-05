using InventoryManagement.DAL;

namespace InventoryManagement.BLL
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _unitOfWork.Products.GetAll();
        }

        public Product GetProductById(int id)
        {
            return _unitOfWork.Products.GetById(id);
        }

        public void CreateProduct(Product product)
        {
            _unitOfWork.Products.Add(product);
            _unitOfWork.Complete();
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _unitOfWork.Products.GetById(product.ProductId);
            if (existingProduct != null)
            {
                _unitOfWork.Complete();
            }
            throw new ArgumentException("Parameter id is not correct");
        }

        public void DeleteProduct(int id)
        {
            var product = _unitOfWork.Products.GetById(id);
            if (product != null)
            {
                _unitOfWork.Products.Remove(product);
                _unitOfWork.Complete();
            }
        }

        public void SetProductQuantity(int id, int quantity)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                product.Quantity = quantity;
                _unitOfWork.Complete();
            }
            else
            {
                throw new ArgumentException("Parameter id is not correct");
            }
        }

        public void AddToPurchaseQueue(int productId, int quantity, int orderId)
        {
            var product = GetProductById(productId);
            if (product == null)
            {
                throw new ArgumentException("Parameter productId is not correct");
            }
            if (!product.IsInStorage)
            {
                var purchaseQueueItem = new PurchaseQueueItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    OrderId = orderId
                    // Встановіть інші необхідні властивості
                };

                _unitOfWork.PurchaseQueue.Add(purchaseQueueItem);
                _unitOfWork.Complete();
            }
            else
            {
                throw new InvalidOperationException("Product is in storage");
            }
        }

        public IEnumerable<PurchaseQueueItem> GetPurchaseQueue()
        {
            return _unitOfWork.PurchaseQueue.GetAll();
        }

        public void ProcessOrder(int orderId)
        {
            var order = _unitOfWork.Orders.GetById(orderId);
            if (order != null)
            {
                if (order.Status == OrderStatus.Processed)
                {
                    throw new ArgumentException($"Order {orderId} is processed");
                }
                foreach (var orderItem in order.OrderItems)
                {
                    var product = GetProductById(orderItem.ProductId);
                    if (product == null)
                    {
                        throw new InvalidOperationException("Product id is not correct");
                    }
                    if (product.IsInStorage)
                    {
                        // if user ordered more product quantity than avaible, save diffrence in queue
                        if (orderItem.Quantity > product.Quantity)
                        {
                            int missedProductQuantity = orderItem.Quantity - product.Quantity;
                            product.Quantity = 0;
                            _unitOfWork.Complete();
                            AddToPurchaseQueue(orderItem.ProductId, missedProductQuantity, orderId);
                        }
                        else
                        {
                            product.Quantity -= orderItem.Quantity;
                        }
                    }
                    else
                    {
                        // Якщо товар відсутній, додайте його до черги придбання
                        AddToPurchaseQueue(orderItem.ProductId, orderItem.Quantity, orderId);
                    }
                }

                // Оновіть стан замовлення
                order.Status = OrderStatus.Processed;
                _unitOfWork.Complete();
            }
            else
            {
                throw new ArgumentException("Parameter orderId is not correct");
            }
        }

        public void TryToProcessPurchaseQueue(int productId)
        {
            var purchaseItem = _unitOfWork.PurchaseQueue.GetAll().FirstOrDefault(x => x.ProductId == productId);
            while (purchaseItem != null)
            {
                var product = GetProductById(productId);
                if (purchaseItem.Quantity <= product.Quantity)
                {
                    product.Quantity -= purchaseItem.Quantity;
                    _unitOfWork.PurchaseQueue.Remove(purchaseItem);
                    _unitOfWork.Complete();
                }
                purchaseItem = _unitOfWork.PurchaseQueue.GetAll().FirstOrDefault(x => x.ProductId == productId);
            }
        }
    }
}
