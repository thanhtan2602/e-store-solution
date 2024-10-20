using Store.ApiService.Services.Interfaces;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ApiService.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;

        public ProductImageService(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }
        public void AddOrUpdateProductImage(ProductImageDTO imageDTO, Guid productId)
        {
            _productImageRepository.AddOrUpdateProductImage(imageDTO, productId);
        }
    }
}
