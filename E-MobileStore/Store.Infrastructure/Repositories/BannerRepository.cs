﻿using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Infrastructure.Data;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories
{
    public class BannerRepository : IBannerRepository
    {
        private readonly ApplicationDbContext _context;

        public BannerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeletedBanner(int bannerId)
        {
            var banner = _context.Banners.FirstOrDefault(x => x.Id == bannerId);
            if (banner == null)
            {
                throw new Exception("Banner was not found");
            }
            else
            {
                banner.IsDeleted = true;
                banner.UpdatedDate = DateTime.Now;
                _context.Banners.Update(banner);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Banner>> GetAllBannerAsync()
        {
            var banners = await _context.Banners
                    .Include(x=>x.Category)
                    .Where(x => x.IsActive == true && x.IsDeleted == false)
                    .ToListAsync();
            return banners ?? new List<Banner>();
        }


        public void InsertOrUpdateBanner(BannerDTO bannerDTO)
        {
            if (bannerDTO.Id > 0)
            {
                var banner = _context.Banners.FirstOrDefault(x => x.Id == bannerDTO.Id);
                if (banner == null)
                {
                    throw new Exception("banner is null");
                }
                else
                {
                    banner.BannerAlt = bannerDTO.BannerAlt;
                    banner.ImageURL = bannerDTO.ImageURL;
                    banner.UpdatedDate = DateTime.Now;
                    banner.UpdatedBy = bannerDTO.UpdatedBy;
                    banner.CategoryId = bannerDTO?.CategoryId;
                    banner.IsActive = bannerDTO.IsActive;
                    banner.IsDeleted = bannerDTO.IsDeleted;
                    _context.Banners.Update(banner);
                }
            }
            else
            {
                var newBanner = new Banner
                {
                    IsDeleted = false,
                    IsActive = bannerDTO.IsActive,
                    CategoryId = bannerDTO?.CategoryId,
                    ImageURL = bannerDTO.ImageURL,
                    BannerAlt = bannerDTO.BannerAlt,
                    CreatedBy = bannerDTO.CreatedBy,
                    CreatedDate = DateTime.Now
                };
                _context.Banners.Add(newBanner);
            }
            _context.SaveChanges();
        }
    }
}
