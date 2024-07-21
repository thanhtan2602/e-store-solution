using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string HouseNumber { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public DateTime CreatedDate { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
