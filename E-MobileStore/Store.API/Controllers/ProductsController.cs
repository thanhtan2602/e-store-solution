using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Store.ApiService.Services.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.ViewModels;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        [Route("GetProductList")]
        public async Task<IActionResult> GetProductListAsync(int categoryId, int page = 1, int pageSize = 2)
        {
            try
            {
                var products = await _productService.GetProductList(categoryId, page, pageSize);
                _response.IsSuccess = true;
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
        [Route("GetSaleProducts")]
        public async Task<IActionResult> GetSaleProductsAsync(int flashSaleId)
        {
            try
            {
                var products = await _productService.GetSaleProducts(flashSaleId);
                _response.IsSuccess = true;
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
                _response.IsSuccess = true;
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
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var statuscode = _response.StatusCode = HttpStatusCode.BadRequest;
                var message = _response.ErrorMessages = new List<string> { ex.Message };

                _response.Failed(statuscode, message, null);
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
    }
}
