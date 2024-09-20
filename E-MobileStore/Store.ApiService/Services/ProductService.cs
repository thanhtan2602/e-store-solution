using Store.ApiService.Services.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;
using Store.Infrastructure.ViewModels;
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
        public void AddOrUpdateProduct(ProductDTO product)
        {
            _productRepository.AddOrUpdateProduct(product);
        }
        public void DeleteProduct(Guid productId)
        {
            _productRepository.DeleteProduct(productId);
        }
        public Task<Product> GetProductById(Guid productId)
        {
            return _productRepository.GetProductById(productId);
        }
        public async Task<IEnumerable<Product>> GetProductListByCateId(int cateId, int page, int pageSize, string? sortBy)
        {
            return await _productRepository.GetProductListByCateId(cateId, page, pageSize, sortBy);
        }

        public async Task<IEnumerable<Product>> GetProductSearchAsync(string search, int page, int pageSize)
        {
            return await _productRepository.GetProductSearchAsync(search, page, pageSize);
        }

        public Task<IEnumerable<ProductsVM>> GetSaleProducts(int flashSaleId)
        {
            return _productRepository.GetSaleProducts(flashSaleId);
        }

        public async Task<int> TotalProductAsync(int cateId)
        {
            return await _productRepository.TotalProductAsync(cateId);
        }
    }
}
