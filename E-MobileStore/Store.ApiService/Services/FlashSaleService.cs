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
        public void AddFlashSaleAsync(FlashSaleDTO flashSale)
        {
            _flashSaleRepository.AddFlashSaleAsync(flashSale);
        }

        public void AddFlashSaleProductAsync(List<FlashSaleProductDTO> flashSaleProductDTO, int flashSaleId)
        {
            _flashSaleRepository.AddFlashSaleProductAsync(flashSaleProductDTO, flashSaleId);
        }

        public Task<IEnumerable<FlashSalesVM>> GetAllAsync(int page, int pageSize)
        {
            return _flashSaleRepository.GetAllAsync(page, pageSize);
        }

        public void PermanentlyDeletedAsync(int flashSaleId)
        {
            _flashSaleRepository.PermanentlyDeletedAsync(flashSaleId);
        }

        public void ManageFlashSaleAsync(int flashSaleId, FlashSaleDTO flashSaleDTO, int action)
        {
            _flashSaleRepository.ManageFlashSaleAsync(flashSaleId, flashSaleDTO, action);
        }

        public void ManageFlashSaleProductAsync(FlashSaleProductDTO flashSaleProductDTO, int flashSaleId, Guid productId, int action)
        {
            _flashSaleRepository.ManageFlashSaleProductAsync(flashSaleProductDTO, flashSaleId, productId, action);
        }
    }
}

