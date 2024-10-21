﻿using Store.ApiService.Services.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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

        public async Task<IEnumerable<Banner>> GetAllBanner(int page, int pageSize)
        {
            return await _bannerRepository.GetAllBannerAsync(page, pageSize);

        }

        public async Task<IEnumerable<Banner>> GetBannerByCateAsync(int page, int pageSize, string categoryUrl)
        {
            return await _bannerRepository.GetBannerByCateAsync(page, pageSize, categoryUrl);
        }

        public async Task<Banner> GetBannerDetail(int bannerId)
        {
            return await _bannerRepository.GetBannerDetailAsync(bannerId);
        }
        public void InsertOrUpdateBanner(BannerDTO bannerDTO)
        {
            _bannerRepository.InsertOrUpdateBanner(bannerDTO);
        }
    }
}
