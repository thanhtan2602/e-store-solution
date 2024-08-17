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
        public string GetAllBanner()
        {
            return $"{baseUrl}/api/banners/getallbanner";
        }
    }
}
