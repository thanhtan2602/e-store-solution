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
        public string FlashSaleTitle { get; set; } = string.Empty;
        public string FlashSaleDescription { get; set; } = string.Empty;
        public string DateOpen { get; set; } = string.Empty;
        public string DateClose { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
