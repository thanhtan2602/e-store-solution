﻿using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.Response
{
    public class FlashSaleResponse:BaseResponse
    {
        public List<FlashSale> result {  get; set; }
    }
}
