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
            _newRepository=newsRepository;
        }
        public void DeleteNew(int id)
        {
            _newRepository.DeleteNew(id);
        }

        public async Task<New> GetNewByIdAsync(int newId)
        {
            return await _newRepository.GetNewByIdAsync(newId);
        }

        public async Task<List<New>> GetNewsAsync()
        {
            return await _newRepository.GetNewsAsync();
        }

        public void InsertOrUpdateNew(NewDTO newDTO)
        {
            _newRepository.InsertOrUpdateNew(newDTO);
        }
    }
}
