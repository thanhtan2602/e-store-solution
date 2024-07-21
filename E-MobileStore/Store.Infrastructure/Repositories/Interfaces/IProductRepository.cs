using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductListAsync(int categoryId, int page, int pageSize);
        Task<IEnumerable<ProductsVM>> GetSaleProductsAsync(int flashSaleId);
        Task<Product> GetProductByIdAsync(Guid productId);
        void AddProductAsync(ProductDTO product);
        void UpdateProductAsync(Product product);
        void DeleteProductAsync(Guid productId, string updateBy);
        void ReStoreProductAsync(Guid productId, string updateBy);
        void PermanentlyDeleteAsync(Guid productId);
    }
}
