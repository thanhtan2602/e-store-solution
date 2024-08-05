using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.DTOs
{
    public class FlashSaleDTO
    {
        public DateTime DateOpen { get; set; }
        public DateTime DateClose { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; } = string.Empty;
        public bool isDeleted { get; set; } = false;
        public bool isActive { get; set; } = true;
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
