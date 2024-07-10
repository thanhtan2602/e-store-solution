using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Rate:BaseEntity<Guid>
    {
        [Range(0,5)]
        public double Score {  get; set; }  
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
