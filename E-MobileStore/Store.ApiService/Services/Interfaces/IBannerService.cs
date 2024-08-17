using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ApiService.Services.Interfaces
{
    public interface IBannerService
    {
        Task<IEnumerable<Banner>> GetAllBannerAsync();
        void DeletedBanner(int bannerId);
        void InsertOrUpdateBanner(BannerDTO bannerDTO);
    }
}
