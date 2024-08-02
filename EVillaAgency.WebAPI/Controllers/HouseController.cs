using AutoMapper;
using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DtoLayer.HouseDtos;
using EVillaAgency.DtoLayer.UserDtos;
using EVillaAgency.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseService _houseService;
        private readonly IMapper _mapper;

        public HouseController(IHouseService houseService, IMapper mapper)
        {
            _houseService = houseService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHouses()
        {
            var values = await _houseService.GetListAsync();
            var result = _mapper.Map<List<ResultHouseDto>>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHouseByID(int id)
        {
            var values = await _houseService.GetByIDAsync(id);
            var result = _mapper.Map<ResultHouseDto>(values);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertHouse(CreateHouseDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                
                var value = _mapper.Map<House>(dto);
                value.CreatedAt = DateTime.Now;
                await _houseService.InsertAsync(value);
                return Ok("Ev Başarıyla Eklendi.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHouse(UpdateHouseDto dto)
        {
            var existingImage = await _houseService.GetByIDAsync(dto.HouseId);

            if (existingImage == null)
            {
                return NotFound();
            }

            dto.UpdatedAt = DateTime.Now;
            // AutoMapper kullanarak DTO'dan mevcut resme güncelleme yapalım
            _mapper.Map(dto, existingImage);

            try
            {
                await _houseService.UpdateAsync(existingImage); // Veritabanında güncelleme işlemi
                return Ok("Ev Başarıyla Güncellendi.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating image: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHouse(int id)
        {
            var values = await _houseService.GetByIDAsync(id);
            if (values == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    await _houseService.DeleteAsync(values); // Veritabanından silme işlemi
                    return Ok("Ev Başarıyla Silindi.");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting image: {ex.Message}");
                }
            }
        }

        [HttpGet("GetHousesWithNames")]
        public async Task<IActionResult> GetHousesWithNames()
        {
            var values = await _houseService.GetHouseWithNamesAsync();
            return Ok(values);
        }

        [HttpGet("GetHousesWithNamesById/{id}")]
        public async Task<IActionResult> GetHousesWithNamesById(int id)
        {
            var values = await _houseService.GetHouseWithNamesByIdAsync(id);
            return Ok(values);
        }

        [HttpGet("GetHousesByHouseTypeId")]
        public async Task<IActionResult> GetHousesByHouseTypeId(int id)
        {
            var values = await _houseService.GetHousesByHouseTypeId(id);
            return Ok(values);
        }

        [HttpGet("GetLast6Houses")]
        public async Task<IActionResult> GetLast6Houses()
        {
            var values = await _houseService.GetLast6Houses();
            return Ok(values);
        }

        [HttpGet("GetTotalHouseOwnerCount")]
        public async Task<IActionResult> GetTotalHouseOwnerCount()
        {
            var values = await _houseService.GetTotalHouseOwnerCount();
            return Ok(values);
        }

        [HttpGet("GetLatestHouseByHouseType")]
        public async Task<IActionResult> GetLatestHouseByHouseType(int id)
        {
            var values = await _houseService.GetLatestHouseByHouseTypeAsync(id);
            return Ok(values);
        }

        [HttpGet("GetLast6ActivesHouses")]
        public async Task<IActionResult> GetLast6ActivesHouses()
        {
            var values = await _houseService.GetLast6ActiveHouses();
            return Ok(values);
        }
    }
}
