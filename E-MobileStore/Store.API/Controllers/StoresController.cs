using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.ApiService.Services.Interfaces;
using Store.Infrastructure.DTOs;
using System.Net;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private BaseApiResponse _response;

        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
            _response = new BaseApiResponse();
        }
        [HttpPut]
        [Route("DeletedStore")]
        public IActionResult DeletedStore(int storeId)
        {
            try
            {
                _storeService.DeletedStore(storeId);
                _response.Message = "Deleted success";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Failed(HttpStatusCode.NotFound, new List<string> { ex.Message }, null);
                return BadRequest(_response);
            }
        }

        [HttpGet]
        [Route("GetStoreById")]
        public async Task<IActionResult> GetStoreById(int storeId)
        {
            try
            {
                var store = await _storeService.GetStoreByIdAsync(storeId);
                _response.Result = store;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Failed(HttpStatusCode.NotFound, new List<string> { ex.Message }, null);
                return BadRequest(_response);
            }
        }
        [HttpGet]
        [Route("GetStoreList")]
        public async Task<IActionResult> GetStoreList(int page, int pageSize)
        {
            try
            {
                var storeList = await _storeService.GetStoreListAsync(page, pageSize);
                _response.Result = storeList;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Failed(HttpStatusCode.NotFound, new List<string> { ex.Message }, null);
                return BadRequest(_response);
            }
        }
        [HttpPost]
        [Route("InsertOrUpdateStore")]
        public IActionResult InsertOrUpdateStore(StoreDTO storeDTO)
        {
            try
            {
                _storeService.InSertOrUpdateStore(storeDTO);
                if (storeDTO.Id > 0)
                {
                    _response.Message = "Update Success";
                }
                else
                {
                    _response.Message = "Insert Success";
                }
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                if (storeDTO.Id > 0)
                {
                    _response.Message = "Update Failed";
                }
                else
                {
                    _response.Message = "Insert Failed";
                }
                _response.Failed(HttpStatusCode.NotFound, new List<string> { ex.Message }, null);
                return BadRequest(_response);
            }
        }
    }
}
