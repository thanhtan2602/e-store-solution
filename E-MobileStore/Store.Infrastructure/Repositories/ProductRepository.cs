using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Infrastructure.Data;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;
using Store.Infrastructure.ViewModels;
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
                Id = Guid.NewGuid(),
                Name = product.Name,
                Description = product.Description.ToString(),
                ShortDesc = product.ShortDesc.ToString(),
                CategoryId = product.CategoryId,
                isDeleted = false,
                Price = product.Price,
                Quantity = product.Quantity,
                CreatedBy = product.CreatedBy,
                CreatedDate = DateTime.Now,
                UpdatedBy = product.UpdatedBy,
                UpdateDate = DateTime.Now,
            };
            _context.Products.Add(newProduct);
            //_context.SaveChanges();
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
            }
            if (product.ProductImages != null && product.ProductImages.Any())
            {
                var productImage = product.ProductImages.Select(piDto => new ProductImage
                {
                    Id = Guid.NewGuid(),
                    ImageName = piDto.ImageName,
                    ImageURL = piDto.ImageURL,
                    Position = piDto.Position,
                    ProductId = newProduct.Id,
                    isActive = true,
                    isDeleted = false,
                    CreatedBy = newProduct.CreatedBy,
                    UpdatedBy = newProduct.UpdatedBy,
                    CreatedDate = newProduct.CreatedDate,
                    UpdateDate = newProduct.UpdateDate
                }).ToList();
                newProduct.ProductImages = productImage;
                _context.ProductImages.AddRange(productImage);
            }
            _context.SaveChanges();

        }
        public async void DeleteProductAsync(Guid productId, string updateBy)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                product.isDeleted = true;
                product.UpdatedBy = updateBy;
                product.UpdateDate = DateTime.Now;
                _context.Products.Update(product);
                _context.SaveChanges();
            }
        }
        public async Task<ProductsVM> GetProductByIdAsync(Guid productId)
        {
            var product = _context.Products
                .Include(x => x.Category)
                .Include(x => x.ProductImages)
                .Include(x => x.ProductAttributes)
                .ThenInclude(x => x.AttributeValue)
                .Include(x => x.Rates)
                .Include(x => x.Comments)
                .FirstOrDefault(x => x.Id == productId);
            var result = new ProductsVM
            {
                Name = product.Name,
                ShortDesc = product.ShortDesc,
                Price = product.Price,
                //PriceSale=x.FlashSaleProducts
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                isActive = product.isActive,
                isDeleted = product.isDeleted,
                Rates = product.Rates,
                Comments = product.Comments,
                ProductImages = product.ProductImages.Where(p => p.isActive && !p.isDeleted).Select(img => new ProductImagesVM
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
                    AttributeName = atb.AttributeValue?.AttributeName,
                    AttributeValueId = atb.AttributeId,
                }).ToList(),
            };
            return result ?? null;
        }
        public async Task<IEnumerable<ProductsVM>> GetProductListAsync(int categoryId, int page = 1, int pageSize = 2)
        {
            var product = _context.Products.Include(p => p.Category).Include(p => p.ProductImages).AsNoTracking();
            var productlist = product.Select(x => new ProductsVM
            {
                Id = x.Id,
                Name = x.Name,
                ShortDesc = x.ShortDesc,
                Description = x.Description,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                isActive = x.isActive,
                isDeleted = x.isDeleted,
                ProductImages = x.ProductImages.Where(p => p.isActive && !p.isDeleted).OrderBy(p => p.Position).Select(img => new ProductImagesVM
                {
                    Id = img.Id,
                    Position = img.Position,
                    ImageName = img.ImageName,
                    ImageURL = img.ImageURL,
                }).ToList(),
                Price = x.Price,
                Quantity = x.Quantity,
            }).Where(p => p.CategoryId == categoryId && p.isDeleted == false && p.isActive == true).ToList();
            var result = productlist.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return result;
        }
        public void UpdateProductAsync(ProductDTO product, Guid productId)
        {
            var existingProduct = _context.Products
                                .Include(p => p.ProductImages)
                                .Include(p => p.ProductAttributes)
                                .ThenInclude(x => x.AttributeValue)
                                .FirstOrDefault(p => p.Id == productId);
            if (existingProduct != null)
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
                existingProduct.isActive = product.isActive;
                existingProduct.isDeleted = product.isDeleted;
                _context.Products.Update(existingProduct);
                if (existingProduct.ProductAttributes != null)
                {
                    foreach (var paDTOs in product.ProductAttributes)
                    {
                            var newAttribute = new ProductAttribute
                            {
                                Id = Guid.NewGuid(),
                                AttributeContent = paDTOs.AttributeContent,
                                AttributeId = paDTOs.AttributeValueId,
                                ProductId = existingProduct.Id,
                            };
                            _context.ProductAttributes.Add(newAttribute);
                    }
                    //remove old attribute
                    var oldAttribute = _context.ProductAttributes.Select(pa => pa.Id).ToList();
                    var attributeToRemove = existingProduct.ProductAttributes
                        .Where(pa => oldAttribute.Contains(pa.Id))
                        .ToList();
                    _context.ProductAttributes.RemoveRange(attributeToRemove);
                }
                else
                {
                    if (product.ProductAttributes != null && product.ProductAttributes.Any())
                    {
                        var productAttributes = product.ProductAttributes.Select(paDto => new ProductAttribute
                        {
                            Id = Guid.NewGuid(),
                            AttributeContent = paDto.AttributeContent,
                            ProductId = existingProduct.Id,
                            AttributeId = paDto.AttributeValueId,
                        }).ToList();
                        existingProduct.ProductAttributes = productAttributes;
                        _context.ProductAttributes.AddRange(productAttributes);
                    }
                }

                if (existingProduct.ProductImages != null)
                {
                    foreach (var piDTOs in product.ProductImages)
                    {
                            var newImage = new ProductImage
                            {
                                Id = Guid.NewGuid(),
                                ImageURL = piDTOs.ImageURL,
                                ImageName = piDTOs.ImageName,
                                Position = piDTOs.Position,
                                isActive = existingProduct.isActive,
                                isDeleted = existingProduct.isDeleted,
                                CreatedBy = existingProduct.CreatedBy,
                                CreatedDate = DateTime.Now,
                                ProductId = existingProduct.Id,
                                UpdatedBy = existingProduct.UpdatedBy,
                                UpdateDate = DateTime.Now,
                            };
                            _context.ProductImages.Add(newImage);
                    }
                    // remove old image
                    var oldImage = _context.ProductImages.Select(pa => pa.Id).ToList();
                    var imageToRemove = existingProduct.ProductImages
                        .Where(pa => oldImage.Contains(pa.Id))
                        .ToList();
                    _context.ProductImages.RemoveRange(imageToRemove);
                }
                else
                {
                    if (product.ProductImages != null && product.ProductImages.Any())
                    {
                        var productImage = product.ProductImages.Select(piDto => new ProductImage
                        {
                            Id = Guid.NewGuid(),
                            ImageName = piDto.ImageName,
                            ImageURL = piDto.ImageURL,
                            Position = piDto.Position,
                            ProductId = existingProduct.Id,
                            isActive = true,
                            isDeleted = false,
                            CreatedBy = existingProduct.CreatedBy,
                            UpdatedBy = existingProduct.UpdatedBy,
                            CreatedDate = existingProduct.CreatedDate,
                            UpdateDate = existingProduct.UpdateDate
                        }).ToList();
                        existingProduct.ProductImages = productImage;
                        _context.ProductImages.AddRange(productImage);
                    }
                }
            }
            _context.SaveChanges();
        }
        public async Task<IEnumerable<ProductsVM>> GetSaleProductsAsync(int flashSaleId)
        {
            var flashSaleProductsInclude = _context.FlashSaleProducts
                .Include(p => p.Product)
                .ThenInclude(p => p.ProductImages)
                .Include(p => p.FlashSale)
                .AsNoTracking();

            var productlist = flashSaleProductsInclude
                .Where(p => p.FlashSaleId == flashSaleId &&
                    p.FlashSale.DateOpen <= DateTime.Now &&
                    p.FlashSale.DateClose >= DateTime.Now &&
                    !p.Product.isDeleted &&
                    p.Product.isActive);
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
                isActive = fsp.Product.isActive,
                isDeleted = fsp.Product.isDeleted,
                ProductImages = fsp.Product.ProductImages.Where(p => p.isActive && !p.isDeleted).Select(img => new ProductImagesVM
                {
                    Id = img.Id,
                    Position = img.Position,
                    ImageName = img.ImageName,
                    ImageURL = img.ImageURL,
                }).ToList(),
            }).ToList();

            return result ?? new List<ProductsVM>();
        }
        public void PermanentlyDeleteAsync(Guid productId)
        {
            var product = _context.Products
                .Include(p => p.ProductAttributes)
                .Include(p => p.ProductImages)
                .FirstOrDefault(x => x.Id == productId);
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
                product.UpdateDate = DateTime.Now;
                _context.Products.Update(product);
                _context.SaveChanges();
            }
        }
    }
}
