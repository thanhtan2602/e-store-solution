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
        public string GetBannerByCate(int page, int pageSize, string categoryUrl)
        {
            return $"{baseUrl}/api/banners/GetBannerByCate?page={page}&pageSize={pageSize}&categoryUrl={categoryUrl}";
        }
        public string InsertOrUpdateBanner()
        {
            return $"{baseUrl}/api/banners/InsertOrUpdateBanner";

        }

        public string DeleteBanner(int bannerId)
        {
            return $"{baseUrl}/api/banners/DeleteBanner?bannerId={bannerId}";

        }

        public string GetAllBanner(int page, int pageSize)
        {
            return $"{baseUrl}/api/banners/GetAllBanner?page={page}&pageSize={pageSize}";
        }

        public string GetBannerDetail(int bannerId)
        {
            return $"{baseUrl}/api/banners/GetBannerDetail?bannerId={bannerId}";
        }
    }
}
