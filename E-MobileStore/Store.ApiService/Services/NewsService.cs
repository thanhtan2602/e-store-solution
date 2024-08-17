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
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newRepository;
        public NewsService(INewsRepository newsRepository)
        {
            _newRepository = newsRepository;
        }
        public void DeleteNews(int newsId)
        {
            _newRepository.DeleteNews(newsId);
        }
        public async Task<New> GetNewsByIdAsync(int newsId)
        {
            return await _newRepository.GetNewsByIdAsync(newsId);
        }
        public async Task<List<New>> GetNewsAsync(int page, int pageSize)
        {
            return await _newRepository.GetNewsAsync(page, pageSize);
        }
        public void InsertOrUpdateNews(NewsDTO newsDTO)
        {
            _newRepository.InsertOrUpdateNews(newsDTO);
        }
    }
}
