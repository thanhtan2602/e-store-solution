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
    public class FlashSaleRepository : IFlashSaleRepository
    {
        private readonly ApplicationDbContext _context;

        public FlashSaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddFlashSaleAsync(FlashSaleDTO flashSale)
        {
            var _flashSale = new FlashSale
            {
                Title = flashSale.Title,
                CreatedBy = flashSale.CreatedBy,
                CreatedDate = DateTime.Now,
                Description = flashSale.Description,
                isActive = true,
                UpdatedBy = flashSale.UpdatedBy,
                UpdateDate = DateTime.Now,
                DateOpen = flashSale.DateOpen,
                DateClose = flashSale.DateClose,
            };
            _context.FlashSales.Add(_flashSale);
            _context.SaveChanges();
        }

        public void AddFlashSaleProductAsync(List<FlashSaleProductDTO> flashSaleProductDTO, int flashSaleId)
        {
            var _flashSale = _context.FlashSales.FirstOrDefault(x => x.Id == flashSaleId);
            if (_flashSale != null)
            {
                foreach (var product in flashSaleProductDTO)
                {
                    var flashsaleProduct = new FlashSaleProduct
                    {
                        FlashSaleId = flashSaleId,
                        ProductId = product.ProductId,
                        PriceSale = product.PriceSale,
                        IsActive = product.IsActive,
                        IsDeleted = product.isDeleted,
                    };
                    _context.FlashSaleProducts.Add(flashsaleProduct);
                }
            }
            _context.SaveChanges();
        }

        public async Task<IEnumerable<FlashSalesVM>> GetAllAsync(int page = 1, int pageSize = 2)
        {
            var flashsales = _context.FlashSales.AsNoTracking();
            var listFlasSale = flashsales.Select(x => new FlashSalesVM
            {
                FlashSaleId = x.Id,
                FlashSaleTitle = x.Title,
                FlashSaleDescription = x.Description,
                DateOpen = x.DateOpen.ToLocalTime().ToString("HH:mm dd-MM-yyyy"),
                DateClose = x.DateClose.ToLocalTime().ToString("HH:mm dd-MM-yyyy"),
                isDelete = x.IsDeleted,
                isActive = x.isActive
            }).Where(f => f.isDelete == false && f.isActive == true);
            var result = listFlasSale.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return result;
        }

        public void PermanentlyDeletedAsync(int flashSaleId)
        {
            var _flasSale = _context.FlashSales
                .Include(x => x.FlashSaleProducts)
                .Where(x => x.IsDeleted == true)
                .FirstOrDefault(x => x.Id == flashSaleId);
            if (_flasSale != null)
            {
                _context.FlashSales.Remove(_flasSale);
                _context.SaveChanges();
            }
        }

        public void ManageFlashSaleAsync(int flashSaleId, FlashSaleDTO flashSaleDTO, int action)
        {
            var _flashSale = _context.FlashSales.FirstOrDefault(x => x.Id == flashSaleId);

            if (_flashSale != null)
            {
                switch (action)
                {
                    case 0: //Update
                        _flashSale.Title = flashSaleDTO.Title;
                        _flashSale.isActive = flashSaleDTO.isActive;
                        _flashSale.IsDeleted = flashSaleDTO.isDeleted;
                        _flashSale.DateClose = flashSaleDTO.DateClose;
                        _flashSale.DateOpen = flashSaleDTO.DateOpen;
                        _flashSale.UpdatedBy = flashSaleDTO.UpdatedBy;
                        _flashSale.UpdateDate = DateTime.Now;
                        _flashSale.Description = flashSaleDTO.Description;
                        _context.FlashSales.Update(_flashSale);
                        break;
                    case 1: //Active or not
                        _flashSale.isActive = flashSaleDTO.isActive;
                        _flashSale.UpdateDate = DateTime.Now;
                        _flashSale.UpdatedBy = flashSaleDTO.UpdatedBy;
                        _context.FlashSales.Update(_flashSale);
                        break;
                    case 2: //Delete
                        _flashSale.IsDeleted = true;
                        _context.FlashSales.Update(_flashSale);

                        break;
                    case 3: // Restore
                        _flashSale.IsDeleted = false;
                        _context.FlashSales.Update(_flashSale);
                        break;
                    default:
                        _flashSale.Title = flashSaleDTO.Title;
                        _flashSale.isActive = flashSaleDTO.isActive;
                        _flashSale.IsDeleted = flashSaleDTO.isDeleted;
                        _flashSale.DateClose = flashSaleDTO.DateClose;
                        _flashSale.DateOpen = flashSaleDTO.DateOpen;
                        _flashSale.UpdatedBy = flashSaleDTO.UpdatedBy;
                        _flashSale.UpdateDate = DateTime.Now;
                        _flashSale.Description = flashSaleDTO.Description;
                        _context.FlashSales.Update(_flashSale);
                        break;
                }
                _context.SaveChanges();
            }
        }

        public void ManageFlashSaleProductAsync(FlashSaleProductDTO flashSaleProductDTO, int flashSaleId, Guid productId, int action)
        {
            var _flashSaleProduct = _context.FlashSaleProducts.FirstOrDefault(x => x.FlashSaleId == flashSaleId && x.ProductId == productId);
            if (_flashSaleProduct != null)
            {
                switch (action)
                {
                    case 0: //Update 
                        _flashSaleProduct.FlashSaleId = flashSaleId;
                        _flashSaleProduct.ProductId = productId;
                        _flashSaleProduct.PriceSale = flashSaleProductDTO.PriceSale;
                        _flashSaleProduct.IsActive = flashSaleProductDTO.IsActive;
                        _flashSaleProduct.IsDeleted = flashSaleProductDTO.isDeleted;
                        _context.FlashSaleProducts.Update(_flashSaleProduct);
                        break;
                    case 1: // Active or Not Active
                        _flashSaleProduct.IsActive = flashSaleProductDTO.IsActive;
                        _context.FlashSaleProducts.Update(_flashSaleProduct);
                        break;
                    case 2: //Delete 
                        _flashSaleProduct.IsDeleted = true;
                        _context.FlashSaleProducts.Update(_flashSaleProduct);
                        break;
                    case 3: //Restore
                        _flashSaleProduct.IsDeleted = false;
                        _context.FlashSaleProducts.Update(_flashSaleProduct);
                        break;

                    default:
                        _flashSaleProduct.FlashSaleId = flashSaleId;
                        _flashSaleProduct.ProductId = productId;
                        _flashSaleProduct.PriceSale = flashSaleProductDTO.PriceSale;
                        _flashSaleProduct.IsActive = flashSaleProductDTO.IsActive;
                        _context.FlashSaleProducts.Update(_flashSaleProduct);
                        break;
                }
                _context.SaveChanges();

            }
        }
    }
}
