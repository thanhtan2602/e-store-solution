using Store.ApiService.Services.Interfaces;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;
using Store.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ApiService.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void AddCategoriesAsync(CategoryDTO category)
        {
            _categoryRepository.AddCategoriesAsync(category);
        }

        public async Task<IEnumerable<CategoryVM>> GetAllCategoriesAsync(int page, int pageSize)
        {
            return await _categoryRepository.GetAllCategoriesAsync(page, pageSize);

        }

        public async Task<CategoryVM> GetByIdAsync(int categoryId)
        {
            return await _categoryRepository.GetByIdAsync(categoryId);
        }

        public void ManageCategoriesAsync(CategoryDTO category, int categoryId, int action)
        {
            _categoryRepository.ManageCategoriesAsync(category, categoryId, action);
        }

        public void ParmanentlyCategoriesAsync(int categoryId)
        {
            _categoryRepository.ParmanentlyCategoriesAsync(categoryId);
        }
    }
}
