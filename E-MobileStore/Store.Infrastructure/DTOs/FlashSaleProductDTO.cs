using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.DTOs
{
    public class FlashSaleProductDTO
    {
        public Guid ProductId { get; set; }
        public decimal PriceSale { get; set; }
        public bool IsActive { get; set; } = true;
        public bool isDeleted { get; set; } = false;
    }
}
