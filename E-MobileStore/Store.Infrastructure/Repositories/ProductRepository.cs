using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Infrastructure.Data;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddProductAsync(Product product)
        {
            _context.Products.Add(product);
        }

        public void DeleteProductAsync(Guid productId)
        {
            var product = _context.Products.FirstOrDefault(x=>x.Id == productId);
            if(product != null)
            {
                _context.Products.Remove(product);
            }
        }

       

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            var product = _context.Products
                .Include(x=>x.Category)
                .FirstOrDefault(x => x.Id == productId);

            return product ?? new Product();
        }

     

        public async Task<IEnumerable<Product>> GetProductListAsync(int categoryId)
        {
            return _context.Products.Where(x => x.Category.Id == categoryId);
        }

        public void UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
