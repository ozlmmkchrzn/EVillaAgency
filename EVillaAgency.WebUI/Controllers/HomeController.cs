
using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.EntityLayer.Concrete;
using EVillaAgency.WebUI.Dtos.BasketDtos;
using EVillaAgency.WebUI.Dtos.FovariteDtos;
using EVillaAgency.WebUI.Dtos.HouseDtos;
using EVillaAgency.WebUI.Dtos.LoginDtos;
using EVillaAgency.WebUI.Dtos.UserDtos;
using EVillaAgency.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace EVillaAgency.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserService _userService;

        public HomeController(IHttpClientFactory httpClientFactory, IUserService userService)
        {
            _httpClientFactory = httpClientFactory;
            _userService = userService;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var houseViewModel = new IndexHouseFavoriteModel();

            // Get Last 6 Houses
            var responseMessage = await client.GetAsync("https://localhost:7037/api/House/GetLast6Houses");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                houseViewModel.LastSixHouses = JsonConvert.DeserializeObject<List<ResultHousesWithNamesDto>>(jsonData);
            }

            // Get Top 3 Favorited Houses
            responseMessage = await client.GetAsync("https://localhost:7037/api/Favorites/GetTop3FavoritedHouses");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData1 = await responseMessage.Content.ReadAsStringAsync();
                houseViewModel.TopFavoritedHouses = JsonConvert.DeserializeObject<List<ResultTop3FavoritedHousesDto>>(jsonData1);
            }

            return View(houseViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> HouseSingle(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7037/api/House/GetHousesWithNamesById/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var houseDetail = JsonConvert.DeserializeObject <ResultHousesWithNamesDto>(jsonData);
                return View(houseDetail);
            }
            return View("Error");
        }

        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CreateUserDto model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    CreatedAt = DateTime.Now,
                    ImageUrl = model.ImageUrl,
                    Password = model.Password,
                    Phone = model.Phone
                };
              await _userService.InsertAsync(user);
                
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userService.ValidateUserAsync(model.Email, model.Password);
            if (user != null)
            {
                // Başarılı giriş
                // Giriş işlemlerini yapabilirsiniz, örneğin: session ayarlamak
                HttpContext.Session.SetInt32("UserId", user.UserId);
                return RedirectToAction("Index", "Home");
            }
            // Başarısız giriş
            ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return Json(new { success = false, message = "Kullanıcı kimliği bulunamadı." });
            }

            CreateBasketDto createBasketDto = new CreateBasketDto
            {
                HouseId = id,
                UserId = userId.Value
            };

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBasketDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                var responseMessage = await client.PostAsync("https://localhost:7037/api/Basket", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                    var basketId = JsonConvert.DeserializeObject<int>(responseContent);

                    // Sepet ID'sini Session'a ekleyin
                    HttpContext.Session.SetInt32("BasketId", basketId);

                    return Json(new { basketId = basketId });
                }
                else
                {
                    var errorMessage = await responseMessage.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = string.IsNullOrEmpty(errorMessage) ? "Bir hata oluştu." : errorMessage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Bir hata oluştu: {ex.Message}" });
            }
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
