using EVillaAgency.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("GetCityCount")]
        public async Task<IActionResult> GetCityCount()
        {
            var values = await _cityService.GetCityCountAsync();
            return Ok(values);
        }

    }
}
