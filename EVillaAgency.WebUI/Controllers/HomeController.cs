
using EVillaAgency.WebUI.Dtos.FovariteDtos;
using EVillaAgency.WebUI.Dtos.HouseDtos;
using EVillaAgency.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;

namespace EVillaAgency.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
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
