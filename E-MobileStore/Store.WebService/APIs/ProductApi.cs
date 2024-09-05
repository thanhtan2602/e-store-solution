using Store.Domain.Entities;
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
        public string GetProductById(Guid productId)
        {
            return $"{baseUrl}/api/products/getproductbyid?productid={productId}";
        }
        public string GetProductListByCateId(int cateId, int page, int pageSize)
        {
            return $"{baseUrl}/api/products/getproductlistbycateid?cateid={cateId}&page={page}&pageSize={pageSize}";
        }

        public string GetProductSearch(string search, int page, int pageSize)
        {
            return $"{baseUrl}/api/products/GetProductSearch?search={search}&page={page}&pageSize={pageSize}";

        }
    }
}
