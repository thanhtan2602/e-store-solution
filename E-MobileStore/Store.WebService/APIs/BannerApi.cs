using Store.Domain.Entities;
using Store.WebService.APIs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.APIs
{
    public class BannerApi : IBannerApi
    {
        public BannerApi() { }
        private string baseUrl = "http://localhost:5163";
		//https://localhost:7031/api/Banners/GetBannerByCate?page=1&pageSize=100&categoryId=15
		public string GetBannerByCate(int page, int pageSize, int categoryId)
        {
            return $"{baseUrl}/api/banners/GetBannerByCate?page={page}&pageSize={pageSize}&categoryId={categoryId}";
        }
    }
}
