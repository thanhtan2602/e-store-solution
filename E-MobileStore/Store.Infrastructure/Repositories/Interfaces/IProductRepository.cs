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

        Task<IEnumerable<FlashSaleProduct>> GetSaleProducts(int flashSaleId);
        Task<Product> GetProductByUrlAsync(string productUrl);
        Task<string> AddOrUpdateProduct(ProductDTO product);
        void DeleteProduct(string productUrl);
        Task<IEnumerable<Product>> GetProductListByCateUrlAsync(string cateUrl, int page, int pageSize, string? sortBy);
        Task<IEnumerable<Product>> GetProductListAsync(int page, int pageSize, string? sortBy);
        Task<int> TotalProductByCateAsync(string cateUrl);
        Task<int> TotalProductAsync();

		Task<IEnumerable<Product>> GetProductSearchAsync(string search, int page, int pageSize);
    }
}
