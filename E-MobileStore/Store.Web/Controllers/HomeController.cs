using Microsoft.AspNetCore.Mvc;
using Store.Domain.Entities;
using Store.Web.ViewsModel;
using Store.WebService.Services;
using Store.WebService.Services.Interfaces;
using Store.WebService.ViewModels;
using System.Diagnostics;

namespace Store.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryWebService _categoryWebService;
        private readonly IBannerWebService _bannerWebService;
        private readonly INewsWebService _newsWebService;
        private readonly IFlashSaleWebService _flashSaleWebService;
        private readonly IStoreWebService _storeWebService;
        private readonly IProductWebService _productWebService;

        public HomeController(ILogger<HomeController> logger, IProductWebService productWebService, IStoreWebService storeWebService, ICategoryWebService categoryWebService, IBannerWebService bannerWebService, INewsWebService newsWebService, IFlashSaleWebService flashSaleWebService)
        {
            _logger = logger;
            _categoryWebService = categoryWebService;
            _bannerWebService = bannerWebService;
            _newsWebService = newsWebService;
            _flashSaleWebService = flashSaleWebService;
            _storeWebService = storeWebService;
            _productWebService = productWebService;

        }
        [Route("/Home/ProductSearchResults")]
        public async Task<IActionResult> ProductSearchResults(string search)
        {
            var searchResult = await _productWebService.GetProductSearch(search, 1, 10);
            return Json(searchResult);
        }
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("This is home page");
            var catelist = await _categoryWebService.GetAllCategory(1, 6);
            var bannerHome = await _bannerWebService.GetBannerByCate(1, 100, "home");
            var tekZone = await _newsWebService.GetAllNews(1, 6);
            var flashSale = await _flashSaleWebService.GetFlashSale(1, 2);
            var storeList = await _storeWebService.GetStoreList(1, 10);
            var jwt = TempData["jwt"];
            ViewBag.jwt = jwt;
            var result = new HomeVM
            {
                ChosseCate = catelist,
                ProductByCate = catelist,
                HomeSlider = bannerHome,
                TekZone = tekZone,
                FlashSale = flashSale,
                Stores = storeList,
            };

            return PartialView("/Views/Home/Index.cshtml", result);
        }
        public IActionResult ProductByCate()
        {
            return View();
        }
        public IActionResult ChosseCate()
        {
            return View();
        }
        public IActionResult TekZone()
        {
            return View();
        }
        public IActionResult FlashSale()
        {
            return View();
        }
        [Route("trang-chu/{flashsaleId?}")]
        public async Task<JsonResult> FlashSaleItem(int flashsaleId)
        {
            var productlist = await _productWebService.GetProductBySaleId(flashsaleId);
            var result = new HomeVM
            {
                Products = productlist,
                Count = productlist.Count()
            };
            return Json(result);
        }
        public IActionResult HomeSlider()
        {
            return View();
        }
        public IActionResult ListBranch()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult PageNotFound()
        {
            _logger.LogWarning("dont have page");
            return View();
        }
    }
}
