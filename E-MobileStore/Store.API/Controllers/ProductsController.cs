using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.ApiService.Services.Interfaces;
using Store.Domain.Entities;

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
        public async Task<IActionResult> GetProductList(int categoryId)
        {
            var products = _productService.GetProductListAsync(categoryId);
            return Ok(products);
        }

        [HttpGet]
        [Route("GetProductById")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            return Ok(product);
        }

        [HttpPut]
        [Route("InsertProduct")]
        public IActionResult AddProduct(Product product)
        {
            _productService.AddProductAsync(product);
            return Ok(_response);
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public IActionResult DeleteProduct(int productId)
        {
            _productService.DeleteProductAsync(productId);
            return Ok(_response);
        }

        [HttpPost]
        [Route("UpdateProduct")]
        public IActionResult UpdateProduct(Product product)
        {
            _productService.UpdateProductAsync(product);
            return Ok(_response);
        }
    }
}
