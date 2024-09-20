using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Infrastructure.Data;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;
using Store.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Store.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteProduct(Guid productId)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);

            if (product == null)
            {
                throw new Exception("Product not found");
            }
            else
            {
                product.IsDeleted = true;
                product.UpdatedDate = DateTime.Now;
                _context.Products.Update(product);
                _context.SaveChanges();
            }
        }
        public async Task<Product> GetProductById(Guid productId)
        {
            var product = await _context.Products
                .Include(x => x.Category)
                .Include(x => x.ProductImages)
                .Include(x => x.ProductAttributes)
                .ThenInclude(x => x.AttributeValue)
                .Include(x => x.Rates)
                .Include(x => x.Comments)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == productId);
            return product ?? new Product();
        }
        private void AddProductImage(ProductImageDTO imageDTO, Product product)
        {
            var newImage = new ProductImage
            {
                Id = Guid.NewGuid(),
                ImageName = imageDTO.ImageName,
                ImageURL = imageDTO.ImageURL,
                CreatedBy = product.CreatedBy,
                UpdatedBy = product.UpdatedBy,
                IsDeleted = product.IsDeleted,
                IsActive = product.IsActive,
                Position = imageDTO.Position,
                Product = product,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                ProductId = product.Id,
            };
            _context.AddAsync(newImage);
        }
        public void AddOrUpdateProduct(ProductDTO product)
        {
            if (product.Id == "0")
            {
                bool nameExists = _context.Products
                           .Any(p => p.Name == product.Name);

                if (nameExists)
                {
                    throw new Exception("This Product Name is already exists.");
                }
                var newProduct = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = product.Name,
                    Description = product.Description,
                    ShortDesc = product.ShortDesc,
                    CategoryId = product.CategoryId,
                    IsDeleted = false,
                    Price = product.Price,
                    PriceSale = product.PriceSale,
                    Quantity = product.Quantity,
                    CreatedBy = product.CreatedBy,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = product.UpdatedBy,
                    UpdatedDate = DateTime.Now,
                };
                _context.Products.Add(newProduct);
                foreach (var item in product.ProductImages)
                {
                    AddProductImage(item, newProduct);
                }
            }
            else
            {
                var existingProduct = _context.Products
                               .Include(p => p.ProductImages)
                               .FirstOrDefault(p => p.Id.ToString() == product.Id);
                if (existingProduct == null)
                {
                    throw new Exception("Product not found");
                }
                else
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.PriceSale = product.PriceSale;
                    existingProduct.Quantity = product.Quantity;
                    existingProduct.ShortDesc = product.ShortDesc;
                    existingProduct.CategoryId = product.CategoryId;
                    existingProduct.CreatedBy = existingProduct.CreatedBy;
                    existingProduct.CreatedDate = existingProduct.CreatedDate;
                    existingProduct.UpdatedBy = product.UpdatedBy;
                    existingProduct.UpdatedDate = DateTime.Now;
                    existingProduct.IsActive = product.IsActive;
                    existingProduct.IsDeleted = product.IsDeleted;
                    _context.Products.Update(existingProduct);
                }
            }
            _context.SaveChanges();
        }
        public Task<IEnumerable<ProductsVM>> GetSaleProducts(int flashSaleId)
        {
            bool isFlashSaleExist = _context.FlashSales.Any(p => p.Id == flashSaleId &&
                    !p.IsDeleted &&
                    p.IsActive &&
                    p.DateOpen <= DateTime.Now &&
                    p.DateClose >= DateTime.Now);
            if (!isFlashSaleExist)
            {
                throw new Exception("The FlashSale was not found");
            }
            var flashSaleProductsInclude = _context.FlashSaleProducts
                .Include(p => p.Product)
                .ThenInclude(p => p.Category)
                .Include(p => p.Product)
                .ThenInclude(p => p.ProductImages)
                .Include(p => p.FlashSale)
                .AsNoTracking();

            var productlist = flashSaleProductsInclude
                .Where(p => p.FlashSaleId == flashSaleId &&
                            !p.Product.IsDeleted &&
                             p.Product.IsActive).ToList();
            if (productlist.Count == 0)
            {
                throw new Exception("There are no products");

            }
            else
            {
                var result = productlist.Select(fsp => new ProductsVM
                {
                    Id = fsp.Product.Id,
                    Name = fsp.Product.Name,
                    Price = fsp.Product.Price,
                    PriceSale = fsp.PriceSale,
                    Description = fsp.Product.Description,
                    CategoryId = fsp.Product.CategoryId,
                    CategoryName = fsp.Product.Category.Name,
                    ShortDesc = fsp.Product.ShortDesc,
                    IsActive = fsp.Product.IsActive,
                    IsDeleted = fsp.Product.IsDeleted,
                    ProductImages = fsp.Product.ProductImages.Where(p => p.IsActive && !p.IsDeleted).Select(img => new ProductImagesVM
                    {
                        Id = img.Id,
                        Position = img.Position,
                        ImageName = img.ImageName,
                        ImageURL = img.ImageURL,
                    }).ToList(),
                }).ToList();
                return Task.FromResult<IEnumerable<ProductsVM>>(result);
            }
        }
        public async Task<IEnumerable<Product>> GetProductListByCateId(int cateId, int page, int pageSize, string? sortBy)
        {
            IQueryable<Product> query = _context.Products
                .AsNoTracking()
                .Where(x => x.CategoryId == cateId && x.IsActive && !x.IsDeleted);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "date_desc":
                        query = query.OrderByDescending(x => x.CreatedDate);
                        break;
                    case "date_asc":
                        query = query.OrderBy(x => x.CreatedDate);
                        break;
                    case "name_desc":
                        query = query.OrderByDescending(x => x.Name);
                        break;
                    case "name_asc":
                        query = query.OrderBy(x => x.Name);
                        break;
                    case "price_desc":
                        query = query.OrderByDescending(x => x.Price);
                        break;
                    case "price_asc":
                        query = query.OrderBy(x => x.Price);
                        break;
                }
            }
            var products = await query
                .Include(x => x.Category)
                .Include(x => x.ProductImages)
                .Include(x => x.ProductAttributes)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return products ?? new List<Product>();
        }
        public async Task<IEnumerable<Product>> GetProductSearchAsync(string search, int page, int pageSize)
        {
            var item = await _context.Products
                .Include(x => x.Category)
                .Include(x => x.ProductImages)
                .Where(x => x.IsActive && !x.IsDeleted && (x.Name.ToLower().Contains(search.ToLower()) || x.ShortDesc.ToLower().Contains(search.ToLower()) || x.Category.Name.ToLower().Contains(search.ToLower())))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(x => x.Name)
                .AsNoTracking()
                .ToListAsync();
            return item ?? new List<Product>();
        }

        public async Task<int> TotalProductAsync(int cateId)
        {
            int totalProduct = await _context.Products.Where(x => x.CategoryId == cateId && !x.IsDeleted && x.IsActive).CountAsync();
            return totalProduct;
        }
    }
}