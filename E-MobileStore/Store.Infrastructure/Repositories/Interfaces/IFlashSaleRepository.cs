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
    public interface IFlashSaleRepository
    {
        void AddFlashSaleAsync(FlashSaleDTO flashSale);
        void AddFlashSaleProductAsync(List<FlashSaleProductDTO> flashSaleProductDTO, int flashSaleId);
        void ManageFlashSaleProductAsync(FlashSaleProductDTO flashSaleProductDTO, int flashSaleId, Guid productId, int action);
        void ManageFlashSaleAsync(int flashSaleId, FlashSaleDTO flashSaleDTO, int action);
        void PermanentlyDeletedAsync(int flashSaleId);
        Task<IEnumerable<FlashSalesVM>> GetAllAsync(int page, int pageSize);
    }
}
