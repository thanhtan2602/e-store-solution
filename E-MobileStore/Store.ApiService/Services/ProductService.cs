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
        public Task<ProductsVM> GetProductById(Guid productId)
        {
            return _productRepository.GetProductById(productId);
        }
        public Task<IEnumerable<ProductsVM>> GetProductList(int categoryId, int page, int pageSize)
        {
            return _productRepository.GetProductList(categoryId, page, pageSize);
        }

        public async Task<IEnumerable<Product>> GetProductListByCateId(int cateId)
        {
            return await _productRepository.GetProductListByCateId(cateId);
        }

        public Task<IEnumerable<ProductsVM>> GetSaleProducts(int flashSaleId)
        {
            return _productRepository.GetSaleProducts(flashSaleId);
        }
    }
}
