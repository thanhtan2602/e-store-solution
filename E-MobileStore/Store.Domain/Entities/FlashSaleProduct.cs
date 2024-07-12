using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class FlashSaleProduct
    {
        public Guid ProductId { get; set; }
        public int FlashSaleId { get; set; }
        public decimal PriceSale {  get; set; }
        public Product Product { get; set; }
        public FlashSale FlashSale { get; set; }
    }
}
