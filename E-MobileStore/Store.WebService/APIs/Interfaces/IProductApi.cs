﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.APIs.Interfaces
{
    public interface IProductApi
    {
        string TotalProductAsync(int categoryId);
        string GetProductById(Guid productId);
		string GetProductListByCateId(int cateId, int page, int pageSize, string? sortBy);
        string GetProductSearch(string search, int page, int pageSize);
    }
}
