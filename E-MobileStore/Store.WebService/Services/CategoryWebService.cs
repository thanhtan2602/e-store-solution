using Newtonsoft.Json;
using Store.Domain.Entities;
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
    public class CategoryWebService : ICategoryWebService
    {
        private readonly HttpClient _httpClient;
        private readonly ICategoryApi _categoryApi;
        public CategoryWebService(ICategoryApi categoryApi)
        {
            _httpClient = new HttpClient();
            _categoryApi = categoryApi;
        }
        public async Task<List<vmCategory>> GetAllCategory(int page, int pageSize)
        {
            try
            {
                var categories = new List<vmCategory>();
                var uri = _categoryApi.GetAllCategory(page, pageSize);
                var response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<CategoryResponse>(content);
                    if (apiResponse != null && apiResponse.result.Count > 0)
                    {
                        foreach (var category in apiResponse.result)
                        {
                            categories.Add(new vmCategory
                            {
                                Id = category.Id,
                                Name = category.Name,
                                ImageURL = category.ImageURL,
                                IsActive = category.IsActive,
                                CreatedBy = category.CreatedBy,
                                Position = category.Position,
                                Description = category.Description,
                                IsDeleted = category.IsDeleted,
                                CreatedDate = category.CreatedDate.ToLocalTime().ToString("HH:mm dd/MM/yyyy"),
                                UpdatedBy = category.UpdatedBy,
                                UpdatedDate = category.UpdatedDate.ToLocalTime().ToString("HH:mm dd/MM/yyyy"),
                                Products = category.Products
                            });
                        }
                    }
                }
                return categories;
            }
            catch (Exception ex)
            {
                return new List<vmCategory>();
            }
        }
    }
}
