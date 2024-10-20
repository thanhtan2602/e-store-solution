using Microsoft.AspNetCore.Mvc;
using Store.Web.ViewsModel;
using Store.WebService.Services.Interfaces;

namespace Store.Web.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly IProductWebService _productWebService;

        public ProductDetailController(IProductWebService productWebService)
        {
            _productWebService = productWebService;

        }
        [Route("{categoryUrl}/{productUrl}")]
        public async Task<IActionResult> Index(string productUrl,string categoryUrl, string? sortBy)
        {
            var product = await _productWebService.GetProductDetail(productUrl);
            ViewBag.productName=product.ProductName;
            var suggestProduct = await _productWebService.GetProductListByCateUrl(categoryUrl, 1, 10, sortBy);
            var result = new DetailProductVM
            {
                Product = product,
                SuggestProduct = suggestProduct
            };
            return PartialView("/Views/ProductDetail/Index.cshtml", result);
        }
        public IActionResult IntroDetail()
        {
            return PartialView("/Views/ProductDetail/IntroDetail.cshtml");
        }
        public IActionResult Description()
        {
            return PartialView("/Views/ProductDetail/Description.cshtml");
        }
        public IActionResult SuggestProduct()
        {
            return PartialView("/Views/ProductDetail/SuggestProduct.cshtml");
        }
    }
}
