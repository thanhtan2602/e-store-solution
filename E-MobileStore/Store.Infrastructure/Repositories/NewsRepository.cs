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
        public void DeleteNew(int id)
        {
            var _new = _context.News.FirstOrDefault(x => x.Id == id);
            if (_new != null)
            {
                _new.IsDeleted = true;
                _new.UpdatedDate = DateTime.Now;
                _context.News.Update(_new);
                _context.SaveChanges();
            }
            throw new Exception("New not found");
        }

        public async Task<New> GetNewByIdAsync(int newId)
        {
            var _new = await _context.News
                .Include(x => x.Category)
                .Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Id == newId);
            if (_new != null)
            {
                return _new;
            }
            else
            {
                throw new Exception("New not found");
            }
        }

        public async Task<List<New>> GetNewsAsync()
        {
            var _news = await _context.News
                .Include(x => x.Category)
                .Where(x => !x.IsDeleted).ToListAsync();
            return _news != null ? _news : new List<New>();
        }

        public void InsertOrUpdateNew(NewDTO newDTO)
        {
            if (newDTO.Id > 0)
            {
                var updateNew = _context.News.FirstOrDefault(x => x.Id == newDTO.Id);
                if (updateNew != null)
                {
                    updateNew.Title = newDTO.Title;
                    updateNew.Description = newDTO.Description;
                    updateNew.ShortDesc = newDTO.ShortDesc;
                    updateNew.ImageURL = newDTO.ImageURL;
                    updateNew.UpdatedDate = newDTO.UpdatedDate;
                    updateNew.UpdatedBy = newDTO.UpdatedBy;
                    updateNew.CategoryId = newDTO.CategoryId;
                    updateNew.IsActive = newDTO.IsActive;
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
                    Title = newDTO.Title,
                    Description = newDTO.Description,
                    ShortDesc = newDTO.ShortDesc,
                    ImageURL = newDTO.ImageURL,
                    CategoryId = newDTO.CategoryId,
                    CreatedBy = newDTO.CreatedBy,
                    CreatedDate = newDTO.CreatedDate,
                    IsActive = newDTO.IsActive,
                    IsDeleted = false,
                };
                _context.News.Add(insertNew);
            }
            _context.SaveChanges();
        }
    }
}
