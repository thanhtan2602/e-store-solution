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
    public class ProductWebService : IProductWebService
    {
        private readonly HttpClient _httpClient;
        private readonly IProductApi _productApi;
        public ProductWebService(IProductApi productApi)
        {
            _httpClient = new HttpClient();
            _productApi = productApi;
        }
        public async Task<vmProduct> GetProductDetail(int productId)
        {
            try
            {
                var uri = _productApi.GetProductById(productId);
                var response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<Product>(content);
                    if (product != null)
                    {
                        return new vmProduct
                        {
                            ProductId = product.Id,
                            ProductName = product.Name,
                            Price = product.Price,
                            CategoryName = product.Category.Name
                        };
                    }
                }
                return new vmProduct();
            }
            catch (Exception ex)
            {
                return new vmProduct();
            }
        }
        public async Task<List<vmProduct>> GetProductListByCateId(int cateId, int page, int pageSize)
        {
            try
            {
                var products = new List<vmProduct>();
                var uri = _productApi.GetProductListByCateId(cateId, page, pageSize);
                var response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var productresponse = JsonConvert.DeserializeObject<ProductResponse>(content);
                    if (productresponse != null && productresponse.result.Count > 0)
                    {
                        foreach (var product in productresponse.result)
                        {
                            products.Add(new vmProduct
                            {
                                ProductId = product.Id,
                                ProductName = product.Name,
                                Price = product.Price,
                                ShortDesc = product.ShortDesc,
                                Description = product.Description,
                                CategoryId = product.CategoryId,
                                CategoryName = product.Category.Name,
                                Quantity = product.Quantity,
                                ProductImages = product.ProductImages,
                                PriceSale = product.PriceSale,
                                //ProductAttributes = product.ProductAttributes,
                                IsDeleted = product.IsDeleted,
                                IsActive = product.IsActive,
                                CreatedBy = product.CreatedBy,
                                CreatedDate = product.CreatedDate.ToLocalTime().ToString("HH:mm dd/MM/yyyy"),
                                UpdatedBy = product.UpdatedBy,
                                UpdatedDate = product.UpdatedDate.ToLocalTime().ToString("HH:mm dd/MM/yyyy"),
                            });
                        }
                    }
                }
                return products;
            }
            catch (Exception ex)
            {
                return new List<vmProduct>();
            }
        }

        public async Task<List<vmProduct>> GetProductSearch(string search, int page, int pageSize)
        {
            try
            {
                var products = new List<vmProduct>();
                var uri = _productApi.GetProductSearch(search, page, pageSize);
                var response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var productresponse = JsonConvert.DeserializeObject<ProductResponse>(content);
                    if (productresponse != null && productresponse.result.Count > 0)
                    {
                        foreach (var product in productresponse.result)
                        {
                            products.Add(new vmProduct
                            {
                                ProductId = product.Id,
                                ProductName = product.Name,
                                Price = product.Price,
                                ShortDesc = product.ShortDesc,
                                Description = product.Description,
                                CategoryId = product.CategoryId,
                                CategoryName = product.Category.Name,
                                Quantity = product.Quantity,
                                ProductImages = product.ProductImages,
                                PriceSale = product.PriceSale,
                                //ProductAttributes = product.ProductAttributes,
                                IsDeleted = product.IsDeleted,
                                IsActive = product.IsActive,
                                CreatedBy = product.CreatedBy,
                                CreatedDate = product.CreatedDate.ToLocalTime().ToString("HH:mm dd/MM/yyyy"),
                                UpdatedBy = product.UpdatedBy,
                                UpdatedDate = product.UpdatedDate.ToLocalTime().ToString("HH:mm dd/MM/yyyy"),
                            });
                        }
                    }
                }
                return products;
            }
            catch (Exception ex)
            {
                return new List<vmProduct>();
            }
        }
    }
}
