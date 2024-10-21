using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }= DateTime.Now;
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
    }
}
