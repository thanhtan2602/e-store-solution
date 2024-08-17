using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.Response
{
    public abstract class BaseResponse
    {
        public int statusCode { get; set; }
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public string id { get; set; }
        public class ErrorMessages
        {
            public string id { get; set; }
            public string values { get; set; }
        }
    }

}
