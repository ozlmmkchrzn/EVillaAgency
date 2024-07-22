using AutoMapper;
using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DtoLayer.FavoriteDtos;
using EVillaAgency.DtoLayer.HouseDtos;
using EVillaAgency.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IMapper _mapper;

        public FavoritesController(IFavoriteService favoriteService, IMapper mapper)
        {
            _favoriteService = favoriteService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFavorites()
        {
            var values = await _favoriteService.GetListAsync();
            var result = _mapper.Map<List<ResultFavoriteDto>>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFavoriteByID(int id)
        {
            var values = await _favoriteService.GetByIDAsync(id);
            var result = _mapper.Map<ResultFavoriteDto>(values);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertFavorite(CreateFavoriteDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var value = _mapper.Map<Favorite>(dto);
                await _favoriteService.InsertAsync(value);
                return Ok("Favori Başarıyla Eklendi.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFavorite(UpdateFavoriteDto dto)
        {
            var existingImage = await _favoriteService.GetByIDAsync(dto.FavoriteId);

            if (existingImage == null)
            {
                return NotFound();
            }

            // AutoMapper kullanarak DTO'dan mevcut resme güncelleme yapalım
            _mapper.Map(dto, existingImage);

            try
            {
                await _favoriteService.UpdateAsync(existingImage); // Veritabanında güncelleme işlemi
                return Ok("Favori Başarıyla Güncellendi.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating image: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var values = await _favoriteService.GetByIDAsync(id);
            if (values == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    await _favoriteService.DeleteAsync(values); // Veritabanından silme işlemi
                    return Ok("Favori Başarıyla Silindi.");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting image: {ex.Message}");
                }
            }
        }

        [HttpGet("GetHouseNameAndUsernameByFavoriteId/{id}")]
        public async Task<IActionResult> GetHouseNameAndUsernameByFavoriteId(int id)
        {
            var values = await _favoriteService.GetHouseNameAndUserNameByFavoriteIdAsync(id);
            return Ok(values);
        }

        [HttpGet("GetAllFavoritesWithNames")]
        public async Task<IActionResult> GetAllFavoritesWithNames()
        {
            var values = await _favoriteService.GetAllFavoritesWithNamesAsync();
            return Ok(values);
        }

        [HttpGet("GetTop3FavoritedHouses")]
        public async Task<IActionResult> GetTop3FavoritedHouses()
        {
            var values = await _favoriteService.GetTopFavoritedHousesAsync();
            return Ok(values);
        }

        [HttpGet("GetMostFavoritedHouse")]
        public async Task<IActionResult> GetMostFavoritedHouse()
        {
            var values = await _favoriteService.GetMostFavoritedHouseAsync();
            return Ok(values);
        }

    }
}
