using Newtonsoft.Json;
using Store.WebService.APIs;
using Store.WebService.APIs.Interfaces;
using Store.WebService.DTO;
using Store.WebService.Response;
using Store.WebService.Services.Interfaces;
using Store.WebService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public async Task<List<vmBanner>> GetBannerByCate(int page, int pageSize, string categoryUrl)
        {
            try
            {
                var banners = new List<vmBanner>();
                var uri = _bannerApi.GetBannerByCate(page, pageSize, categoryUrl);
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
                                CategoryName = banner.Category.Name,
                                CategoryUrl = banner.Category.CategoryUrl,
                                CreatedDate = banner.CreatedDate.ToLocalTime().ToString("HH:mm dd/MM/yyyy"),
                                UpdatedDate = banner.UpdatedDate.ToLocalTime().ToString("HH:mm dd/MM/yyyy"),
                                CreatedBy=banner.CreatedBy,
                                UpdatedBy=banner.UpdatedBy,
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
        public async Task<string> InsertOrUpdateBanner(BannerDTO bannerDTO)
        {
            try
            {
                //add banner
                var uri = _bannerApi.InsertOrUpdateBanner();
                var jsonContent = JsonConvert.SerializeObject(bannerDTO);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(uri, httpContent);
                var responseApi = await response.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<AuthenResponse>(responseApi);
                if (response.IsSuccessStatusCode && content != null)
                {
                    var result = content.statusCode.ToString();
                    return result;
                }
                else
                {
                    var result = content.message;
                    return result;
                }
            }
            catch
            {
                return "404";
            }
        }
        public async Task<string> DeleteBanner(int bannerId)
        {
            try
            {
                //add banner
                var uri = _bannerApi.DeleteBanner(bannerId);
                var jsonContent = JsonConvert.SerializeObject(bannerId);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync(uri, httpContent);
                var responseApi = await response.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<AuthenResponse>(responseApi);
                var result = "";
                if (response.IsSuccessStatusCode && content != null)
                {
                    result = content.statusCode.ToString();
                }
                else
                {
                    result = content.message;
                }
                return result;
            }
            catch
            {
                return "404";
            }
        }

        public async Task<List<vmBanner>> GetAllBanner(int page, int pageSize)
        {
            try
            {
                var banners = new List<vmBanner>();
                var uri = _bannerApi.GetAllBanner(page, pageSize);
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
                                CategoryName = banner.Category.Name,
                                CategoryUrl = banner.Category.CategoryUrl,
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

        public async Task<vmBanner> GetBannerDetail(int bannerId)
        {
            try
            {
                var uri = _bannerApi.GetBannerDetail(bannerId);
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var bannerresponse = JsonConvert.DeserializeObject<BannerDetailResponse>(content);
                    if (bannerresponse != null && bannerresponse.result != null)
                    {
                        var banner = bannerresponse.result;
                        return new vmBanner()
                        {
                            Id = banner.Id,
                            BannerAlt = banner.BannerAlt,
                            CategoryId = banner.Category.Id,
                            ImageURL = banner.ImageURL,
                            Position = banner.Position,
                            IsDeleted = banner.IsDeleted,
                            IsActive = banner.IsActive,
                            CreatedBy = banner.CreatedBy,
                            CategoryName=banner.Category.Name,
                            CategoryUrl=banner.Category.CategoryUrl,
                            CreatedDate = banner.CreatedDate.ToLocalTime().ToString("HH:mm dd/MM/yyyy"),
                            UpdatedBy = banner.UpdatedBy,
                            UpdatedDate = banner.UpdatedDate.ToLocalTime().ToString("HH:mm dd/MM/yyyy"),
                        };
                    }
                }
                return new vmBanner();
            }
            catch
            {
                return new vmBanner();
            }
        }
    }
}
