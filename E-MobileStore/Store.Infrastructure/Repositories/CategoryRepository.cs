using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Infrastructure.Data;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;
using Store.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddCategoriesAsync(CategoryDTO category)
        {
            var newCategory = new Category
            {
                Name = category.Name,
                Description = category.Description,
                ImageURL = category.ImageURL,
                CreatedBy = category.CreatedBy,
                CreatedDate = DateTime.Now,
                isActive = category.isActive,
                isDeleted = false,
                UpdatedBy = string.Empty,
            };
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<CategoryVM>> GetAllCategoriesAsync(int page = 1, int pageSize = 2)
        {
            var listCate = _context.Categories.AsNoTracking();
            var result = listCate.Select(x => new CategoryVM
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageURL = x.ImageURL,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate.ToLocalTime().ToString("HH:mm dd:MM:yyyy"),
                isActive = x.isActive,
                isDeleted = x.isDeleted,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate=x.UpdateDate.ToLocalTime().ToString("HH:mm dd:MM:yyyy")
            }).Where(c=>c.isActive==true && c.isDeleted==false).ToList();
            var pagination = result.Skip((page - 1) * pageSize).Take(pageSize);
            return pagination ?? new List<CategoryVM>();
        }

        public async Task<CategoryVM> GetByIdAsync(int categoryId)
        {
            var cate = _context.Categories.FirstOrDefault(x => x.Id == categoryId);
            if (cate != null)
            {
                var result = new CategoryVM
                {
                    Id = cate.Id,
                    Name = cate.Name,
                    Description = cate.Description,
                    ImageURL = cate.ImageURL,
                    isActive = cate.isActive,
                    isDeleted = cate.isDeleted,
                    CreatedBy = cate.CreatedBy,
                    CreatedDate = cate.CreatedDate.ToLocalTime().ToString("HH:mm dd:MM:yyyy"),
                    UpdatedBy = cate.UpdatedBy,
                    UpdatedDate = cate.UpdateDate.ToLocalTime().ToString("HH:mm dd:MM:yyyy")
                };
                return result;
            }
            return new CategoryVM();
        }

        public void ManageCategoriesAsync(CategoryDTO category, int categoryId, int action)
        {
            var cate = _context.Categories.FirstOrDefault(x => x.Id == categoryId);
            if (cate != null)
            {
                switch (action)
                {
                    case 0: //Update
                        cate.Name = category.Name;
                        cate.Description = category.Description;
                        cate.ImageURL = category.ImageURL;
                        cate.isActive = category.isActive;
                        cate.isDeleted = category.isDeleted;
                        cate.UpdatedBy = category.UpdatedBy;
                        cate.UpdateDate = DateTime.Now;
                        _context.Categories.Update(cate);
                        break;
                    case 1: //Active or Not
                        cate.isActive = category.isActive;
                        _context.Categories.Update(cate);
                        break;
                    case 2: //Delete
                        cate.isDeleted = true;
                        _context.Categories.Update(cate);
                        break;
                    case 3: //ReStore
                        cate.isDeleted = false;
                        _context.Categories.Update(cate);
                        break;
                    default:
                        cate.Name = category.Name;
                        cate.Description = category.Description;
                        cate.ImageURL = category.ImageURL;
                        cate.isActive = category.isActive;
                        cate.isDeleted = category.isDeleted;
                        cate.UpdatedBy = category.UpdatedBy;
                        cate.UpdateDate = DateTime.Now;
                        _context.Categories.Update(cate);
                        break;
                }
                _context.SaveChanges();
            }
        }

        public void ParmanentlyCategoriesAsync(int categoryId)
        {
            var cate = _context.Categories.Where(c=>c.isDeleted==true).FirstOrDefault(c => c.Id == categoryId);
            if (cate != null) 
            {
                _context.Categories.Remove(cate);
                _context.SaveChanges();
            };
        }
    }
}
