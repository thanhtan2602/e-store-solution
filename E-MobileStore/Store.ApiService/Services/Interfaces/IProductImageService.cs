﻿using Store.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ApiService.Services.Interfaces
{
    public interface IProductImageService
    {
        public void AddOrUpdateProductImage(ProductImageDTO imageDTO, Guid productId);
    }
}
