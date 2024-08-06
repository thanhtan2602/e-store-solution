using Store.Infrastructure.DTOs;
using Store.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ApiService.Services.Interfaces
{
    public interface ICategoryService
    {
        void AddCategoriesAsync(CategoryDTO category);
        void ManageCategoriesAsync(CategoryDTO category, int categoryId, int action);
        Task<IEnumerable<CategoryVM>> GetAllCategoriesAsync(int page, int pageSize);
        Task<CategoryVM> GetByIdAsync(int categoryId);
        void ParmanentlyCategoriesAsync(int categoryId);
    }
}
