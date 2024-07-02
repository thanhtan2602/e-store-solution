using Microsoft.AspNetCore.Mvc;

namespace Store.Web.Controllers
{
    public class DetailProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Description()
        {
            return PartialView();
        }
    }
}
