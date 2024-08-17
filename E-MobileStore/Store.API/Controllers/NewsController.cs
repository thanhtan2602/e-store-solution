using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.ApiService.Services;
using Store.ApiService.Services.Interfaces;
using Store.Infrastructure.DTOs;
using System.Net;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newService;
        private readonly BaseApiResponse _response;

        public NewsController(INewsService newsService)
        {
            _newService = newsService;
            _response = new BaseApiResponse();
        }
        [HttpGet]
        [Route("GetAllNews")]
        public async Task<IActionResult> GetAllNews()
        {
            try
            {
                var listNews = await _newService.GetNewsAsync();
                _response.IsSuccess = true;
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
                var _new = await _newService.GetNewByIdAsync(newId);
                _response.IsSuccess = true;
                _response.Result = _new;
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
        public IActionResult InsertOrUpdateNews(NewDTO News)
        {

            try
            {
                _newService.InsertOrUpdateNew(News);
                _response.IsSuccess = true;
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
        public IActionResult DeleteNews(int NewsId)
        {
            try
            {
                _newService.DeleteNew(NewsId);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
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
