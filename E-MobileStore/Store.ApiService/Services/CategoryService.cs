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
        public async Task<IEnumerable<CategoryVM>> GetAllCategories(int page, int pageSize)
        {
            return await _categoryRepository.GetAllCategories(page, pageSize);

        }
        public async Task<CategoryVM> GetById(int categoryId)
        {
            return await _categoryRepository.GetById(categoryId);
        }
        public void AddOrUpdateCategory(CategoryDTO category)
        {
            _categoryRepository.AddOrUpdateCategory(category);
        }
        public void DeleteCategory(int categoryId)
        {
            _categoryRepository.DeleteCategory(categoryId);
        }
    }
}
