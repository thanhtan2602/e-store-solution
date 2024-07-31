using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.DTOs
{
    public class ProductImageDTO
    {
        public Guid Id { get; set; }
        public string ImageURL { get; set; }
        public string ImageName { get; set; }
        public string Position { get; set; }
        public bool isDeleted { get; set; } = false;
        public bool isActive { get; set; } = true;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
