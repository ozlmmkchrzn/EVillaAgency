
using EVillaAgency.WebUI.Dtos.FavoriteDtos;
using EVillaAgency.WebUI.Dtos.HouseDtos;
using EVillaAgency.WebUI.Dtos.UserDtos;

//using EVillaAgency.WebUI.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace EVillaAgency.WebUI.Controllers.Admin
{
    public class FavoriteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FavoriteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7037/api/Favorites/GetAllFavoritesWithNames");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFavoriteDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateFavorite()
        {
            var client = _httpClientFactory.CreateClient();

            // Get Houses
            var responseMessage = await client.GetAsync("https://localhost:7037/api/House");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var houseTypes = JsonConvert.DeserializeObject<List<ResultHousesWithNamesDto>>(jsonData);

            var housesSelectList = houseTypes.Select(ht => new SelectListItem
            {
                Value = ht.HouseId.ToString(),
                Text = ht.Title
            }).ToList();

            ViewBag.Houses = housesSelectList;

            // Get users
            responseMessage = await client.GetAsync("https://localhost:7037/api/AppUser");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            jsonData = await responseMessage.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<ResultUserDto>>(jsonData);

            var userSelectList = users.Select(u => new SelectListItem
            {
                Value = u.UserId.ToString(),
                Text = u.Username
            }).ToList();

            ViewBag.users = userSelectList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFavorite(CreateFavoriteDto createFavoriteDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createFavoriteDto);
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createFavoriteDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7037/api/Favorites", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(createFavoriteDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFavorite(int id)
        {
            var client = _httpClientFactory.CreateClient();

            // Ev Türlerini Getir
            var responseMessage = await client.GetAsync("https://localhost:7037/api/House");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var houses = JsonConvert.DeserializeObject<List<ResultHousesWithNamesDto>>(jsonData);

            var houseSelectList = houses.Select(ht => new SelectListItem
            {
                Value = ht.HouseId.ToString(),
                Text = ht.Title,
            }).ToList();

            ViewBag.Houses = houseSelectList;

            // Kullanıcıları Getir
            responseMessage = await client.GetAsync("https://localhost:7037/api/AppUser");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            jsonData = await responseMessage.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<List<ResultUserDto>>(jsonData);

            var usersSelectList = user.Select(u => new SelectListItem
            {
                Value = u.UserId.ToString(),
                Text = u.Username,
            }).ToList();

            ViewBag.Users = usersSelectList;

            // Mevcut Ev Bilgilerini Getir
            responseMessage = await client.GetAsync($"https://localhost:7037/api/Favorites/GetHouseNameAndUsernameByFavoriteId/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            jsonData = await responseMessage.Content.ReadAsStringAsync();
            var house = JsonConvert.DeserializeObject<UpdateFavoriteDto>(jsonData);

            return View(house);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFavorite(UpdateFavoriteDto updateFavoriteDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateFavoriteDto);
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateFavoriteDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            var responseMessage = await client.PutAsync("https://localhost:7037/api/Favorites", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(updateFavoriteDto);
        }

        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7037/api/Favorites/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
