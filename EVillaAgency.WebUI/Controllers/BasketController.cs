
using EVillaAgency.WebUI.Dtos.BasketDtos;
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

        public async Task<IActionResult> Index(int id)
        {
            var basketId = HttpContext.Session.GetInt32("BasketId");
            if (!basketId.HasValue)
            {
                ViewBag.ErrorMessage = "Sepet ID'si bulunamadı.";
                return View();
            }

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7037/api/Basket/{basketId.Value}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var basket = JsonConvert.DeserializeObject<ResultBasketDto>(jsonData);
                return View(basket);
            }

            ViewBag.ErrorMessage = "Sepet bilgisi alınamadı.";
            return View();
        }

    }
}
