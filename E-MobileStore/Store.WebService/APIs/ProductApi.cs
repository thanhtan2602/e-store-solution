using Store.Domain.Entities;
using Store.WebService.APIs.Interfaces;
using Store.WebService.DTO;
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
		public string GetProductBySaleId(int flashsaleId)
		{
			return $"{baseUrl}/api/products/GetSaleProducts?flashSaleId={flashsaleId}";

		}

		public string GetProductByUrl(string productUrl)
		{
			return $"{baseUrl}/api/products/GetProductByUrl?productUrl={productUrl}";
		}

		public string GetProductList(int page, int pageSize, string? sortBy)
		{
			return $"{baseUrl}/api/products/GetProductList?page={page}&pageSize={pageSize}&sortBy={sortBy}";

		}

		public string GetProductListByCateUrl(string categoryUrl, int page, int pageSize, string? sortBy)
		{
			return $"{baseUrl}/api/products/GetProductListByCateUrl?cateUrl={categoryUrl}&page={page}&pageSize={pageSize}&sortBy={sortBy}";
		}

		public string GetProductSearch(string search, int page, int pageSize)
		{
			return $"{baseUrl}/api/products/GetProductSearch?search={search}&page={page}&pageSize={pageSize}";

		}
		public string InserOrUpdateProduct()
		{
			return $"{baseUrl}/api/Products/InsertOrUpdateProduct";

		}

		public string TotalProductByCateAsync(string categoryUrl)
		{
			return $"{baseUrl}/api/products/TotalProductByCate?cateUrl={categoryUrl}";

		}
		public string TotalProductAsync()
		{
			return $"{baseUrl}/api/products/TotalProduct";

		}

        public string DeletedProduct(string productUrl)
        {
            return $"{baseUrl}/api/products/DeleteProduct?productUrl={productUrl}";

        }
    }
}
