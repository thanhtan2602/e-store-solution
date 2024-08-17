using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.ApiService.Services;
using Store.ApiService.Services.Interfaces;
using Store.Infrastructure.DTOs;
using System.Net;
using System.Reflection.Metadata;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly BaseApiResponse _response;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
            _response = new BaseApiResponse();
        }
        [HttpGet]
        [Route("GetAllNews")]
        public async Task<IActionResult> GetAllNews(int page, int pageSize)
        {
            try
            {
                var listNews = await _newsService.GetNewsAsync(page,pageSize);
                _response.Result = listNews;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var statuscode = _response.StatusCode = HttpStatusCode.BadRequest;
                var errorMessenger = _response.ErrorMessages = new List<string> { ex.Message };
                _response.IsSuccess = false;
                _response.Failed(statuscode, errorMessenger);
                return BadRequest(_response);
            }
        }
        [HttpGet]
        [Route("GetNewsById")]
        public async Task<IActionResult> GetNewsById(int newId)
        {
            try
            {
                var news = await _newsService.GetNewsByIdAsync(newId);
                _response.Result = news;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var statuscode = _response.StatusCode = HttpStatusCode.BadRequest;
                var errorMessenger = _response.ErrorMessages = new List<string> { ex.Message };
                _response.IsSuccess = false;
                _response.Failed(statuscode, errorMessenger);
                return BadRequest(_response);
            }
        }
        [HttpPost]
        [Route("InsertOrUpdateNews")]
        public IActionResult InsertOrUpdateNews(NewsDTO News)
        {
            try
            {
                _newsService.InsertOrUpdateNews(News);
                _response.Result = News;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var statuscode = _response.StatusCode = HttpStatusCode.BadRequest;
                var errorMessenger = _response.ErrorMessages = new List<string> { ex.Message };
                _response.IsSuccess = false;
                _response.Failed(statuscode, errorMessenger);
                return BadRequest(_response);
            }
        }
        [HttpPut]
        [Route("DeleteNews")]
        public IActionResult DeleteNews(int newsId)
        {
            try
            {
                _newsService.DeleteNews(newsId);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "This News has been deleted";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var statuscode = _response.StatusCode = HttpStatusCode.BadRequest;
                var errorMessenger = _response.ErrorMessages = new List<string> { ex.Message };
                _response.IsSuccess = false;
                _response.Failed(statuscode, errorMessenger);
                return BadRequest(_response);
            }
        }
    }
}
