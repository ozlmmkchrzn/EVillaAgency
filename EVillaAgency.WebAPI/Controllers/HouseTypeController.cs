using AutoMapper;
using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DtoLayer.HouseTypeDtos;
using EVillaAgency.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EVillaAgency.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseTypeController : ControllerBase
    {
        private readonly IHouseTypeService _houseTypeService;
        private readonly IMapper _mapper;

        public HouseTypeController(IHouseTypeService houseTypeService, IMapper mapper)
        {
            _houseTypeService = houseTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHouseTypes()
        {
            var values = await _houseTypeService.GetListAsync();
            var result = _mapper.Map<List<ResultHouseTypeDto>>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHouseTypeByID(int id)
        {
            var values = await _houseTypeService.GetByIDAsync(id);
            var result = _mapper.Map<ResultHouseTypeDto>(values);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertHouseType(CreateHouseTypeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var value = _mapper.Map<HouseType>(dto);
                await _houseTypeService.InsertAsync(value);
                return Ok("Ev Tipi Başarıyla Eklendi.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHouseType(UpdateHouseTypeDto dto)
        {
            var existingImage = await _houseTypeService.GetByIDAsync(dto.Id);

            if (existingImage == null)
            {
                return NotFound();
            }

            // AutoMapper kullanarak DTO'dan mevcut resme güncelleme yapalım
            _mapper.Map(dto, existingImage);

            try
            {
                await _houseTypeService.UpdateAsync(existingImage); // Veritabanında güncelleme işlemi
                return Ok("Ev Tipi Başarıyla Güncellendi.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating image: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHouseType(int id)
        {
            var values = await _houseTypeService.GetByIDAsync(id);
            if (values == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    await _houseTypeService.DeleteAsync(values); // Veritabanından silme işlemi
                    return Ok("Ev Tipi Başarıyla Silindi.");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting image: {ex.Message}");
                }
            }
        }
    }
}
