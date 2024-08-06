using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.ApiService.Services.Interfaces;
using Store.Infrastructure.DTOs;
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
        }

        [HttpGet]
        [Route("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory(int page = 1, int pageSize = 2)
        {
            var listCate = _category.GetAllCategoriesAsync(page, pageSize);
            var result = JsonSerializer.Serialize(listCate);
            return Content(result, "application/json");
        }

        [HttpGet]
        [Route("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var cate = _category.GetByIdAsync(categoryId);
            var result = JsonSerializer.Serialize(cate);
            return Content(result, "application/json");
        }

        [HttpPost]
        [Route("InsertCategory")]
        public IActionResult InsertCategory(CategoryDTO category)
        {
            _category.AddCategoriesAsync(category);
            return Ok(_response);
        }

        [HttpPut]
        [Route("ManageCategory")]
        public IActionResult ManageCategory(CategoryDTO category, int categoryId, int action)
        {
            _category.ManageCategoriesAsync(category, categoryId, action);
            return Ok(_response);
        }

        [HttpDelete]
        [Route("ParmmanentlyCategory")]
        public IActionResult ParmanentlyCategory(int categoryId)
        {
            _category.ParmanentlyCategoriesAsync(categoryId);
            return Ok(_response);
        }
    }
}
