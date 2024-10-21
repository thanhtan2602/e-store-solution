using Store.WebService.DTO;
using Store.WebService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.Services.Interfaces
{
	public interface IProductWebService
	{
		Task<int> TotalProductByCate(string categoryUrl);
		Task<string> DeleteProduct(string productUrl);
		Task<int> TotalProduct();
		Task<vmProduct> GetProductDetail(string productUrl);
		Task<List<vmProduct>> GetProductListByCateUrl(string categoryUrl, int page, int pageSize, string? sortBy);
		Task<List<vmProduct>> GetProductList(int page, int pageSize, string? sortBy);
		Task<List<vmProduct>> GetProductSearch(string search, int page, int pageSize);
		Task<List<vmProduct>> GetProductBySaleId(int flashsaleId);
		Task<string> InserOrUpdateProduct(ProductDTO productDTO, List<ProductImageDTO> Images);
	}
}
