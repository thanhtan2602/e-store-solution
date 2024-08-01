using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.ApiService.Services.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.DTOs;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeValuesController : ControllerBase
    {
        private readonly IAttributeValueService _attributeValueService;
        private BaseApiResponse _response;
        public AttributeValuesController(IAttributeValueService attributeValueService)
        {
            _attributeValueService = attributeValueService;
        }
        [HttpPost]
        [Route("InsertAttributeValue")]
        public IActionResult AddAttributes(AttributeDTO attributeValue)
        {
            _attributeValueService.AddAttributesAsync(attributeValue);
            return Ok(_response);
        }
    }
}
