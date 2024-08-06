using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.DTOs
{
    public class ProductAttributeDTO
    {
        public Guid Id { get; set; }
        public Guid AttributeValueId { get; set; }
        public string? AttributeContent { get; set; }
    }
}
