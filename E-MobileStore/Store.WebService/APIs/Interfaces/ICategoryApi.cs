using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.APIs.Interfaces
{
    public interface ICategoryApi
    {
        string GetAllCategory(int page, int pageSize);
        string InsertOrUpdateCategory();
        string DeleteCategory(string categoryUrl);
        string GetCategoryByUrl(string categoryUrl);
    }
}
