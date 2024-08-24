using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ILogger<HomeController> logger, IStoreWebService storeWebService, ICategoryWebService categoryWebService, IBannerWebService bannerWebService, INewsWebService newsWebService, IFlashSaleWebService flashSaleWebService)
        {
            _logger = logger;
            _categoryWebService = categoryWebService;
            _bannerWebService = bannerWebService;
            _newsWebService = newsWebService;
            _flashSaleWebService = flashSaleWebService;
            _storeWebService = storeWebService;
        }
        public async Task<IActionResult> Index()
        {
            var catelist = await _categoryWebService.GetAllCategory(1, 6);
            var bannerHome = await _bannerWebService.GetAllBanner(1, 7);
            var tekZone = await _newsWebService.GetAllNews(1, 6);
            var flashSale = await _flashSaleWebService.GetFlashSale(1, 2);
            var storeList = await _storeWebService.GetStoreList(1, 10);
            var result = new HomeVM
            {
                ChosseCate = catelist,
                ProductByCate = catelist,
                HomeSlider = bannerHome,
                TekZone = tekZone,
                FlashSale = flashSale,
                Stores = storeList
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
    }
}
