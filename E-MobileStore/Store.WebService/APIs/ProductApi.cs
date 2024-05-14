using Store.WebService.APIs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.APIs
{
    public class ProductApi : IProductApi
    {
        private string baseUrl = "http://localhost:5165";  //"http://localhost:5165/api/Products/GetProductById";

        public ProductApi()
        {
        }

        public string GetProductById(int productId)
        {
            return $"{baseUrl}/api/product/getproductbyid?productid={productId}";
        }
    }
}
