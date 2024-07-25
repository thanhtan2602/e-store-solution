using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class New : BaseEntity<int>
    {
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Category Category { get; set; }
    }
}
