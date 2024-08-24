using Store.WebService.APIs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.APIs
{

    public class FlashSaleApi : IFlashSaleApi
    {
        private string baseUrl = "http://localhost:5163";
        public string GetFlashSale(int page, int pageSize)
        {
            return $"{baseUrl}/api/FlashSale/GetListFlashSale?page={page}&pageSize={pageSize}";
        }
    }
}
