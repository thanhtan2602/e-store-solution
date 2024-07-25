using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class AttributeValue
    {
        public Guid Id { get; set; }
        public string AttributeName { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; set; }
    }
}
