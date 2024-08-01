using AutoMapper;
using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DtoLayer.BasketDtos;
using EVillaAgency.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBaskets()
        {
            var values = await _basketService.GetAllBAskets();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasket(CreateBasketDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _basketService.CreateBasket(dto);
                return Ok("Sepet Başarıyla Oluşturuldu.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBasketById(int id)
        {
            var values = await _basketService.GetBasketById(id);
            return Ok(values);
        }

        [HttpGet("GetLastBasketbyUserId")]
        public async Task<IActionResult> GetLastBasketbyUserId(int id)
        {
            var values = await _basketService.GetLastBasketByUserId(id);
            return Ok(values);
        }
    }
}
