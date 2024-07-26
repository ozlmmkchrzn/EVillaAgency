
using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.EntityLayer.Concrete;
using EVillaAgency.WebUI.Dtos.FovariteDtos;
using EVillaAgency.WebUI.Dtos.HouseDtos;
using EVillaAgency.WebUI.Dtos.UserDtos;
using EVillaAgency.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;

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
                _userService.InsertAsync(user);
                
                return RedirectToAction("Login");
            }
            return View(model);
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
