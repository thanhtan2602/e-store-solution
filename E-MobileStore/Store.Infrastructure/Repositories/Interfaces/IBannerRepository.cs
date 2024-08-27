using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Interfaces
{
    public interface IBannerRepository
    {
        Task<IEnumerable<Banner>> GetAllBannerAsync(int page, int pageSize);
        void InsertOrUpdateBanner(BannerDTO bannerDTO);
        void DeletedBanner(int bannerId);
    }
}
