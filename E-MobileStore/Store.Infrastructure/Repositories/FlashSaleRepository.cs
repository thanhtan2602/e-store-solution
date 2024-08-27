using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Infrastructure.Data;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;
using Store.Infrastructure.ViewModels;

namespace Store.Infrastructure.Repositories
{
    public class FlashSaleRepository : IFlashSaleRepository
    {
        private readonly ApplicationDbContext _context;
        public FlashSaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddOrUpdateFlashSale(FlashSaleDTO flashSale)
        {
            if (flashSale.Id > 0)
            {
                var _flashSale = _context.FlashSales.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == flashSale.Id);
                if (_flashSale == null)
                {
                    throw new Exception("FlashSale was not found");
                }
                else
                {
                    _flashSale.Title = flashSale.Title;
                    _flashSale.IsActive = flashSale.IsActive;
                    _flashSale.IsDeleted = flashSale.IsDeleted;
                    _flashSale.DateClose = flashSale.DateClose;
                    _flashSale.DateOpen = flashSale.DateOpen;
                    _flashSale.UpdatedBy = flashSale.UpdatedBy;
                    _flashSale.UpdatedDate = DateTime.Now;
                    _flashSale.Description = flashSale.Description;
                    _context.FlashSales.Update(_flashSale);
                }
            }
            else
            {
                var _flashSale = new FlashSale
                {
                    Title = flashSale.Title,
                    CreatedBy = flashSale.CreatedBy,
                    CreatedDate = DateTime.Now,
                    Description = flashSale.Description,
                    IsActive = true,
                    UpdatedBy = flashSale.UpdatedBy,
                    UpdatedDate = DateTime.Now,
                    DateOpen = flashSale.DateOpen,
                    DateClose = flashSale.DateClose,
                };
                _context.FlashSales.Add(_flashSale);
            }
            _context.SaveChanges();
        }
        public void AddListFlashSaleProduct(List<FlashSaleProductDTO> flashSaleProductDTO, int flashSaleId)
        {
            var _flashSale = _context.FlashSales.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == flashSaleId);
            if (_flashSale == null)
            {
                throw new Exception("FlashSale was not found");
            }
            else
            {
                foreach (var product in flashSaleProductDTO)
                {
                    bool isFlashSaleExists = _context.FlashSaleProducts.Any(x => x.FlashSaleId == flashSaleId && x.ProductId == product.ProductId);
                    if (isFlashSaleExists)
                    {
                        throw new Exception("The Product is already exists in this FlashSale");
                    }
                    else
                    {
                        var flashsaleProduct = new FlashSaleProduct
                        {
                            FlashSaleId = flashSaleId,
                            ProductId = product.ProductId,
                            PriceSale = product.PriceSale,
                            IsActive = product.IsActive,
                            IsDeleted = product.IsDeleted,
                        };
                        _context.FlashSaleProducts.Add(flashsaleProduct);
                        _context.SaveChanges();
                    }
                }
            }
        }
        public async Task<IEnumerable<FlashSale>> GetAllFlashSale(int page, int pageSize)
        {
            var flashsales = await _context.FlashSales
                .Include(x => x.FlashSaleProducts)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.ProductImages)
                .Where(x => x.IsActive && !x.IsDeleted && x.DateOpen <= DateTime.Now && x.DateClose >= DateTime.Now)
                .OrderByDescending(x => x.CreatedDate)
                .AsNoTracking()
                .ToListAsync();
            return flashsales ?? new List<FlashSale>();
        }
        public void DeletedFlashSale(int flashSaleId)
        {
            var _flasSale = _context.FlashSales
                .Include(x => x.FlashSaleProducts)
                .FirstOrDefault(x => x.Id == flashSaleId);
            if (_flasSale != null)
            {
                _context.FlashSales.Remove(_flasSale);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("FlashSale was not found");
            }
        }
        public void UpdateFlashSaleProduct(FlashSaleProductDTO flashSaleProductDTO)
        {
            var _flashSaleProduct = _context.FlashSaleProducts.FirstOrDefault(x => x.FlashSaleId == flashSaleProductDTO.FlashSaleId && x.ProductId == flashSaleProductDTO.ProductId);
            if (_flashSaleProduct != null)
            {
                _flashSaleProduct.FlashSaleId = flashSaleProductDTO.FlashSaleId;
                _flashSaleProduct.ProductId = flashSaleProductDTO.ProductId;
                _flashSaleProduct.PriceSale = flashSaleProductDTO.PriceSale;
                _flashSaleProduct.IsActive = flashSaleProductDTO.IsActive;
                _flashSaleProduct.IsDeleted = flashSaleProductDTO.IsDeleted;
                _context.FlashSaleProducts.Update(_flashSaleProduct);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("FlashSale was not found");
            }
        }
        public void DeletedFlashSaleProduct(FlashSaleProductDTO flashSaleProductDTO)
        {
            var _flashSaleProduct = _context.FlashSaleProducts.FirstOrDefault(x => x.FlashSaleId == flashSaleProductDTO.FlashSaleId && x.ProductId == flashSaleProductDTO.ProductId);
            if (_flashSaleProduct != null)
            {
                _flashSaleProduct.IsDeleted = flashSaleProductDTO.IsDeleted;
                _context.FlashSaleProducts.Update(_flashSaleProduct);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("FlashSale or product was not found");
            }
        }
    }
}
