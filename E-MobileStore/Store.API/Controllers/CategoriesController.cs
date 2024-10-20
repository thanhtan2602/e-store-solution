using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Store.ApiService.Services.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using System.Net;
using static Store.Common.Utility.ProductsUtility;
namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private BaseApiResponse _response;
        private readonly ICategoryService _category;
        private readonly IDistributedCache _distributedCache;
        public CategoriesController(ICategoryService category, IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _category = category;
            _response = new BaseApiResponse();
        }
        [HttpGet]
        [Route("GetAllCategory")]
        public async Task<IActionResult> GetAllCategoriesAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            try
            {
                var key = $"cate:page={page}";
                string? catchMember = await _distributedCache.GetStringAsync(key, cancellationToken);
                IEnumerable<Category>? listCate;
                if (string.IsNullOrEmpty(catchMember))
                {
                    listCate = await _category.GetCategoriesAsync(page, pageSize);
                    if (listCate == null)
                    {
                        return NotFound();
                    }
                    string jsonList = JsonConvert.SerializeObject(listCate, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    await _distributedCache.SetStringAsync(key, jsonList, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) }, cancellationToken); ;
                    _response.Result = listCate;
                }
                else
                {
                    listCate = JsonConvert.DeserializeObject<IEnumerable<Category>>(catchMember);
                    _response.Result = listCate;
                }
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
        [Route("GetCategoryByUrl")]
        public async Task<IActionResult> GetCategoryByIdAsync(string categoryUrl, CancellationToken cancellationToken = default)
        {
            try
            {
                var key = $"cate={categoryUrl}";
                string? catchMember = await _distributedCache.GetStringAsync(key, cancellationToken);
                Category? cate;
                if (string.IsNullOrEmpty(catchMember))
                {
                    cate = await _category.GetByUrl(categoryUrl);
                    if (cate == null)
                    {
                        return NotFound();
                    }
                    string json = JsonConvert.SerializeObject(cate, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    await _distributedCache.SetStringAsync(key, json, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) }, cancellationToken);
                    _response.Result = cate;
                }
                else
                {
                    cate = JsonConvert.DeserializeObject<Category>(catchMember);
                    _response.Result = cate;
                }
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
        [Route("InsertOrUpdateCategory")]
        public async Task<IActionResult> InsertOrUpdateCategory(CategoryDTO category, CancellationToken cancellationToken = default)
        {
            try
            {
                _category.AddOrUpdateCategory(category);
                var key = $"cate={ToUrl(category.Name)}";
                await _distributedCache.RemoveAsync(key, cancellationToken);
                string catepage;
                for (int i = 0; i < 10; i++)
                {
                    catepage= $"cate:page={i}";
                    await _distributedCache.RemoveAsync(catepage, cancellationToken);
                }
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var statuscode = _response.StatusCode = HttpStatusCode.BadRequest;
                var errorMessenger = _response.ErrorMessages = new List<string> { ex.Message };
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _response.Failed(statuscode, errorMessenger);
                return BadRequest(_response);
            }
        }
        [HttpPut]
        [Route("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(string categoryUrl, CancellationToken cancellationToken = default)
        {
            try
            {
                _category.DeleteCategory(categoryUrl);
                var key = $"cate={categoryUrl}";
                await _distributedCache.RemoveAsync(key, cancellationToken);
                string catepage;
                for (int i = 0; i < 10; i++)
                {
                    catepage = $"cate:page={i}";
                    await _distributedCache.RemoveAsync(catepage, cancellationToken);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "200";
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
