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
        private string baseUrl = "http://localhost:5163";  //"http://localhost:5163/api/Products/GetProductById?productId=1";

        public ProductApi()
        {
        }

        public string GetProductById(int productId)
        {
            return $"{baseUrl}/api/products/getproductbyid?productid={productId}";
        }
    }
}
