﻿using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Interfaces
{
    public interface IStoreRepository
    {
        Task<IEnumerable<StoreList>> GetStoreListAsync(int page, int pageSize);
        Task<StoreList> GetStoreByIdAsync(int storeId);
        void DeletedStore(int storeId);
        void InSertOrUpdateStore(StoreDTO storeDTO);
    }
}
