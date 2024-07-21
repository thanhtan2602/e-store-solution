using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Product : BaseEntity<Guid>
    {
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]

        public decimal Price { get; set; }
        [Required]
        [StringLength(250)]
        public int Quantity { get; set; }
        public bool isDeleted { get; set; } = false;
        public string ShortDesc { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        public ICollection<FlashSaleProduct> FlashSaleProducts { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<Rate> Rates { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
