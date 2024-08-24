using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Banner : BaseEntity<int>
    {
        public string ImageURL { get; set; }
        public string BannerAlt { get; set; }
        [StringLength(50)]
        public string Position { get; set; }
        public int? CategoryId { get; set; }
        public bool IsDeleted { get; set; } = false;
        [JsonIgnore]
        public Category Category { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
