using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class StoreList : BaseEntity<int>
    {
        public string City { get; set; }
        public string District { get; set; }
        public string Adress { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Policy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
