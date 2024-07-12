using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.ViewModel
{
    public class ProductSaleVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public decimal PriceSale { get; set; }
    
        public int Quantity { get; set; }
        public bool isDeleted { get; set; } = false;
        public string ShortDesc { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
