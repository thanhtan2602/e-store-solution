using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Infrastructure.Data;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;
using Store.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var result = await _context.Categories.Include(x=>x.Products).ThenInclude(x=>x.ProductImages)
                .Where(x => x.IsActive && !x.IsDeleted).ToListAsync();
            return result != null ? result : new List<Category>();

        }
        public Task<CategoryVM> GetById(int categoryId)
        {
            var cate = _context.Categories.FirstOrDefault(x => x.Id == categoryId);
            if (cate == null)
            {
                throw new Exception("This Category was not found");

            }
            else
            {
                var result = new CategoryVM
                {
                    Id = cate.Id,
                    Name = cate.Name,
                    Description = cate.Description,
                    ImageURL = cate.ImageURL,
                    IsActive = cate.IsActive,
                    IsDeleted = cate.IsDeleted,
                    CreatedBy = cate.CreatedBy,
                    CreatedDate = cate.CreatedDate.ToLocalTime().ToString("HH:mm dd:MM:yyyy"),
                    UpdatedBy = cate.UpdatedBy,
                    UpdatedDate = cate.UpdatedDate.ToLocalTime().ToString("HH:mm dd:MM:yyyy")
                };
                return Task.FromResult(result);
            }
        }
        public void AddOrUpdateCategory(CategoryDTO category)
        {
            if (category.Id > 0)
            {
                var cate = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
                if (cate == null)
                {
                    throw new Exception("This Category was not found");
                }
                else
                {
                    cate.Name = category.Name;
                    cate.Description = category.Description;
                    cate.ImageURL = category.ImageURL;
                    cate.IsActive = category.isActive;
                    cate.IsDeleted = category.isDeleted;
                    cate.UpdatedBy = category.UpdatedBy;
                    cate.UpdatedDate = DateTime.Now;
                    _context.Categories.Update(cate);
                }
            }
            else
            {
                bool isExistCateName = _context.Categories.Any(x => x.Name == category.Name);
                if (isExistCateName)
                {
                    throw new Exception("This Category Name is already exists");
                }
                else
                {
                    var newCategory = new Category
                    {
                        Name = category.Name,
                        Description = category.Description,
                        ImageURL = category.ImageURL,
                        CreatedBy = category.CreatedBy,
                        CreatedDate = DateTime.Now,
                        IsActive = category.isActive,
                        IsDeleted = false,
                        UpdatedBy = string.Empty,
                    };
                    _context.Categories.Add(newCategory);
                }
            }
            _context.SaveChanges();
        }
        public void DeleteCategory(int categoryId)
           {
            var cate = _context.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (cate == null)
            {
                throw new Exception("Category was not found");
            }
            else
            {
                cate.IsDeleted = true;
                cate.UpdatedDate = DateTime.Now;
                _context.Categories.Update(cate);
                _context.SaveChanges();
            };
        }
    }
}
