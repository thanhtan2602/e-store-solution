using Store.ApiService.Services.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;
using Store.Infrastructure.ViewModel;
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
        public void AddProductAsync(ProductDTO product)
        {
            _productRepository.AddProductAsync(product);
        }

        public void DeleteProductAsync(Guid productId, string updateBy)
        {
            _productRepository.DeleteProductAsync(productId, updateBy);
        }

        public void PermanentlyDeleteAsync(Guid productId)
        {
            _productRepository.PermanentlyDeleteAsync(productId);
        }

        public Task<Product> GetProductByIdAsync(Guid productId)
        {
            return _productRepository.GetProductByIdAsync(productId);
        }

        public Task<IEnumerable<Product>> GetProductListAsync(int categoryId, int page, int pageSize)
        {
            return _productRepository.GetProductListAsync(categoryId, page, pageSize);
        }


        public void UpdateProductAsync(Product product)
        {
            _productRepository.UpdateProductAsync(product);
        }

        Task<IEnumerable<ProductsVM>> IProductService.GetSaleProductsAsync(int flashSaleId)
        {
            return _productRepository.GetSaleProductsAsync(flashSaleId);
        }

        public void ReStoreProductAsync(Guid productId, string updateBy)
        {
            _productRepository.ReStoreProductAsync(productId, updateBy);
        }
    }
}
