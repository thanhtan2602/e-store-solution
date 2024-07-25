using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Infrastructure.Data;
using Store.Infrastructure.DTOs;
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
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddProductAsync(ProductDTO product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                ShortDesc = product.ShortDesc,
                CategoryId = product.CategoryId,
                isDeleted = false,
                Price = product.Price,
                Quantity = product.Quantity,
                CreatedBy = product.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = product.UpdatedBy,
                UpdateDate = DateTime.UtcNow,
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            if (product.ProductAttributes != null && product.ProductAttributes.Any())
            {
                var productAttributes = product.ProductAttributes.Select(paDto => new ProductAttribute
                {
                    Id = Guid.NewGuid(),
                    AttributeContent = paDto.AttributeContent,
                    ProductId = newProduct.Id,
                    AttributeId = paDto.AttributeValueId,
                }).ToList();

                newProduct.ProductAttributes = productAttributes;
                _context.ProductAttributes.AddRange(productAttributes);
                _context.SaveChanges();
            }


        }


        public async void DeleteProductAsync(Guid productId, string updateBy)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                product.isDeleted = true;
                product.UpdatedBy = updateBy;
                product.UpdateDate = DateTime.UtcNow;
                _context.Products.Update(product);
                _context.SaveChanges();
            }
        }
        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            var product = await _context.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == productId && x.isDeleted == false);

            return product ?? new Product();
        }
        public async Task<IEnumerable<Product>> GetProductListAsync(int categoryId, int page = 1, int pageSize = 2)
        {

            var product = _context.Products.Include(p => p.Category).AsQueryable();
            var productlist = product.Where(x => x.CategoryId == categoryId && x.isDeleted == false);
            var result = productlist.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return result;
        }
        public void UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
        }
        public async Task<IEnumerable<ProductsVM>> GetSaleProductsAsync(int flashSaleId)
        {
            var flashSaleProductsInclude = _context.FlashSaleProducts
                .Include(p => p.Product)
                .ThenInclude(p => p.ProductImages)
                .Include(p => p.FlashSale).AsNoTracking();
            var productlist = flashSaleProductsInclude.Where(p => p.FlashSaleId == flashSaleId);
            var result = productlist.Select(fsp => new ProductsVM
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
            }).ToList().Where(x => x.isDeleted == false);

            return result;
        }

        public void PermanentlyDeleteAsync(Guid productId)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public void ReStoreProductAsync(Guid productId, string updateBy)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                product.isDeleted = false;
                product.UpdatedBy = updateBy;
                product.UpdateDate = DateTime.UtcNow;
                _context.Products.Update(product);
                _context.SaveChanges();
            }
        }
    }
}
