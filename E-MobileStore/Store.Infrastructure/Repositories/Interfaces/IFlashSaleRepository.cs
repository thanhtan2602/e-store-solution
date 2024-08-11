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
        void AddOrUpdateFlashSale(FlashSaleDTO flashSaleDTO);
        void AddListFlashSaleProduct(List<FlashSaleProductDTO> flashSaleProductDTO, int flashSaleId);
        Task<IEnumerable<FlashSalesVM>> GetAllFlashSale(int page, int pageSize);
        void UpdateFlashSaleProduct(FlashSaleProductDTO flashSaleProductDTO);
        void DeletedFlashSale(int flashSaleId);
        void DeletedFlashSaleProduct(FlashSaleProductDTO flashSaleProductDTO);

    }
}
