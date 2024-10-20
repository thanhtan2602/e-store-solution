using Store.WebService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.APIs.Interfaces
{
    public interface IProductApi
    {
        string TotalProductByCateAsync(string categoryUrl);
        string TotalProductAsync();
        string GetProductByUrl(string productUrl);
        string GetProductListByCateUrl(string categoryUrl, int page, int pageSize, string? sortBy);
        string GetProductList(int page, int pageSize, string? sortBy);
        string GetProductSearch(string search, int page, int pageSize);
        string GetProductBySaleId(int flashsaleId);
        string InserOrUpdateProduct();
        string DeletedProduct(string productUrl);
    }
}
