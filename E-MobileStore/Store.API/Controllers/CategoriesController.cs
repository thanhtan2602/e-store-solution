using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.ApiService.Services.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using System.Net;
using System.Text.Json;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private BaseApiResponse _response;
        private readonly ICategoryService _category;
        public CategoriesController(ICategoryService category)
        {
            _category = category;
            _response = new BaseApiResponse();
        }
        [HttpGet]
        [Route("GetAllCategory")]
        public async Task<IActionResult> GetAllCategoriesAsync(int page, int pageSize)
        {
            try
            {
                var listCate = await _category.GetCategoriesAsync(page, pageSize);
                _response.Result = listCate;
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
        [Route("GetCategoryById")]
        public async Task<IActionResult> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                var cate = await _category.GetById(categoryId);
                _response.Result = cate;
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
        public IActionResult InsertOrUpdateCategory(CategoryDTO category)
        {
            try
            {
                _category.AddOrUpdateCategory(category);
                _response.Result = category;
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
        [Route("DeleteCategory")]
        public IActionResult DeleteCategory(int categoryId)
        {
            try
            {
                _category.DeleteCategory(categoryId);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "This category has been deleted";
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
