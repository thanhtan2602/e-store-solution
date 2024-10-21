using Microsoft.AspNetCore.Mvc;
using Store.WebService.DTO;
using Store.WebService.Services;
using Store.WebService.Services.Interfaces;

namespace Store.Web.Areas.Admin.Controllers

{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        private readonly ICategoryWebService categoryWebService;
        private readonly IWebHostEnvironment env;

        public CategoryController(IWebHostEnvironment env, ICategoryWebService categoryWebService)
        {
            this.categoryWebService = categoryWebService;
            this.env = env;
        }
        private void UploadImage(string ImageURL)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), env.WebRootPath, "uploads/images", ImageURL);
            if (!System.IO.File.Exists(path))
            {
                using var stream = new FileStream(path, FileMode.Create);
            }
        }
        [Route("quan-li-nganh-hang")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var jwt = HttpContext.Session.GetString("jwtadmin");
            if (jwt == null)
            {
                return RedirectToRoute("login");
            }
            var categoryList = await categoryWebService.GetAllCategory(page, pageSize);
            ViewBag.TotalCategory = categoryList.Count;
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(categoryList);
            }
            return View(categoryList);
        }
        [Route("quan-li-nganh-hang/them-nganh-hang")]
        public async Task<IActionResult> CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        [Route("quan-li-nganh-hang/them-nganh-hang")]
        public async Task<IActionResult> CategoryAdd(CategoryDTO categoryDTO)
        {
            var result = await categoryWebService.InsertOrUpdateCategory(categoryDTO);
            if (result == "200")
            {
                if (!string.IsNullOrEmpty(categoryDTO.ImageURL))
                {
                    UploadImage(categoryDTO.ImageURL);
                }
                return RedirectToAction("Index");
            }
            ViewBag.ErrorAddCate = result;
            return View();
        }
        [Route("quan-li-nganh-hang/cap-nhat-nganh-hang")]
        public async Task<IActionResult> CategoryEdit(string categoryUrl)
        {
            ViewBag.CategoryInfo = await categoryWebService.GetCategoryByURL(categoryUrl);
            return View();
        }
        [HttpPost]
        [Route("quan-li-nganh-hang/cap-nhat-nganh-hang")]
        public async Task<IActionResult> CategoryEdit(CategoryDTO categoryDTO)
        {
            var result = await categoryWebService.InsertOrUpdateCategory(categoryDTO);
            if (result == "200")
            {
                if (!string.IsNullOrEmpty(categoryDTO.ImageURL))
                {
                    UploadImage(categoryDTO.ImageURL);
                }
                return RedirectToAction("Index");
            }
            ViewBag.ErrorEditCate = result;
            return View("Index");
        }
        [HttpPut]
        [Route("quan-li-nganh-hang/xoa-nganh-hang")]
        public async Task<IActionResult> CategoryDelete(string categoryUrl)
        {
            var result = await categoryWebService.DeleteCategory(categoryUrl);
            ViewBag.DeletedMessage = result;
            return Json(new { success = true });
        }
        [Route("quan-li-nganh-hang/{categoryUrl}")]
        public async Task<IActionResult> CategoryDetail(string categoryUrl)
        {
            var result = await categoryWebService.GetCategoryByURL(categoryUrl);
            return View(result);
        }
    }
}
