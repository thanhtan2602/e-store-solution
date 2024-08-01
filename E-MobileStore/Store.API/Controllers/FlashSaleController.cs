using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.ApiService.Services.Interfaces;
using Store.Infrastructure.DTOs;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashSaleController : ControllerBase
    {
        private readonly IFlashSaleService _flashSale;
        private BaseApiResponse _response;
        public FlashSaleController(IFlashSaleService flashSale)
        {
            _flashSale = flashSale;
        }
        [HttpPost]
        [Route("InsertFlashSale")]
        public IActionResult AddFlashSale(FlashSaleDTO flashSale)
        {
            _flashSale.AddFlashSaleAsync(flashSale);
            return Ok(_response);
        }
    }
}
