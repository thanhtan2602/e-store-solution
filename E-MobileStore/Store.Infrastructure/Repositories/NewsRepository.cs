using Microsoft.EntityFrameworkCore;
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
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _context;
        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void DeleteNews(int newsId)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == newsId);
            if (news != null)
            {
                news.IsDeleted = true;
                news.UpdatedDate = DateTime.Now;
                _context.News.Update(news);
                _context.SaveChanges();
            }
            throw new Exception("New not found");
        }
        public async Task<New> GetNewsByIdAsync(int newId)
        {
            var news = await _context.News
                .Include(x => x.Category)
                .Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Id == newId);
            if (news != null)
            {
                return news;
            }
            else
            {
                throw new Exception("New not found");
            }
        }
        public async Task<List<New>> GetNewsAsync(int page, int pageSize)
        {
            var news = await _context.News
                .Include(x => x.Category)
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
            return news ?? new List<New>();
        }
        public void InsertOrUpdateNews(NewsDTO newsDTO)
        {
            if (newsDTO.Id > 0)
            {
                var updateNew = _context.News.FirstOrDefault(x => x.Id == newsDTO.Id);
                if (updateNew != null)
                {
                    updateNew.Title = newsDTO.Title;
                    updateNew.Description = newsDTO.Description;
                    updateNew.ShortDesc = newsDTO.ShortDesc;
                    updateNew.ImageURL = newsDTO.ImageURL;
                    updateNew.UpdatedDate = newsDTO.UpdatedDate;
                    updateNew.UpdatedBy = newsDTO.UpdatedBy;
                    updateNew.CategoryId = newsDTO.CategoryId;
                    updateNew.IsActive = newsDTO.IsActive;
                    _context.News.Update(updateNew);
                }
                else
                {
                    throw new Exception("New not found");
                }
            }
            else
            {
                var insertNew = new New()
                {
                    Title = newsDTO.Title,
                    Description = newsDTO.Description,
                    ShortDesc = newsDTO.ShortDesc,
                    ImageURL = newsDTO.ImageURL,
                    CategoryId = newsDTO.CategoryId,
                    CreatedBy = newsDTO.CreatedBy,
                    CreatedDate = newsDTO.CreatedDate,
                    IsActive = newsDTO.IsActive,
                    IsDeleted = false,
                };
                _context.News.Add(insertNew);
            }
            _context.SaveChanges();
        }
    }
}
