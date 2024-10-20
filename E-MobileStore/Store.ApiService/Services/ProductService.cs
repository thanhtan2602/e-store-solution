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
        public Task<string> AddOrUpdateProduct(ProductDTO product)
        {
            return _productRepository.AddOrUpdateProduct(product);
        }
        public void DeleteProduct(string productUrl)
        {
            _productRepository.DeleteProduct(productUrl);
        }
        public Task<Product> GetProductByUrlAsync(string productUrl)
        {
            return _productRepository.GetProductByUrlAsync(productUrl);
        }

        public async Task<IEnumerable<Product>> GetProductListAsync(int page, int pageSize, string? sortBy)
        {
            return await _productRepository.GetProductListAsync(page, pageSize, sortBy);
        }

        public async Task<IEnumerable<Product>> GetProductListByCateUrlAsync(string cateUrl, int page, int pageSize, string? sortBy)
        {
            return await _productRepository.GetProductListByCateUrlAsync(cateUrl, page, pageSize, sortBy);
        }

        public async Task<IEnumerable<Product>> GetProductSearchAsync(string search, int page, int pageSize)
        {
            return await _productRepository.GetProductSearchAsync(search, page, pageSize);
        }

        public Task<IEnumerable<FlashSaleProduct>> GetSaleProducts(int flashSaleId)
        {
            return _productRepository.GetSaleProducts(flashSaleId);
        }

        public async Task<int> TotalProductByCateAsync(string cateUrl)
        {
            return await _productRepository.TotalProductByCateAsync(cateUrl);
        }
        public async Task<int> TotalProductAsync()
        {
            return await _productRepository.TotalProductAsync();
        }
    }
}
