using Microsoft.AspNetCore.Mvc;

namespace Store.Web.Controllers
{
    public class ProductCateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Banner()
        {
            return View();
        }
    }
}
