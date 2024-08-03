using AutoMapper;
using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DataAccessLayer.Context;
using EVillaAgency.DtoLayer.CouponDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;
        private readonly IMapper _mapper;

        public CouponController(ICouponService couponService, IMapper mapper)
        {
            _couponService = couponService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CreateCouponDto dto)
        {
            await _couponService.CreateCoupon(dto);
            return Ok("Kupon Başarıyla Oluşturuldu.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoupons()
        {
            var values = await _couponService.GetListAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCouponById(int id)
        {
            var values = await _couponService.GetByIDAsync(id);
            return Ok(values);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCoupon(UpdateCouponDto dto)
        {
            await _couponService.UpdateCoupon(dto);
            return Ok("Kupon Başarıyla Güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var values = await _couponService.GetByIDAsync(id);
            await _couponService.DeleteAsync(values);
            return Ok("Kupon Başarıyla Silindi.");
        }

        [HttpGet("CheckCoupon")]
        public async Task<IActionResult> CheckCoupon([FromQuery] string coupon)
        {
            if (string.IsNullOrWhiteSpace(coupon))
            {
                return BadRequest("Geçersiz kupon kodu.");
            }

            var result = await _couponService.CheckCouponAsync(coupon);
            return Ok(result);
        }
    }
}
