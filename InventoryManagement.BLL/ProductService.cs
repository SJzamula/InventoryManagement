// BLL/ProductService.cs
using InventoryManagement.DAL;
using System.Collections.Generic;

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
            // BLL logic to update a product
            var existingProduct = _unitOfWork.Products.GetById(product.ProductId);
            if (existingProduct != null)
            {
                // Оновлення властивостей
                _unitOfWork.Complete();
            }
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
    }
}
