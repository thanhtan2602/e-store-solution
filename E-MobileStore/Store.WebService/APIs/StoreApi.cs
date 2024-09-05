using Store.WebService.APIs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.APIs
{
    public class StoreApi : IStoreApi
    {
        private string baseUrl = "http://localhost:5163";

        public string GetStoreList(int page, int pageSize)
        {
            return $"{baseUrl}/api/Stores/GetStoreList?page={page}&pageSize={pageSize}";
        }
    }
}
