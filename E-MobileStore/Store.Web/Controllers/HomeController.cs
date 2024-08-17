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

        public HomeController(ILogger<HomeController> logger, ICategoryWebService categoryWebService , IBannerWebService bannerWebService, INewsWebService newsWebService,IFlashSaleWebService flashSaleWebService)
        {
            _logger = logger;
            _categoryWebService =categoryWebService;
            _bannerWebService=bannerWebService;
            _newsWebService = newsWebService;
            _flashSaleWebService=flashSaleWebService;
        }

        public async Task<IActionResult> Index()
        {
            var catelist = await _categoryWebService.GetAllCategory();
            var bannerHome = await _bannerWebService.GetAllBanner();
            var tekZone = await _newsWebService.GetAllNews();
            var flashSale = await _flashSaleWebService.GetFlashSale();
            var result = new HomeVM
            {
                ChosseCate = catelist,
                ProductByCate = catelist,
                HomeSlider =bannerHome,
                TekZone= tekZone,
                FlashSale=flashSale,
            };
            return PartialView("/Views/Home/Index.cshtml", result);
        }
        public async Task<PartialViewResult> ProductByCate()
        {
            return PartialView("/Views/Home/ProductByCate.cshtml");
        }
        public async Task<PartialViewResult> ChosseCate()
        {
            return  PartialView("/Views/Home/ChosseCate.cshtml");
        }
        public  IActionResult TekZone()
        {
            return View();
        }
        public IActionResult FlashSale()
        {
            return View();
        }
        public IActionResult HomeSlider()
        {
            return PartialView("/Views/Home/HomeSlider.cshtml");
        }

        public IActionResult ListBranch()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
       
       
       

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
