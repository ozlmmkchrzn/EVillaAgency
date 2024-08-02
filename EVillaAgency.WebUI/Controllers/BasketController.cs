
using EVillaAgency.DtoLayer.CouponDtos;
using EVillaAgency.EntityLayer.Concrete;
using EVillaAgency.WebUI.Dtos.BasketDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace EVillaAgency.WebUI.Controllers
{
    public class BasketController : BaseController
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

        public async Task<IActionResult> ApplyCoupon(string couponCode)
        {
            if (string.IsNullOrWhiteSpace(couponCode))
            {
                ModelState.AddModelError("", "Kupon kodu boş olamaz.");
                return View(); // Geriye dönüş: formu tekrar gösterir
            }

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PostAsync(
                "https://localhost:7037/api/Coupon/ApplyCoupon",
                new StringContent(JsonConvert.SerializeObject(new { Code = couponCode }), Encoding.UTF8, "application/json")
            );

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<bool>(jsonResponse);
                var coupon  = JsonConvert.DeserializeObject<ResultCouponDto>(jsonResponse);

                if (result == true)
                {
                    // Kupon uygulaması başarılı
                    ViewBag.DiscountRate = coupon.DiscountRate;
                    return RedirectToAction("Index", "Basket"); // Sepet sayfasına yönlendir
                }
                else
                {
                    // Kupon geçersiz
                    ModelState.AddModelError("", "Geçersiz kupon kodu.");
                    return View(); // Geriye dönüş: formu tekrar gösterir
                }
            }
            else
            {
                // API isteği başarısız
                ModelState.AddModelError("", "Kupon uygulama işlemi sırasında bir hata oluştu.");
                return View(); // Geriye dönüş: formu tekrar gösterir
            }
        }

    }
}
