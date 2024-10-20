using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Store.ApiService.Services.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using System.Net;
using System.Threading;
using static Store.Common.Utility.ProductsUtility;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
		private readonly ILogger<ProductsController> _logger;
		private readonly IProductService _productService;
        private readonly IDistributedCache _distributedCache;
        private BaseApiResponse _response;
        public ProductsController(ILogger<ProductsController> logger,IProductService productService, IDistributedCache distributedCache)
        {
			_logger= logger;
			_productService = productService;
            _distributedCache = distributedCache;
            _response = new BaseApiResponse();
        }

        [HttpGet]
        [Route("GetSaleProducts")]
        public async Task<IActionResult> GetSaleProductsAsync(int flashSaleId, CancellationToken cancellationToken = default)
        {
            try
            {
                var key = $"sale:{flashSaleId}";
                string? cacheMember = await _distributedCache.GetStringAsync(key, cancellationToken);
                IEnumerable<FlashSaleProduct>? products;
                if (String.IsNullOrEmpty(cacheMember))
                {
                    products = await _productService.GetSaleProducts(flashSaleId);
                    string? jsonList = JsonConvert.SerializeObject(products, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    await _distributedCache.SetStringAsync(key, jsonList, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) }, cancellationToken);
                    _response.Success(products);
                }
                else
                {
                    products = JsonConvert.DeserializeObject<IEnumerable<FlashSaleProduct>>(cacheMember);
                    _response.Success(products);
                }
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
        [Route("GetProductByUrl")]
        public async Task<IActionResult> GetProductByUrl(string productUrl, CancellationToken cancellationToken = default)
        {
            try
            {
                var key = $"product={productUrl}";
                string? cacheMember = await _distributedCache.GetStringAsync(key, cancellationToken);
                Product? product;
                if (String.IsNullOrEmpty(cacheMember))
                {
                    product = await _productService.GetProductByUrlAsync(productUrl);
                    string? json = JsonConvert.SerializeObject(product, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    await _distributedCache.SetStringAsync(key, json, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) }, cancellationToken);
                    _response.Success(product);
                }
                else
                {
                    product = JsonConvert.DeserializeObject<Product>(cacheMember);
                    _response.Success(product);
                }
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
        public async Task<IActionResult> AddProduct(ProductDTO product, CancellationToken cancellationToken = default)
        {
            try
            {
                _response.Message = await _productService.AddOrUpdateProduct(product);
                var key = $"product={ToUrl(product.Name)}";
                await _distributedCache.RemoveAsync(key, cancellationToken);
                string productcate;
                for (int i = 0; i < 10; i++)
                {
                    productcate = $"productCate:page={i}";
                    await _distributedCache.RemoveAsync(productcate, cancellationToken);
                }
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"error to add or update:{ex.Message}");
				var statuscode = _response.StatusCode = HttpStatusCode.BadRequest;
                var message = _response.ErrorMessages = new List<string> { ex.Message };
                _response.Message = ex.Message;
                _response.Failed(statuscode, message);
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }
        [HttpPut]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(string productUrl, CancellationToken cancellationToken = default)
        {
            try
            {
                _productService.DeleteProduct(productUrl);
                var key = $"product={productUrl}";
                await _distributedCache.RemoveAsync(key, cancellationToken);
                string productcate;
                for (int i = 0; i < 10; i++)
                {
                    productcate = $"productCate:page={i}";
                    await _distributedCache.RemoveAsync(productcate, cancellationToken);
                }
                _response.IsSuccess = true;
                _response.Message = "200";
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
        [Route("GetProductListByCateUrl")]
        public async Task<IActionResult> GetProductListByCate(string cateUrl, int page, int pageSize, string? sortBy, CancellationToken cancellationToken = default)
        {
            var key = $"productCate:{cateUrl}&pageSize:{pageSize}";
            string? cacheMember = await _distributedCache.GetStringAsync(key, cancellationToken);
            IEnumerable<Product>? products;
            if (String.IsNullOrEmpty(cacheMember))
            {
                products = await _productService.GetProductListByCateUrlAsync(cateUrl, page, pageSize, sortBy);
                string? jsonList = JsonConvert.SerializeObject(products, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                await _distributedCache.SetStringAsync(key, jsonList, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) }, cancellationToken);
                _response.Result = products;
            }
            else
            {
                products = JsonConvert.DeserializeObject<IEnumerable<Product>>(cacheMember);
                _response.Result = products;
            }
            return Ok(_response);
        }
        [HttpGet]
        [Route("GetProductList")]
        public async Task<IActionResult> GetProductList(int page, int pageSize, string? sortBy, CancellationToken cancellationToken = default)
        {
            var key = $"productCate:page={page}";
            string? cacheMember = await _distributedCache.GetStringAsync(key, cancellationToken);
            IEnumerable<Product>? products;
            if (String.IsNullOrEmpty(cacheMember))
            {
                products = await _productService.GetProductListAsync(page, pageSize, sortBy);
                string? jsonList = JsonConvert.SerializeObject(products, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                await _distributedCache.SetStringAsync(key, jsonList, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) }, cancellationToken);
                _response.Result = products;
            }
            else
            {
                products = JsonConvert.DeserializeObject<IEnumerable<Product>>(cacheMember);
                _response.Result = products;
            }
            return Ok(_response);
        }

        [HttpGet]
        [Route("TotalProductByCate")]
        public async Task<IActionResult> TotalProductByCateAsync(string cateUrl)
        {
            var products = await _productService.TotalProductByCateAsync(cateUrl);
            if (products == 0)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
            }
            _response.Result = products;
            return Ok(_response);
        }
        [HttpGet]
        [Route("TotalProduct")]
        public async Task<IActionResult> TotalProductAsync()
        {
            var products = await _productService.TotalProductAsync();
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
