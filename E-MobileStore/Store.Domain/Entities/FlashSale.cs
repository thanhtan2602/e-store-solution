using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class FlashSale : BaseEntity<int>
    {
        public DateTime DateOpen { get; set; }
        public DateTime DateClose { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<FlashSaleProduct> FlashSaleProducts { get; set; }
    }
}
