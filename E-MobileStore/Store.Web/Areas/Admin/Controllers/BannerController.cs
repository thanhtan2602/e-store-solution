using Microsoft.AspNetCore.Mvc;
using Store.WebService.DTO;
using Store.WebService.Services.Interfaces;

namespace Store.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private readonly ICategoryWebService categoryWebService;
        private readonly IBannerWebService bannerWebService;
        private readonly IWebHostEnvironment env;

        public BannerController(IWebHostEnvironment env, IBannerWebService bannerWebService, ICategoryWebService categoryWebService)
        {
            this.categoryWebService = categoryWebService;
            this.bannerWebService = bannerWebService;
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
        [Route("quan-li-banner")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var jwt = HttpContext.Session.GetString("jwtadmin");
            if (jwt == null)
            {
                return RedirectToRoute("login");
            }
            var bannerList = await bannerWebService.GetAllBanner(page, pageSize);
            ViewBag.TotalBanner = bannerList.Count;
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(bannerList);
            }
            return View(bannerList);
        }
        [Route("quan-li-banner/them-banner")]
        public async Task<IActionResult> BannerAdd()
        {
            ViewBag.CategoryList = await categoryWebService.GetAllCategory(1, 100);
            return View();
        }
        [HttpPost]
        [Route("quan-li-banner/them-banner")]
        public async Task<IActionResult> BannerAdd(BannerDTO bannerDTO)
        {
            var result = await bannerWebService.InsertOrUpdateBanner(bannerDTO);
            if (result == "200")
            {
                if (!string.IsNullOrEmpty(bannerDTO.ImageURL))
                {
                    UploadImage(bannerDTO.ImageURL);
                }
                return RedirectToAction("Index");
            }
            ViewBag.ErrorAddCate = result;
            return View();
        }
        [Route("quan-li-banner/cap-nhat-banner")]
        public async Task<IActionResult> BannerEdit(int bannerId)
        {
            ViewBag.CategoryList = await categoryWebService.GetAllCategory(1, 100);
            ViewBag.BannerInfo = await bannerWebService.GetBannerDetail(bannerId);
            return View();
        }
        [HttpPost]
        [Route("quan-li-banner/cap-nhat-banner")]
        public async Task<IActionResult> BannerEdit(BannerDTO bannerDTO)
        {
            var result = await bannerWebService.InsertOrUpdateBanner(bannerDTO);
            if (result == "200")
            {
                if (!string.IsNullOrEmpty(bannerDTO.ImageURL))
                {
                    UploadImage(bannerDTO.ImageURL);
                }
                return RedirectToAction("Index");
            }
            ViewBag.ErrorEditCate = result;
            return View("Index");
        }
        [HttpPut]
        [Route("quan-li-banner/xoa-banner")]
        public async Task<IActionResult> BannerDelete(int bannerId)
        {
            var result = await bannerWebService.DeleteBanner(bannerId);
            ViewBag.DeletedMessage = result;
            return Json(new { success = true });
        }
        [Route("quan-li-banner/{bannerId}")]
        public async Task<IActionResult> BannerDetail(int bannerId)
        {
            var result = await bannerWebService.GetBannerDetail(bannerId);
            return View(result);
        }
    }
}
