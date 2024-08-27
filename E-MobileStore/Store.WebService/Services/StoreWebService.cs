using Newtonsoft.Json;
using Store.WebService.APIs.Interfaces;
using Store.WebService.Response;
using Store.WebService.Services.Interfaces;
using Store.WebService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.Services
{
    public class StoreWebService : IStoreWebService
    {
        private readonly IStoreApi _storeApi;
        private readonly HttpClient _client;

        public StoreWebService(IStoreApi storeApi)
        {
            _storeApi = storeApi;
            _client = new HttpClient();
        }
        public async Task<vmStore> GetStoreById(int storeId)
        {
            try
            {
                var uri = _storeApi.GetStoreById(storeId);
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var responseApi = JsonConvert.DeserializeObject<StoreResponse>(content);
                    if (responseApi != null && responseApi.result.Count > 0)
                    {
                        return new vmStore()
                        {
                            Id = responseApi.result[0].Id,
                            Adress = responseApi.result[0].Adress,
                            City = responseApi.result[0].City,
                            CreatedBy = responseApi.result[0].CreatedBy,
                            UpdatedBy = responseApi.result[0].UpdatedBy,
                            CreatedDate = responseApi.result[0].CreatedDate.ToLocalTime().ToString("HH:mm dd/MM/yyyy"),
                            UpdatedDate = responseApi.result[0].UpdatedDate.ToLocalTime().ToString("HH:mm dd/MM/yyyy"),
                            Description = responseApi.result[0].Description,
                            District = responseApi.result[0].District,
                            ImageUrl = responseApi.result[0].ImageUrl,
                            IsActive = responseApi.result[0].IsActive,
                            IsDeleted = responseApi.result[0].IsDeleted,
                            Policy = responseApi.result[0].Policy
                        };
                    }
                }
                return new vmStore();
            }
            catch (Exception ex)
            {
                return new vmStore();
            }
        }

        public async Task<List<vmStore>> GetStoreList(int page, int pageSize)
        {
            try
            {
                var stores = new List<vmStore>();
                var uri = _storeApi.GetStoreList(page, pageSize);
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var responseApi = JsonConvert.DeserializeObject<StoreResponse>(content);
                    if (responseApi != null && responseApi.result.Count > 0)
                    {
                        foreach (var store in responseApi.result)
                        {
                            stores.Add(new vmStore()
                            {
                                Adress = store.Adress,
                                District = store.District,
                                City = store.City,
                                ImageUrl = store.ImageUrl,
                                Id = store.Id,
                                Description = store.Description,
                                Policy = store.Policy,
                                CreatedBy = store.CreatedBy,
                                UpdatedBy = store.UpdatedBy,
                                CreatedDate = store.CreatedDate.ToLocalTime().ToString("HH:mm dd/MM/yyyy"),
                                UpdatedDate = store.UpdatedDate.ToLocalTime().ToString("HH:mm dd/MM/yyyy"),
                                IsDeleted = store.IsDeleted,
                                IsActive = store.IsActive,
                            });
                        }
                    }
                }
                return stores;
            }
            catch (Exception ex)
            {
                return new List<vmStore>();
            }
        }
    }
}
