﻿using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ApiService.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductListAsync(int categoryId);
        Task<Product> GetProductByIdAsync(int productId);
        void AddProductAsync(Product product);
        void UpdateProductAsync(Product product);
        void DeleteProductAsync(int productId);
    }
}
