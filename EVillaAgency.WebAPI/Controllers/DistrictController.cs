using AutoMapper;
using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DtoLayer.FavoriteDtos;
using EVillaAgency.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService _districtService;
        private readonly IMapper _mapper;

        public DistrictController(IDistrictService districtService, IMapper mapper)
        {
            _districtService = districtService;
            _mapper = mapper;
        }

        [HttpGet("GetDistrictCount")]
        public async Task<IActionResult> GetDistrictCountAsync()
        {
            var values = await _districtService.GetDistrictCountAsync();
            return Ok(values);
        }
    }
}
