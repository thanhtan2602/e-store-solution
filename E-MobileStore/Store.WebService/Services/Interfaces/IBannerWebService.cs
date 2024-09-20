using Store.WebService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.Services.Interfaces
{
    public interface IBannerWebService
    {
        Task<List<vmBanner>> GetBannerByCate(int page, int pageSize, int categoryId);
    }
}
