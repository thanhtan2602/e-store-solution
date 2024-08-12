using Microsoft.AspNetCore.Mvc;
using Store.WebService.Services.Interfaces;
using System.Diagnostics;

namespace Store.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductWebService _productWebService;

        public HomeController(ILogger<HomeController> logger, IProductWebService productWebService)
        {
            _logger = logger;
            _productWebService = productWebService;
        }

        public async Task<IActionResult> Index()
        {
            var test = await _productWebService.GetProductListByCateId(5);    
            return View("/Views/Common/ProductBox.cshtml");
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
       
       
       

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
