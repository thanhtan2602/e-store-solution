﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.APIs.Interfaces
{
    public interface IProductImageApi
    {
        string AddOrUpdateProductImage(string productId);
    }
}
