using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.APIs.Interfaces
{
    public interface IBannerApi
    {
        string GetBannerByCate(int page, int pageSize, string categoryUrl);
        string GetAllBanner(int page, int pageSize);
        string GetBannerDetail(int bannerId);
        string InsertOrUpdateBanner();
        string DeleteBanner(int bannerId);
    }
}
