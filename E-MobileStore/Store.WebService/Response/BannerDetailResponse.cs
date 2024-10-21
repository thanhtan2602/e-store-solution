using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.Response
{
    public class BannerDetailResponse : BaseResponse
    {
        public Banner result { get; set; }
    }

}
