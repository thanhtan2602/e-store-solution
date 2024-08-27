using Store.ApiService.Services.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ApiService.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public void DeletedStore(int storeId)
        {
            _storeRepository.DeletedStore(storeId);
        }

        public async Task<StoreList> GetStoreByIdAsync(int storeId)
        {
            return await _storeRepository.GetStoreByIdAsync(storeId);
        }

        public async Task<IEnumerable<StoreList>> GetStoreListAsync(int page, int pageSize)
        {
            return await _storeRepository.GetStoreListAsync(page, pageSize);
        }

        public void InSertOrUpdateStore(StoreDTO storeDTO)
        {
            _storeRepository.InSertOrUpdateStore(storeDTO);
        }
    }
}
