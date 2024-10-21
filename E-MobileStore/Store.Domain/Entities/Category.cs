using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace Store.Domain.Entities
{
    public class Category : BaseEntity<int>
    {
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        public string CategoryUrl { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        [StringLength(50)]
        public string Position { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Banner> Banners { get; set; }
        public ICollection<New> News { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
