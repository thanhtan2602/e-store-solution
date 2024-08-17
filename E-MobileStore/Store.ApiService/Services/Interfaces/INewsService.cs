using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ApiService.Services.Interfaces
{
    public interface INewsService
    {
        Task<List<New>> GetNewsAsync();
        Task<New> GetNewByIdAsync(int newId);
        void InsertOrUpdateNew(NewDTO newDTO);
        void DeleteNew(int id);
    }
}
