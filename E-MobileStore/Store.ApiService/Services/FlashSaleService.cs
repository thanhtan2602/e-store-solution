using Store.ApiService.Services.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories;
using Store.Infrastructure.Repositories.Interfaces;
using Store.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ApiService.Services
{
    public class FlashSaleService : IFlashSaleService
    {
        private readonly IFlashSaleRepository _flashSaleRepository;

        public FlashSaleService(IFlashSaleRepository flashSaleRepository)
        {
            _flashSaleRepository = flashSaleRepository;
        }

        public void AddOrUpdateFlashSale(FlashSaleDTO flashSaleDTO)
        {
            _flashSaleRepository.AddOrUpdateFlashSale(flashSaleDTO);
        }

        public void AddListFlashSaleProduct(List<FlashSaleProductDTO> flashSaleProductDTO, int flashSaleId)
        {
            _flashSaleRepository.AddListFlashSaleProduct(flashSaleProductDTO, flashSaleId);
        }

        public async Task<IEnumerable<FlashSale>> GetAllFlashSale()
        {
            return await _flashSaleRepository.GetAllFlashSale();
        }

        public void UpdateFlashSaleProduct(FlashSaleProductDTO flashSaleProductDTO)
        {
            _flashSaleRepository.UpdateFlashSaleProduct(flashSaleProductDTO);
        }

        public void DeletedFlashSale(int flashSaleId)
        {
            _flashSaleRepository.DeletedFlashSale(flashSaleId);
        }

        public void DeletedFlashSaleProduct(FlashSaleProductDTO flashSaleProductDTO)
        {
            _flashSaleRepository.DeletedFlashSaleProduct(flashSaleProductDTO);
        }
    }
}

