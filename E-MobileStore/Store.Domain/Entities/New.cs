using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public Category Category { get; set; }
    }
}
