using AutoMapper;
using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DtoLayer.HeatingTypeDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeatingTypeApiController : ControllerBase
    {
        private readonly IHeatingTypeService _heatingTypeService;
        private readonly IMapper _mapper;

        public HeatingTypeApiController(IHeatingTypeService heatingTypeService, IMapper mapper)
        {
            _heatingTypeService = heatingTypeService;
            _mapper = mapper;
        }

        [HttpGet("GetHeatingTypes")]
        public async Task<IActionResult> GetHeatingTypes()
        {
            try
            {
                var values = await _heatingTypeService.GetListAsync();
                var result = _mapper.Map<List<ResultHeatingTypeDto>>(values);
                return Ok(result); // 'result' nesnesini döndürmelisiniz.
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
