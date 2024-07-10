using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<CartItem> CartItems { get; set;}
    }
}
