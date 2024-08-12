using Store.Domain.Entities;
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
        public string ImageURL { get; set; }=string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
