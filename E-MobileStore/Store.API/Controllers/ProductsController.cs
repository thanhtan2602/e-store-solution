using Microsoft.AspNetCore.Mvc;
using Store.ApiService.Services.Interfaces;
using Store.Infrastructure.DTOs;
using System.Net;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private BaseApiResponse _response;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
            _response = new BaseApiResponse();
        }

        [HttpGet]
        [Route("GetSaleProducts")]
        public async Task<IActionResult> GetSaleProductsAsync(int flashSaleId)
        {
            try
            {
                var products = await _productService.GetSaleProducts(flashSaleId);
                _response.Success(products);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var statuscode = _response.StatusCode = HttpStatusCode.NotFound;
                var message = _response.ErrorMessages = new List<string> { ex.Message };

                _response.Failed(statuscode, message);
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }
        [HttpGet]
        [Route("GetProductById")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            try
            {
                var product = await _productService.GetProductById(productId);
                _response.Success(product);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var statuscode = _response.StatusCode = HttpStatusCode.NotFound;
                var message = _response.ErrorMessages = new List<string> { ex.Message };
                _response.Failed(statuscode, message);
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }
        [HttpPost]
        [Route("InsertOrUpdateProduct")]
        public IActionResult AddProduct(ProductDTO product)
        {
            try
            {
                _productService.AddOrUpdateProduct(product);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var statuscode = _response.StatusCode = HttpStatusCode.BadRequest;
                var message = _response.ErrorMessages = new List<string> { ex.Message };
                _response.Failed(statuscode, message);
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }
        [HttpPut]
        [Route("DeleteProduct")]
        public IActionResult DeleteProduct(Guid productId)
        {
            try
            {
                _productService.DeleteProduct(productId);
                _response.IsSuccess = true;
                _response.Message = "Product has been deleted";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var statuscode = _response.StatusCode = HttpStatusCode.NotFound;
                var message = _response.ErrorMessages = new List<string> { ex.Message };
                _response.Failed(statuscode, message);
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }
        [HttpGet]
        [Route("GetProductListByCateId")]
        public async Task<IActionResult> GetProductListByCate(int cateId, int page, int pageSize, string? sortBy)
        {
            var products = await _productService.GetProductListByCateId(cateId, page, pageSize, sortBy);
            if (products == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
            }
            _response.Result = products;
            return Ok(_response);
        }

        [HttpGet]
        [Route("TotalProduct")]
        public async Task<IActionResult> TotalProductAsync(int cateId)
        {
            var products = await _productService.TotalProductAsync(cateId);
            if (products == 0)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
            }
            _response.Result = products;
            return Ok(_response);
        }

        [HttpGet]
        [Route("GetProductSearch")]
        public async Task<IActionResult> GetProductSearch(string search, int page, int pageSize)
        {
            var products = await _productService.GetProductSearchAsync(search, page, pageSize);
            if (products == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
            }
            _response.Result = products;
            return Ok(_response);
        }
    }
}
