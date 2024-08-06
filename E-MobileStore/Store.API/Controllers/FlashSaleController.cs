using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.ApiService.Services.Interfaces;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.ViewModels;
using System.Text.Json;

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

        [HttpPut]
        [Route("ManageFlashSale")]
        public IActionResult ManageFlashSale(int flashSaleId, FlashSaleDTO flashSaleDTO, int action)
        {
            _flashSale.ManageFlashSaleAsync(flashSaleId, flashSaleDTO, action);
            return Ok(_response);
        }
        [HttpPost]
        [Route("AddFlashSaleProduct")]
        public IActionResult AddFlashSaleProduct(List<FlashSaleProductDTO> flashSaleProductDTO, int flashSaleId)
        {
            _flashSale.AddFlashSaleProductAsync(flashSaleProductDTO, flashSaleId);
            return Ok(_response);
        }

        [HttpPut]
        [Route("ManageFlashSaleProduct")]
        public IActionResult ManageFlashSaleProduct(FlashSaleProductDTO flashSaleProductDTO, int flashSaleId, Guid productId, int action)
        {
            _flashSale.ManageFlashSaleProductAsync(flashSaleProductDTO, flashSaleId, productId, action);
            return Ok(_response);
        }

        [HttpGet]
        [Route("GetListFlashSale")]
        public async Task<IActionResult> GetFlashSale(int page = 1, int pageSize = 2)
        {
            var list = await _flashSale.GetAllAsync(page, pageSize);
            var result = JsonSerializer.Serialize(list);
            return Content(result, "application/json");
        }
        [HttpDelete]
        [Route("ParanentlyDeleted")]
        public IActionResult ParmanentlyDeleted(int flashSaleId)
        {
            _flashSale.PermanentlyDeletedAsync(flashSaleId);
            return Ok(_response);
        }
    }
}
