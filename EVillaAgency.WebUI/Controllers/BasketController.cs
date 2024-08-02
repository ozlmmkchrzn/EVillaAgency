
using EVillaAgency.EntityLayer.Concrete;
using EVillaAgency.WebUI.Dtos.BasketDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EVillaAgency.WebUI.Controllers
{
    public class BasketController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BasketController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
           var userid = HttpContext.Session.GetInt32("UserId");

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7037/api/Basket/GetLastBasketbyUserId?id={userid}");

            

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var basket = JsonConvert.DeserializeObject<ResultBasketDto>(jsonData);

                ViewBag.BasketId = basket.BasketId;

                return View(basket);
            }

            ViewBag.ErrorMessage = "Sepet bilgisi alınamadı.";
            return View();
        }

        public async Task<IActionResult> DeleteBasket(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7037/api/Basket/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            return NoContent();
        }

    }
}
