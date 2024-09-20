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
    public class BannerWebService : IBannerWebService
    {
        private readonly IBannerApi _bannerApi;
        private readonly HttpClient _client;
        public BannerWebService(IBannerApi bannerApi)
        {
            _bannerApi = bannerApi;
            _client = new HttpClient();
        }
        public async Task<List<vmBanner>> GetBannerByCate(int page, int pageSize, int categoryId)
        {
            try
            {
                var banners = new List<vmBanner>();
                var uri = _bannerApi.GetBannerByCate(page, pageSize, categoryId);
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var responseApi = JsonConvert.DeserializeObject<BannerResponse>(content);
                    if (responseApi != null && responseApi.result.Count > 0)
                    {
                        foreach (var banner in responseApi.result)
                        {
                            banners.Add(new vmBanner
                            {
                                Id = banner.Id,
                                ImageURL = banner.ImageURL,
                                BannerAlt = banner.BannerAlt,
                                Position = banner.Position,
                                IsDeleted = banner.IsDeleted,
                                IsActive = banner.IsActive,
                                CategoryId = banner.CategoryId,
                                CreatedDate = banner.CreatedDate.ToLocalTime().ToString("HH:mm dd/MM/yyyy"),
                                UpdatedDate = banner.UpdatedDate.ToLocalTime().ToString("HH:mm dd/MM/yyyy")
                            });
                        }
                    }
                }
                return banners;
            }
            catch (Exception ex)
            {
                return new List<vmBanner>();
            }
        }
    }
}
