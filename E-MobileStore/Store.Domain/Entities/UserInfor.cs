using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class UserInfor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(450)]
        public string UserId { get; set; }
        public string Avatar { get; set; }
        public string Gender { get; set; } 
        public string HouseNumber { get; set; }
        public string Ward {  get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public ApplicationUser User { get; set; }
    }
}
