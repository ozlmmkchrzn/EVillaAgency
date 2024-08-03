using EVillaAgency.EntityLayer.Concrete;
using EVillaAgency.WebUI.Dtos.BasketDtos;
using EVillaAgency.WebUI.Dtos.CouponDtos;
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
                return View("Index"); // Sepet sayfasını tekrar gösterir
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                var requestUrl = $"https://localhost:7037/api/Coupon/CheckCoupon?coupon={Uri.EscapeDataString(couponCode)}";

                var responseMessage = await client.GetAsync(requestUrl);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResultCouponDto>(jsonResponse);

                    if (result != null && result.IsValid)
                    {
                        // Kupon uygulaması başarılı
                        HttpContext.Session.SetString("DiscountRate", result.DiscountRate.ToString());
                        return RedirectToAction("Index", "Basket"); // Sepet sayfasına yönlendir
                    }
                    else
                    {
                        // Kupon geçersiz
                        ModelState.AddModelError("", "Geçersiz kupon kodu.");
                        return View("Index"); // Sepet sayfasını tekrar gösterir
                    }
                }
                else
                {
                    // API isteği başarısız
                    ModelState.AddModelError("", "Kupon uygulama işlemi sırasında bir hata oluştu.");
                    return View("Index"); // Sepet sayfasını tekrar gösterir
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                ModelState.AddModelError("", $"Bir hata oluştu: {ex.Message}");
                return View("Index"); // Sepet sayfasını tekrar gösterir
            }
        }

    }
}
