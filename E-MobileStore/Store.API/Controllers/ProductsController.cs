using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.ApiService.Services.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
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
        public async Task<IActionResult> GetProductList(int categoryId, int page = 1, int pageSize = 2)
        {
            var products = _productService.GetProductListAsync(categoryId, page, pageSize);
            var jsonList = JsonSerializer.Serialize(products);
            return Content(jsonList, "application/json");
        }
        [HttpGet]
        [Route("GetSaleProducts")]
        public async Task<IActionResult> GetSaleProducts(int flashSaleId)
        {
            var products = _productService.GetSaleProductsAsync(flashSaleId);
            var jsonList = JsonSerializer.Serialize(products);
            return Content(jsonList, "application/json");
        }

        [HttpGet]
        [Route("GetProductById")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            var jsonResult = JsonSerializer.Serialize(product);
            return Content(jsonResult, "application/json");
        }

        [HttpPost]
        [Route("InsertProduct")]
        public IActionResult AddProduct(ProductDTO product)
        {
            _productService.AddProductAsync(product);
            return Ok(_response);
        }

        [HttpPost]
        [Route("DeleteProduct")]
        public IActionResult DeleteProduct(Guid productId, string updateBy)
        {
            _productService.DeleteProductAsync(productId, updateBy);
            return Ok(_response);
        }

        [HttpPost]
        [Route("RestoreProduct")]
        public IActionResult RestoreProduct(Guid productId, string updateBy)
        {
            _productService.ReStoreProductAsync(productId, updateBy);
            return Ok(_response);
        }

        [HttpDelete]
        [Route("PermanentlyDelete")]
        public IActionResult PermanentlyDelete(Guid productId)
        {
            _productService.PermanentlyDeleteAsync(productId);
            return Ok(_response);
        }

        [HttpPost]
        [Route("UpdateProduct")]
        public IActionResult UpdateProduct(ProductDTO product, Guid productId)
        {
            _productService.UpdateProductAsync(product, productId);
            return Ok(_response);
        }
    }
}
