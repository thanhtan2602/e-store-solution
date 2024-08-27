using Store.ApiService.Services.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ApiService.Services
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;
        public BannerService(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }
        public void DeletedBanner(int bannerId)
        {
            _bannerRepository.DeletedBanner(bannerId);
        }
        public async Task<IEnumerable<Banner>> GetAllBannerAsync(int page, int pageSize)
        {
            return await _bannerRepository.GetAllBannerAsync(page, pageSize);
        }
        public void InsertOrUpdateBanner(BannerDTO bannerDTO)
        {
            _bannerRepository.InsertOrUpdateBanner(bannerDTO);
        }
    }
}
