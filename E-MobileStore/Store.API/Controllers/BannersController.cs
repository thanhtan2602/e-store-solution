﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.ApiService.Services;
using Store.ApiService.Services.Interfaces;
using Store.Infrastructure.DTOs;
using System.Net;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly IBannerService _bannerService;
        private BaseApiResponse _response;
        public BannersController(IBannerService bannerService)
        {
            _bannerService = bannerService;
            _response = new BaseApiResponse();
        }
        [HttpGet]
        [Route("GetBannerByCate")]
        public async Task<IActionResult> GetBannerByCate(int page, int pageSize, string categoryUrl)
        {
            try
            {
                var listBanner = await _bannerService.GetBannerByCateAsync(page, pageSize, categoryUrl);
                _response.Result = listBanner;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var statuscode = _response.StatusCode = HttpStatusCode.BadRequest;
                var errorMessenger = _response.ErrorMessages = new List<string> { ex.Message };
                _response.IsSuccess = false;
                _response.Failed(statuscode, errorMessenger);
                return BadRequest(_response);
            }
        }
        [HttpGet]
        [Route("GetAllBanner")]
        public async Task<IActionResult> GetAllBanner(int page, int pageSize)
        {
            try
            {
                var listBanner = await _bannerService.GetAllBanner(page, pageSize);
                _response.Result = listBanner;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var statuscode = _response.StatusCode = HttpStatusCode.BadRequest;
                var errorMessenger = _response.ErrorMessages = new List<string> { ex.Message };
                _response.IsSuccess = false;
                _response.Failed(statuscode, errorMessenger);
                return BadRequest(_response);
            }
        }
        [HttpGet]
        [Route("GetBannerDetail")]
        public async Task<IActionResult> GetBannerDetail(int bannerId)
        {
            try
            {
                var banner = await _bannerService.GetBannerDetail(bannerId);
                _response.Result = banner;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var statuscode = _response.StatusCode = HttpStatusCode.BadRequest;
                var errorMessenger = _response.ErrorMessages = new List<string> { ex.Message };
                _response.IsSuccess = false;
                _response.Failed(statuscode, errorMessenger);
                return BadRequest(_response);
            }
        }
        [HttpPost]
        [Route("InsertOrUpdateBanner")]
        public IActionResult InsertOrUpdateBanner(BannerDTO banner)
        {
            try
            {
                _bannerService.InsertOrUpdateBanner(banner);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var statuscode = _response.StatusCode = HttpStatusCode.BadRequest;
                var errorMessenger = _response.ErrorMessages = new List<string> { ex.Message };
                _response.IsSuccess = false;
                _response.Failed(statuscode, errorMessenger);
                return BadRequest(_response);
            }
        }
        [HttpPut]
        [Route("DeleteBanner")]
        public IActionResult DeleteBanner(int bannerId)
        {
            try
            {
                _bannerService.DeletedBanner(bannerId);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "200";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var statuscode = _response.StatusCode = HttpStatusCode.BadRequest;
                var errorMessenger = _response.ErrorMessages = new List<string> { ex.Message };
                _response.IsSuccess = false;
                _response.Failed(statuscode, errorMessenger);
                return BadRequest(_response);
            }
        }
    }
}
