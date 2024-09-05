using Microsoft.AspNetCore.Mvc;
using Store.WebService.Services;
using Store.WebService.Services.Interfaces;

namespace Store.Web.ViewComponents
{
	public class SearchViewComponent : ViewComponent
	{
		private readonly IProductWebService _productWebService;

		public SearchViewComponent(IProductWebService productWebService)
		{
			_productWebService = productWebService;
		}
		public async Task<IViewComponentResult> InvokeAsync(string search)
		{
			var searchResult = await _productWebService.GetProductSearch(search,1,10);
			return View(searchResult);
		}
	}
}
