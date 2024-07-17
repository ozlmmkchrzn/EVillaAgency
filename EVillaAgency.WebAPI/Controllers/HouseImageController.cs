using AutoMapper;
using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DtoLayer.HouseImageDtos;
using EVillaAgency.DtoLayer.HouseTypeDtos;
using EVillaAgency.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseImageController : ControllerBase
    {
        private readonly IHouseImageService _houseImageService;
        private readonly IMapper _mapper;

        public HouseImageController(IHouseImageService houseImageService, IMapper mapper)
        {
            _houseImageService = houseImageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHouseImages()
        {
            var values = await _houseImageService.GetListAsync();
            var result = _mapper.Map<List<ResultHouseImageDto>>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHouseImageByID(int id)
        {
            var values = await _houseImageService.GetByIDAsync(id);
            var result = _mapper.Map<ResultHouseImageDto>(values);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertHouseImage(CreateHouseImageDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var value = _mapper.Map<HouseImage>(dto);
                await _houseImageService.InsertAsync(value);
                return Ok("Ev Resmi Başarıyla Eklendi.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHouseImage(UpdateHouseImageDto dto)
        {
            var existingImage = await _houseImageService.GetByIDAsync(dto.Id);

            if (existingImage == null)
            {
                return NotFound();
            }

            // AutoMapper kullanarak DTO'dan mevcut resme güncelleme yapalım
            _mapper.Map(dto, existingImage);

            try
            {
                await _houseImageService.UpdateAsync(existingImage); // Veritabanında güncelleme işlemi
                return Ok("Ev Resmi Başarıyla Güncellendi.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating image: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHouseImage(int id)
        {
            var values = await _houseImageService.GetByIDAsync(id);
            if (values == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    await _houseImageService.DeleteAsync(values); // Veritabanından silme işlemi
                    return Ok("Ev Resmi Başarıyla Silindi.");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting image: {ex.Message}");
                }
            }
        }
    }
}
