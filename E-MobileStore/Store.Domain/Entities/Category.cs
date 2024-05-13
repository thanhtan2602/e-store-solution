using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
