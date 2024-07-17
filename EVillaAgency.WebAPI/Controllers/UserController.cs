using AutoMapper;
using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DtoLayer.HouseTypeDtos;
using EVillaAgency.DtoLayer.UserDtos;
using EVillaAgency.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var values = await _userService.GetListAsync();
            var result = _mapper.Map<List<ResultUserDto>>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByID(int id)
        {
            var values = await _userService.GetByIDAsync(id);
            var result = _mapper.Map<ResultUserDto>(values);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser(CreateUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                dto.CreatedAt = DateTime.Now;
                var value = _mapper.Map<User>(dto);
                await _userService.InsertAsync(value);
                return Ok("Kullanıcı Başarıyla Eklendi.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserDto dto)
        {
            var existingImage = await _userService.GetByIDAsync(dto.UserId);

            if (existingImage == null)
            {
                return NotFound();
            }

            // AutoMapper kullanarak DTO'dan mevcut resme güncelleme yapalım
            _mapper.Map(dto, existingImage);

            try
            {
                await _userService.UpdateAsync(existingImage); // Veritabanında güncelleme işlemi
                return Ok("Kullanıcı Başarıyla Güncellendi.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating image: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var values = await _userService.GetByIDAsync(id);
            if (values == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    await _userService.DeleteAsync(values); // Veritabanından silme işlemi
                    return Ok("Kullanıcı Başarıyla Silindi.");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting image: {ex.Message}");
                }
            }
        }
    }
}
