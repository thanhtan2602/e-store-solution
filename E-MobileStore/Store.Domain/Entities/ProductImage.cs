using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class ProductImage : BaseEntity<Guid>
    {
        public string ImageURL { get; set; }
        [StringLength(150)]
        public string ImageName { get; set; }
        [StringLength(150)]
        public string Position { get; set; }
        public bool isDeleted { get; set; } = false;
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
