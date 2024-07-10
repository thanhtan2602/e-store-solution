using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class ProductAttribute
    {
        public Guid Id { get; set; }
        public string AttributeContent { get; set; }
        public Guid ProductId { get; set; }
        public Guid AttributeId { get; set; }
        public Product Product { get; set; }
        public AttributeValue AttributeValue { get; set; }
    }
}
