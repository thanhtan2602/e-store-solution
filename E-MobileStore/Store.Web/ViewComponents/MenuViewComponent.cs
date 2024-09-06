using Microsoft.AspNetCore.Mvc;
using Store.WebService.Services.Interfaces;

namespace Store.Web.ViewComponents
{
	public class MenuViewComponent : ViewComponent
	{
		private readonly ICategoryWebService _categoryWebService;

		public MenuViewComponent(ICategoryWebService categoryWebService)
		{
			_categoryWebService = categoryWebService;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var menu = await _categoryWebService.GetAllCategory(1, 6);
			return View(menu);
		}
	}
}
