using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.ViewModels
{
    public class FlashSalesVM
    {
        public int FlashSaleId { get; set; }
        public decimal PriceSale { get; set; }
        public string FlashSaleTitle { get; set; }
        public string FlashSaleDescription { get; set; }
        public string DateOpen { get; set; }
        public string DateClose { get; set; }
        public bool isActive { get; set; }
        public bool isDelete { get; set; }
    }
}
