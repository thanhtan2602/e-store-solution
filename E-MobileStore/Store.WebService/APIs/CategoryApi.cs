using Store.WebService.APIs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.APIs
{
    public class CategoryApi : ICategoryApi
    {
        public CategoryApi() { }
        private string baseUrl = "http://localhost:5163";
        public string GetAllCategory(int page, int pageSize)
        {
            return $"{baseUrl}/api/categories/getallcategory?page={page}&pageSize={pageSize}";
        }

        public string InsertOrUpdateCategory()
        {
            return $"{baseUrl}/api/categories/InsertOrUpdateCategory";

        }

        public string DeleteCategory(string categoryUrl)
        {
            return $"{baseUrl}/api/categories/DeleteCategory?categoryUrl={categoryUrl}";

        }

        public string GetCategoryByUrl(string categoryUrl)
        {
            return $"{baseUrl}/api/Categories/GetCategoryByUrl?categoryUrl={categoryUrl}";

        }
    }
}
