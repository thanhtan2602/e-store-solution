using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Interfaces
{
    public interface INewsRepository
    {
        Task<List<New>> GetNewsAsync(int page, int pageSize);
        Task<New> GetNewsByIdAsync(int newsId);
        void InsertOrUpdateNews(NewsDTO newsDTO);
        void DeleteNews(int newsId);
    }
}
