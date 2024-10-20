using Microsoft.AspNetCore.Mvc;
using Store.WebService.DTO;
using Store.WebService.Services;
using Store.WebService.Services.Interfaces;
using static Store.Web.Utility.ProductsUtilities;
namespace Store.Web.Areas.Admin.Controllers

{
    [Area("Admin")]

    public class ProductController : Controller
    {
        private readonly IProductWebService _productWebService;
        private readonly ICategoryWebService _categoryWebService;
        private readonly IWebHostEnvironment _env;


        public ProductController(IWebHostEnvironment env, IProductWebService productWebService, ICategoryWebService categoryWebService)
        {
            _productWebService = productWebService;
            _categoryWebService = categoryWebService;
            _env = env;

        }
		[Route("quan-li-san-pham/tim-kiem")]
		public async Task<IActionResult> ProductSearchResults(string search)
		{
			var searchResult = await _productWebService.GetProductSearch(search, 1, 10);
			return Json(searchResult);
		}
		[Route("quan-li-san-pham")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string? sortBy = "date_desc")
        {
            var jwt = HttpContext.Session.GetString("jwtadmin");
            if (jwt == null)
            {
                return RedirectToRoute("login");
            }
            var productList = await _productWebService.GetProductList(page, pageSize, sortBy);
            ViewBag.TotalItem = await _productWebService.TotalProduct();
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(productList);
            }
            return View(productList);
        }
        [Route("quan-li-san-pham/them-san-pham")]
        public async Task<IActionResult> ProductAdd()
        {
            ViewBag.CategoryList = await _categoryWebService.GetAllCategory(1, 20);
            return View();
        }
        private void UploadImage(string ImageURL)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), _env.WebRootPath, "uploads/images", ImageURL);
            if (!System.IO.File.Exists(path))
            {
                using var stream = new FileStream(path, FileMode.Create);
            }
        }
        [HttpPost]
        [Route("quan-li-san-pham/them-san-pham")]
        public async Task<IActionResult> ProductAdd(ProductDTO productDTO, List<ProductImageDTO> Images)
        {
            var result = await _productWebService.InserOrUpdateProduct(productDTO, Images);
            if (result == "200")
            {

                for (int i = 0; i < Images.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(Images[i].ImageURL))
                    {
                        UploadImage(Images[i].ImageURL);
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorCode = result;
                return View();
            }
        }


        [Route("quan-li-san-pham/cap-nhat-san-pham")]
        public async Task<IActionResult> ProductEdit(string productUrl)
        {
            ViewBag.ProductInfo = await _productWebService.GetProductDetail(productUrl);
            ViewBag.CategoryList = await _categoryWebService.GetAllCategory(1, 20);
            return View();
        }
        [HttpPost]
        [Route("quan-li-san-pham/cap-nhat-san-pham")]
        public async Task<IActionResult> ProductEdit(ProductDTO productDTO, List<ProductImageDTO> Images)
        {
            var result = await _productWebService.InserOrUpdateProduct(productDTO, Images);
            if (result == "200")
            {
                for (int i = 0; i < Images.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(Images[i].ImageURL))
                    {
                        UploadImage(Images[i].ImageURL);
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorCode = result;
                return View();
            }
        }
        [Route("quan-li-san-pham/{productUrl}")]
        public async Task<IActionResult> ProductDetail(string productUrl)
        {
            var result = await _productWebService.GetProductDetail(productUrl);
            return View("~/Areas/Admin/Views/Product/ProductDetail.cshtml", result);
        }
        [HttpPut]
        [Route("quan-li-san-pham/xoa-san-pham")]
        public async Task<IActionResult> ProductDelete(string productUrl)
        {
            var result = await _productWebService.DeleteProduct(productUrl);
            ViewBag.DeletedMessage = result;
            return Json(new { success = true });
        }
    }
}
