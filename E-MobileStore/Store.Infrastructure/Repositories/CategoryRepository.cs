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
        public Task<IEnumerable<CategoryVM>> GetAllCategories(int page = 1, int pageSize = 2)
        {
            var listCate = _context.Categories
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Where(x => x.IsActive)
                .AsNoTracking();
            var result = listCate.Select(x => new CategoryVM
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageURL = x.ImageURL,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate.ToLocalTime().ToString("HH:mm dd:MM:yyyy"),
                isActive = x.IsActive,
                isDeleted = x.isDeleted,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdateDate.ToLocalTime().ToString("HH:mm dd:MM:yyyy")
            }).Where(c => c.isDeleted == false).ToList();
            if (result == null)
            {
                throw new Exception("There are no category");
            }
            else
            {
                return Task.FromResult<IEnumerable<CategoryVM>>(result);
            }
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
                    isActive = cate.IsActive,
                    isDeleted = cate.isDeleted,
                    CreatedBy = cate.CreatedBy,
                    CreatedDate = cate.CreatedDate.ToLocalTime().ToString("HH:mm dd:MM:yyyy"),
                    UpdatedBy = cate.UpdatedBy,
                    UpdatedDate = cate.UpdateDate.ToLocalTime().ToString("HH:mm dd:MM:yyyy")
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
                    cate.isDeleted = category.isDeleted;
                    cate.UpdatedBy = category.UpdatedBy;
                    cate.UpdateDate = DateTime.Now;
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
                        isDeleted = false,
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
                cate.isDeleted = true;
                cate.UpdateDate = DateTime.Now;
                _context.Categories.Update(cate);
                _context.SaveChanges();
            };
        }
    }
}
