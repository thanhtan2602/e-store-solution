using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Infrastructure.Data;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;
using Store.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
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
                product.UpdateDate = DateTime.Now;
                _context.Products.Update(product);
                _context.SaveChanges();
            }
        }
        public Task<ProductsVM> GetProductById(Guid productId)
        {
            var product = _context.Products
                .Include(x => x.Category)
                .Include(x => x.ProductImages)
                .Include(x => x.ProductAttributes)
                .ThenInclude(x => x.AttributeValue)
                .Include(x => x.Rates)
                .Include(x => x.Comments)
                .FirstOrDefault(x => x.Id == productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            else
            {
                var result = new ProductsVM
                {
                    Name = product.Name,
                    ShortDesc = product.ShortDesc,
                    Price = product.Price,
                    //PriceSale=x.FlashSaleProducts
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.Name,
                    isActive = product.IsActive,
                    isDeleted = product.IsDeleted,
                    Rates = product.Rates,
                    Comments = product.Comments,
                    ProductImages = product.ProductImages.Where(p => p.IsActive && !p.IsDeleted).Select(img => new ProductImagesVM
                    {
                        Id = img.Id,
                        Position = img.Position,
                        ImageName = img.ImageName,
                        ImageURL = img.ImageURL,
                    }).ToList(),
                    ProductAttributes = product.ProductAttributes.Select(atb => new ProductAttributesVM
                    {
                        Id = atb.Id,
                        AttributeContent = atb.AttributeContent,
                        AttributeName = atb.AttributeValue.AttributeName,
                        AttributeValueId = atb.AttributeId,
                    }).ToList(),
                };
                return Task.FromResult<ProductsVM>(result);
            }

        }
        public Task<IEnumerable<ProductsVM>> GetProductList(int categoryId, int page = 1, int pageSize = 2)
        {
            var product = _context.Products
                .Where(x => x.IsActive)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking();
            var productlist = product
                .Where(x => x.Category.isDeleted == false && x.Category.IsActive == true)
                .Select(x => new ProductsVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    ShortDesc = x.ShortDesc,
                    Description = x.Description,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    isActive = x.IsActive,
                    isDeleted = x.IsDeleted,
                    ProductImages = x.ProductImages
                    .Where(p => p.IsActive && !p.IsDeleted)
                    .OrderBy(p => p.Position)
                    .Select(img => new ProductImagesVM
                    {
                        Id = img.Id,
                        Position = img.Position,
                        ImageName = img.ImageName,
                        ImageURL = img.ImageURL,
                    }).ToList(),
                    Price = x.Price,
                    Quantity = x.Quantity,
                }).Where(p => p.CategoryId == categoryId && p.isDeleted == false).ToList();
            if (productlist == null)
            {
                throw new Exception("There are no products");
            }
            else
            {
                return Task.FromResult<IEnumerable<ProductsVM>>(productlist);
            }
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
                    Quantity = product.Quantity,
                    CreatedBy = product.CreatedBy,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = product.UpdatedBy,
                    UpdateDate = DateTime.Now,
                };
                _context.Products.Add(newProduct);

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
                    existingProduct.Quantity = product.Quantity;
                    existingProduct.ShortDesc = product.ShortDesc;
                    existingProduct.CategoryId = product.CategoryId;
                    existingProduct.CreatedBy = existingProduct.CreatedBy;
                    existingProduct.CreatedDate = existingProduct.CreatedDate;
                    existingProduct.UpdatedBy = product.UpdatedBy;
                    existingProduct.UpdateDate = DateTime.Now;
                    existingProduct.IsActive = product.isActive;
                    existingProduct.IsDeleted = product.isDeleted;
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
                    isActive = fsp.Product.IsActive,
                    isDeleted = fsp.Product.IsDeleted,
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

        public async Task<IEnumerable<Product>> GetProductListByCateId(int cateId)
        {
            var products = await _context.Products.Where(x => x.CategoryId == cateId).ToListAsync();
            return products != null ? products : new List<Product>();
        }
    }
}