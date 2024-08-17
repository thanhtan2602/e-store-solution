using Store.WebService.APIs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.APIs
{
    public class NewsApi : INewsApi
    {
        public NewsApi() { }
        private string baseUrl = "http://localhost:5163";
        public string GetNews(int page, int pageSize)
        {
            return $"{baseUrl}/api/news/getallnews?page={page}&pageSize={pageSize}";
        }
    }
}
