using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.ViewModels
{
    public class vmProduct
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
      
        public decimal PriceSale { get; set; }

        public int Quantity { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; }

        public string ShortDesc { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }
        //public ICollection<ProductAttribute> ProductAttributes { get; set; }
        public ICollection<Rate> Rates { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
