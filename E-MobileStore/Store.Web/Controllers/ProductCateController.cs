using Microsoft.AspNetCore.Mvc;
using Store.Web.ViewsModel;
using Store.WebService.Services;
using Store.WebService.Services.Interfaces;

namespace Store.Web.Controllers
{
    public class ProductCateController : Controller
    {
        private readonly IBannerWebService _bannerWebService;
        private readonly IProductWebService _productWebService;

        public ProductCateController(IBannerWebService bannerWebService, IProductWebService productWebService)
        {
            _bannerWebService = bannerWebService;
            _productWebService = productWebService;

        }
        public async Task<IActionResult> Index(int categoryId, string? sortBy, int pageSize = 6)
        {
            var banner = await _bannerWebService.GetBannerByCate(1, 100, categoryId);
            var listProduct = await _productWebService.GetProductListByCateId(categoryId, 1, pageSize, sortBy);
            int totalProduct = await _productWebService.TotalProduct(categoryId);
            ViewBag.TotalProduct = totalProduct;
            var productByCate = new ProductsByCateVM
            {
                Products = listProduct,
                Banners = banner,
            };

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // Nếu là yêu cầu AJAX, trả về chỉ danh sách sản phẩm
                return PartialView("/Views/ProductCate/ListProduct.cshtml", productByCate.Products);
            }
            // Nếu không phải yêu cầu AJAX, trả về trang đầy đủ
            return View(productByCate);
        }

        public IActionResult Banner()
        {
            return View();
        }
        public IActionResult ListProduct()
        {
            return View();
        }
    }
}
