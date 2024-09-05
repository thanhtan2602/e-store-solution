using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.WebService.Services;
using Store.WebService.Services.Interfaces;

namespace Store.Web.Controllers
{
    public class CommonController : Controller
    {
        private readonly IProductWebService _productWebService;
        private readonly ICategoryWebService _categoryWebService;

        public CommonController(ICategoryWebService categoryWebService, IProductWebService productWebService)
        {
            _productWebService = productWebService;
            _categoryWebService = categoryWebService;
        }

        public async Task<IActionResult> ProductSearchResults(string search)
        {
            var searchResult = await _productWebService.GetProductSearch(search, 1, 10);
            return Json(searchResult);
        }
    }
}
