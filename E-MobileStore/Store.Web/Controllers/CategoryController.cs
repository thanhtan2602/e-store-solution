using Microsoft.AspNetCore.Mvc;
using Store.Web.ViewsModel;
using Store.WebService.Services;
using Store.WebService.Services.Interfaces;

namespace Store.Web.Controllers
{
    public class CategoryController : Controller
    {
		private readonly ILogger<CategoryController> _logger;
		private readonly IBannerWebService _bannerWebService;
        private readonly IProductWebService _productWebService;

        public CategoryController(ILogger<CategoryController> logger ,IBannerWebService bannerWebService, IProductWebService productWebService)
        {
			_logger= logger;
			_bannerWebService = bannerWebService;
            _productWebService = productWebService;

        }
        [Route("{categoryUrl}")]
        public async Task<IActionResult> Index(string categoryUrl, string? sortBy= "date_desc", int pageSize = 6)
        {
            _logger.LogInformation("This is category page");
            var banner = await _bannerWebService.GetBannerByCate(1, 100, categoryUrl);
            var listProduct = await _productWebService.GetProductListByCateUrl(categoryUrl, 1, pageSize, sortBy);
            int totalProduct = await _productWebService.TotalProductByCate(categoryUrl);
            ViewBag.TotalProduct = totalProduct;
            ViewBag.CategoryName = listProduct.FirstOrDefault()?.CategoryName;
            var productByCate = new ProductsByCateVM
            {
                Products = listProduct,
                Banners = banner,
            };
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("/Views/Category/ProductList.cshtml", productByCate.Products);
            }
            return View(productByCate);
        }

        public IActionResult Banner()
        {
            return PartialView("/Views/Category/BannerCate.cshtml");
        }
        public IActionResult ListProduct()
        {
            return PartialView("/Views/Category/ProductList.cshtml");
        }
    }
}
