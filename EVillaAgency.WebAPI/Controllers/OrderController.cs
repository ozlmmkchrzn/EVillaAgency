using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DtoLayer.OrderDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasket(CreateOrderDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _orderService.CreateOrder(dto);
                return Ok("Sepet Başarıyla Oluşturuldu.");
            }
        }
    }
}
