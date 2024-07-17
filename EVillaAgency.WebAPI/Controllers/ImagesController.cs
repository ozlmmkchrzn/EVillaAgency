using AutoMapper;
using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DtoLayer.ImageDtos;
using EVillaAgency.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ImagesController(IImageService imageService, IMapper mapper)
        {
            _imageService = imageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var values = await _imageService.GetListAsync();
            var result =_mapper.Map<List<ResultImageDto>>(values);
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageById(int id)
        {
            var values = await _imageService.GetByIDAsync(id);
            var result = _mapper.Map<ResultImageDto>(values);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertImage(CreateImageDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var value = _mapper.Map<Image>(dto);
                await _imageService.InsertAsync(value);
                return Ok("Resim Başarıyla Eklendi.");
            }
               
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var values = await _imageService.GetByIDAsync(id);
            if (values == null)
            {
                return NotFound();
            }
            else 
            {
                try
                {
                    await _imageService.DeleteAsync(values); // Veritabanından silme işlemi
                    return Ok("Resim Başarıyla Silindi.");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting image: {ex.Message}");
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateImage( UpdateImageDto dto)
        {
            

            var existingImage = await _imageService.GetByIDAsync(dto.Id);

            if (existingImage == null)
            {
                return NotFound();
            }

            // AutoMapper kullanarak DTO'dan mevcut resme güncelleme yapalım
            _mapper.Map(dto, existingImage);

            try
            {
                await _imageService.UpdateAsync(existingImage); // Veritabanında güncelleme işlemi
                return Ok("Resim Başarıyla Güncellendi.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating image: {ex.Message}");
            }
        }
    }
}
