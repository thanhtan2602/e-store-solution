using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.APIs.Interfaces
{
    public interface IBannerApi
    {
        string GetBannerByCate(int page, int pageSize, int categoryId);
    }
}
