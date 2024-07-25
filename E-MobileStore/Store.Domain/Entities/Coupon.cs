using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Coupon : BaseEntity<int>
    {
        [Required]
        [StringLength(100)]
        public string Code { get; set; }
        [Required]
        [StringLength(100)]
        public string DiscoutType { get; set; }
        [Required]
        public decimal DiscountAmount { get; set; }
        public decimal MinimumPurchaseAmount { get; set; }
        public DateTime ExpirationDate { get; set; }
        [StringLength(100)]
        public int UsageLimit { get; set; }


    }
}
