﻿using Store.WebService.DTO;
using Store.WebService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.Services.Interfaces
{
    public interface ICategoryWebService
    {
        Task<List<vmCategory>> GetAllCategory(int page, int pageSize);
        Task<vmCategory> GetCategoryByURL(string categoryUrl);
        Task<string> InsertOrUpdateCategory(CategoryDTO categoryDTO);

        Task<string> DeleteCategory(string categoryUrl);
    }
}
