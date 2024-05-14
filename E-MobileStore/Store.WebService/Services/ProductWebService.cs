using Newtonsoft.Json;
using Store.Domain.Entities;
using Store.WebService.APIs.Interfaces;
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

        public async Task<vmProduct> GetProductDetail(int categoryId)
        {

            var uri = _productApi.GetProductById(categoryId);

            var response = await _httpClient.GetStringAsync(uri);
            var product = JsonConvert.DeserializeObject<Product>(response);

            return new vmProduct();
        }
    }
}
