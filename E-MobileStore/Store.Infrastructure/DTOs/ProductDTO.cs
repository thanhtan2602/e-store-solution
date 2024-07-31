using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal PriceSale { get; set; }
        public int Quantity { get; set; }
        public string ShortDesc { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool isDeleted { get; set; } = false;
        public bool isActive { get; set; } = true;
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public List<ProductImageDTO> ProductImages { get; set; }
        public List<ProductAttributeDTO> ProductAttributes { get; set; }
    }
}
