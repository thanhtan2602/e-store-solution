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
        private string baseUrl = "http://localhost:5163";
        public CategoryApi() { }
        public string GetAllCategory()
        {
            return $"{baseUrl}/api/categories/getallcategory";

        }
    }
}
