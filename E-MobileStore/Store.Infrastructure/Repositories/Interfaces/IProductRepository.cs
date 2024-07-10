using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductListAsync(int categoryId);
        Task<Product> GetProductByIdAsync(Guid productId);
        void AddProductAsync(Product product);
        void UpdateProductAsync(Product product);
        void DeleteProductAsync(Guid productId);
    }
}
