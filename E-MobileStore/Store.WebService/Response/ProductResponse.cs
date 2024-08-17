using Store.Domain.Entities;
using Store.WebService.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.Response
{
    public class ProductResponse:BaseResponse
    {
        public List<Product> result { get; set; }
    }
 
}
