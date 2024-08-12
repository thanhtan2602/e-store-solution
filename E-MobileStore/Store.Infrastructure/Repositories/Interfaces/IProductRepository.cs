using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductsVM>> GetProductList(int categoryId, int page, int pageSize);
        Task<IEnumerable<ProductsVM>> GetSaleProducts(int flashSaleId);
        Task<ProductsVM> GetProductById(Guid productId);
        void AddOrUpdateProduct(ProductDTO product);
        void DeleteProduct(Guid productId);
        Task<IEnumerable<Product>> GetProductListByCateId(int cateId);
    }
}
