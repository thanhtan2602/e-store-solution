﻿using Microsoft.EntityFrameworkCore;
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
using static Store.Common.Utility.ProductsUtility;
namespace Store.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync(int page, int pageSize)
        {
            var result = await _context.Categories
                .Include(x => x.Products.Where(p => p.IsActive && !p.IsDeleted))
                .ThenInclude(x => x.ProductImages)
                .Where(x => x.IsActive && !x.IsDeleted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
            return result ?? new List<Category>();
        }
        public async Task<Category> GetByUrl(string categoryUrl)
        {
            var cate = _context.Categories.Include(x=>x.Products).FirstOrDefault(x => x.CategoryUrl == categoryUrl);
            if (cate == null)
            {
                throw new Exception("This Category was not found");
            }
            else
            {
				return cate ?? new Category();
			}
        }
        public void AddOrUpdateCategory(CategoryDTO category)
        {
            try
            {
                if (category.Id == 0)
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
                            CategoryUrl = ToUrl(category.Name),
                            Description = category.Description,
                            ImageURL = category.ImageURL,
                            CreatedBy = category.CreatedBy,
                            Position = category.Position,
                            CreatedDate = DateTime.Now,
                            IsActive = category.IsActive,
                            IsDeleted = false,
                            UpdatedBy = category.UpdatedBy,
                        };
                        _context.Categories.Add(newCategory);
                    }
                }
                else
                {
                    var cate = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
                    if (cate == null)
                    {
                        throw new Exception("This Category was not found");
                    }
                    else
                    {
                        cate.Name = category.Name;
                        cate.CategoryUrl = ToUrl(category.Name);
                        cate.Description = category.Description;
                        cate.Position = category.Position;
                        cate.ImageURL = category.ImageURL;
                        cate.IsActive = category.IsActive;
                        cate.IsDeleted = category.IsDeleted;
                        cate.UpdatedBy = category.UpdatedBy;
                        cate.UpdatedDate = DateTime.Now;
                        _context.Categories.Update(cate);
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while saving category. See inner exception for details.", ex);
            }
        }
        public void DeleteCategory(string categoryUrl)
        {
            var cate = _context.Categories.FirstOrDefault(c => c.CategoryUrl == categoryUrl);
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
