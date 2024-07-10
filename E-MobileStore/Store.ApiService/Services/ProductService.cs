using Store.ApiService.Services.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ApiService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public void AddProductAsync(Product product)
        {
            _productRepository.AddProductAsync(product);
        }

        public void DeleteProductAsync(Guid productId)
        {
            _productRepository.DeleteProductAsync(productId);
        }

        public Task<Product> GetProductByIdAsync(Guid productId)
        {
            return _productRepository.GetProductByIdAsync(productId);
        }

        public Task<IEnumerable<Product>> GetProductListAsync(int categoryId)
        {
            return _productRepository.GetProductListAsync(categoryId);
        }

        public void UpdateProductAsync(Product product)
        {
            _productRepository.UpdateProductAsync(product);
        }
    }
}
