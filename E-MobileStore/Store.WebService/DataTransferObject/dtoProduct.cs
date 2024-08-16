using Store.Domain.Entities;
using Store.WebService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.DataTransferObject
{
    public class dtoProduct
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public decimal PriceSale { get; set; }

        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public string ShortDesc { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public List<vmProductImage> ProductImages { get; set; }
        //public ICollection<ProductAttributesVM> ProductAttributes { get; set; }
        public ICollection<Rate> Rates { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; internal set; }
    }
}
