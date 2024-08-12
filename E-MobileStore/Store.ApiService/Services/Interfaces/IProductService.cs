using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ApiService.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductsVM>> GetProductList(int categoryId, int page, int pageSize);
        Task<IEnumerable<ProductsVM>> GetSaleProducts(int flashSaleId);
        Task<ProductsVM> GetProductById(Guid productId);
        void AddOrUpdateProduct(ProductDTO product);
        void DeleteProduct(Guid productId);
        Task<IEnumerable<Product>> GetProductListByCateId(int cateId);
    }
}
