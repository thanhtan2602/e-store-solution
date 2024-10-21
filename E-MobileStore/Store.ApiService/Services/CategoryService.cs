using Store.ApiService.Services.Interfaces;
using Store.Domain.Entities;
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
        public async Task<IEnumerable<Category>> GetCategoriesAsync(int page, int pageSize)
        {
            return await _categoryRepository.GetCategoriesAsync(page, pageSize);
        }
        public async Task<Category> GetByUrl(string categoryUrl)
        {
            return await _categoryRepository.GetByUrl(categoryUrl);
        }
        public void AddOrUpdateCategory(CategoryDTO category)
        {
            _categoryRepository.AddOrUpdateCategory(category);
        }
        public void DeleteCategory(string categoryUrl)
        {
            _categoryRepository.DeleteCategory(categoryUrl);
        }
    }
}
