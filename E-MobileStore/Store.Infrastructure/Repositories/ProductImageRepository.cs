using Store.Domain.Entities;
using Store.Infrastructure.Data;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddOrUpdateProductImage(ProductImageDTO imageDTO, Guid productId)
        {
            if(imageDTO.Id == "00000000-0000-0000-0000-000000000000")
            {
                var newImage = new ProductImage
                {
                    Id = Guid.NewGuid(),
                    ImageName = imageDTO.ImageName,
                    ImageURL = imageDTO.ImageURL,
                    CreatedBy = imageDTO.CreatedBy,
                    UpdatedBy = imageDTO.UpdatedBy,
                    IsDeleted = imageDTO.IsDeleted,
                    IsActive = imageDTO.IsActive,
                    Position = imageDTO.Position,
                    //Product = product,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    ProductId = productId,
                };
                _context.ProductImages.AddAsync(newImage);
            }
            else
            {
                var image= _context.ProductImages.FirstOrDefault(x=>x.Id.ToString()==imageDTO.Id);
                if (image == null)
                {
                    throw new Exception("Product image not found");
                }
                else
                {

                    image.ImageName = imageDTO.ImageName;
                    image.ImageURL = imageDTO.ImageURL;
                    image.CreatedBy = imageDTO.CreatedBy;
                    image.UpdatedBy = imageDTO.UpdatedBy;
                    image.IsDeleted = imageDTO.IsDeleted;
                    image.IsActive = imageDTO.IsActive;
                    image.Position = imageDTO.Position;
                    image.CreatedDate = DateTime.Now;
                    image.UpdatedDate = DateTime.Now;
                    image.ProductId = productId;
                };
                _context.ProductImages.Update(image);
            }
            _context.SaveChanges();
        }
    }
}
