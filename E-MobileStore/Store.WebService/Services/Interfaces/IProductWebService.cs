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
		Task<int> TotalProduct(int categoryId);
		Task<vmProduct> GetProductDetail(Guid productId);
		Task<List<vmProduct>> GetProductListByCateId(int cateId, int page, int pageSize, string? sortBy);
		Task<List<vmProduct>> GetProductSearch(string search, int page, int pageSize);
	}
}
