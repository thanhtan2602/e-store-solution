using Microsoft.AspNetCore.Mvc;
using Store.Web.ViewsModel;
using Store.WebService.Services.Interfaces;

namespace Store.Web.Controllers
{
    public class DetailProductController : Controller
    {
        private readonly IProductWebService _productWebService;

        public DetailProductController(IProductWebService productWebService)
        {
            _productWebService = productWebService;

        }
        public async Task<IActionResult> Index(Guid productId)
        {
            var product = await _productWebService.GetProductDetail(productId);
            var suggestProduct = await _productWebService.GetProductListByCateId(product.CategoryId, 1, 10);
            var result = new DetailProductVM
            {
                Product = product,
                SuggestProduct = suggestProduct
            };
            return PartialView("/Views/DetailProduct/Index.cshtml", result);
        }
        public IActionResult IntroDetail()
        {
            return PartialView();
        }
        public IActionResult Description()
        {
            return PartialView();
        }
        public IActionResult SuggestProduct()
        {
            return PartialView();
        }
    }
}
