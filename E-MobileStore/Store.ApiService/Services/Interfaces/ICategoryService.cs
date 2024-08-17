using Store.Domain.Entities;
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
        void AddOrUpdateCategory(CategoryDTO category);
        Task<IEnumerable<Category>> GetAllCategories();
        Task<CategoryVM> GetById(int categoryId);
        void DeleteCategory(int categoryId);
    }
}
