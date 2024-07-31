using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.ViewModels
{
    public class ProductImagesVM
    {
        public Guid Id { get; set; }
        public string ImageURL { get; set; }
        public string ImageName { get; set; }
        public string Position { get; set; }
    }
}
