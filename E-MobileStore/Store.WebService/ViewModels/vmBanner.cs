using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.ViewModels
{
    public class vmBanner
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public string Position { get; set; }
        public string BannerAlt { get; set; }
        public int? CategoryId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
    }
}
