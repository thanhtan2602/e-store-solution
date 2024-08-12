﻿using Store.Infrastructure.DTOs;
using Store.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        void AddOrUpdateCategory(CategoryDTO category);
        Task<IEnumerable<CategoryVM>> GetAllCategories(int page , int pageSize);
        Task<CategoryVM> GetById(int categoryId);
        void DeleteCategory(int categoryId);
    }
}