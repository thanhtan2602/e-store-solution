using Store.Domain.Entities;
using Store.Infrastructure.Data;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;
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
                CreatedDate = DateTime.UtcNow,
                Description = flashSale.Description,
                isActive = true,
                UpdatedBy = flashSale.UpdatedBy,
                UpdateDate = DateTime.UtcNow,
                DateOpen = flashSale.DateOpen,
                DateClose = flashSale.DateClose,
            };
            _context.FlashSales.Add(_flashSale);
            _context.SaveChanges();
        }
    }
}
