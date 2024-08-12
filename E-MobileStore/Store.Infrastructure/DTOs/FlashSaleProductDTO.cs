using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.DTOs
{
    public class FlashSaleProductDTO
    {
        public int FlashSaleId { get; set; }
        public Guid ProductId { get; set; }
        public decimal PriceSale { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
