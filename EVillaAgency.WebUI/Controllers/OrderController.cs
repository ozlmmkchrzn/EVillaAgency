using EVillaAgency.WebUI.Dtos.OrderDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace EVillaAgency.WebUI.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

       
        public async Task<IActionResult> CreateOrder(int id)
        {
            var basketid = ViewBag.BasketId;
            var client = _httpClientFactory.CreateClient();

            // CreateOrderDto nesnesini oluşturun
            CreateOrderDto createOrderDto = new CreateOrderDto
            {
                BasketId = id
            };

            // JSON verisini oluşturun
            var jsonData = JsonConvert.SerializeObject(createOrderDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // API'ye POST isteği gönderin
            var responseMessage = await client.PostAsync("https://localhost:7037/api/Order", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Hata durumunda
                ViewBag.ErrorMessage = "Sipariş oluşturulurken bir hata oluştu.";
                return View("Index", "Basket");
            }

        }

    }
}
