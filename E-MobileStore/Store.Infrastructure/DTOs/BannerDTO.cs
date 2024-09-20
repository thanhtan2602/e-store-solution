using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.DTOs
{
    public class BannerDTO
    {
        public int Id { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public string BannerAlt { get; set; } = string.Empty;
        public string Position { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime UpdatedDate { get; set; }

    }
}
