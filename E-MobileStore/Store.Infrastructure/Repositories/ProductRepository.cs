using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Infrastructure.Data;
using Store.Infrastructure.Repositories.Interfaces;
using Store.Infrastructure.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public static int PageSize { get; set; } = 2;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddProductAsync(Product product)
        {
            _context.Products.Add(product);
        }

        public void DeleteProductAsync(Guid productId)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
        }



        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            var product = _context.Products
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == productId);

            return product ?? new Product();
        }



        public async Task<IEnumerable<Product>> GetProductListAsync(int categoryId, int page = 1)
        {
            var product = _context.Products.Include(p => p.Category).AsQueryable();
            var productlist = product.Where(x => x.CategoryId == categoryId);
            var result = productlist.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            return result;
        }

        public void UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task<IEnumerable<ProductSaleVM>> GetProductSaleAsync(int flashSaleId)
        {
            var flashSaleProductsInclude = _context.FlashSaleProducts
                .Include(p => p.Product)
                .ThenInclude(p => p.ProductImages)
                .Include(p => p.FlashSale).AsQueryable();
            var productlist = flashSaleProductsInclude.Where(p => p.FlashSaleId == flashSaleId);
            var result = productlist.Select(fsp => new ProductSaleVM

            {
                Id = fsp.Product.Id,
                Name = fsp.Product.Name,
                Price = fsp.Product.Price,
                Description = fsp.Product.Description,
                CategoryId = fsp.Product.CategoryId,
                ShortDesc = fsp.Product.ShortDesc,
                IsActive = fsp.Product.isActive,
                isDeleted = fsp.Product.isDeleted,
                ProductImages = fsp.Product.ProductImages,

                PriceSale = fsp.PriceSale,
            }).ToList();
            return result;

        }
    }
}
