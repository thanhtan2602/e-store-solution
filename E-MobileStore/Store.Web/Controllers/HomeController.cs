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

        public IActionResult Index()
        {
            var response = _productWebService.GetProductDetail(1);
            return View("/Views/Common/ProductBox.cshtml");
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
