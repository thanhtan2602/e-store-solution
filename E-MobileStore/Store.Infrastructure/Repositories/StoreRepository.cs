using Microsoft.EntityFrameworkCore;
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
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _context;

        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void DeletedStore(int storeId)
        {
            var store = _context.Stores.FirstOrDefault(x => x.Id == storeId);
            if (store != null)
            {
                store.IsDeleted = true;
                store.IsActive = false;
                store.UpdatedDate = DateTime.Now;
                _context.Stores.Update(store);
                _context.SaveChanges();
            }
            throw new Exception("not found");
        }

        public async Task<StoreList> GetStoreByIdAsync(int storeId)
        {
            var store = await _context.Stores
                .Where(x => x.IsActive && !x.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == storeId);
            return store ?? new StoreList();
        }

        public async Task<IEnumerable<StoreList>> GetStoreListAsync(int page, int pageSize)
        {
            var storeList = await _context.Stores
                .Where(x => x.IsActive && !x.IsDeleted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
            return storeList ?? new List<StoreList>();
        }

        public void InSertOrUpdateStore(StoreDTO storeDTO)
        {
            if (storeDTO.Id > 0)
            {
                var store = _context.Stores.Where(x => !x.IsDeleted).FirstOrDefault(x => x.Id == storeDTO.Id);
                if (store != null)
                {
                    store.Adress = storeDTO.Adress;
                    store.City = storeDTO.City;
                    store.District = storeDTO.District;
                    store.ImageUrl = storeDTO.ImageUrl;
                    store.Description = storeDTO.Description;
                    store.Policy = storeDTO.Policy;
                    store.UpdatedBy = storeDTO.UpdatedBy;
                    store.UpdatedDate = DateTime.Now;
                    _context.Stores.Update(store);
                }
                else
                {
                    throw new Exception("Not found");
                }
            }
            else
            {
                var newStore = new StoreList()
                {
                    Id = storeDTO.Id,
                    Adress = storeDTO.Adress,
                    District = storeDTO.District,
                    City = storeDTO.City,
                    Description = storeDTO.Description,
                    Policy = storeDTO.Policy,
                    ImageUrl = storeDTO.ImageUrl,
                    CreatedBy = storeDTO.CreatedBy,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    IsActive = true,
                };
                _context.Add(newStore);
            }
            _context.SaveChanges();
        }
    }
}
