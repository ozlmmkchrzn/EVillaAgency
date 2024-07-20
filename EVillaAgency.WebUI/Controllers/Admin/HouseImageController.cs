

using EVillaAgency.WebUI.Dtos.HouseDtos;
using EVillaAgency.WebUI.Dtos.HouseImageDtos;
using EVillaAgency.WebUI.Dtos.ImageDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace EVillaAgency.WebUI.Controllers.Admin
{
    public class HouseImageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HouseImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7037/api/HouseImage/GetImagesWithNames");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultHouseImageDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateHouseImage()
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

            // Get Images
            responseMessage = await client.GetAsync("https://localhost:7037/api/Images");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            jsonData = await responseMessage.Content.ReadAsStringAsync();
            var images = JsonConvert.DeserializeObject<List<ResultImageDto>>(jsonData);

            var imageSelectList = images.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Url
            }).ToList();

            ViewBag.Images = imageSelectList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateHouseImage(CreateHouseImageDto createHouseImageDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createHouseImageDto);
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createHouseImageDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7037/api/HouseImage", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(createHouseImageDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateHouseImages(int id)
        {
            var client = _httpClientFactory.CreateClient();

            // Ev Türlerini Getir
            var responseMessage = await client.GetAsync("https://localhost:7037/api/Images");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var images = JsonConvert.DeserializeObject<List<ResultImageDto>>(jsonData);

            var imageSelectList = images.Select(ht => new SelectListItem
            {
                Value = ht.Id.ToString(),
                Text = ht.Url
            }).ToList();

            ViewBag.Images = imageSelectList;

            // Kullanıcıları Getir
            responseMessage = await client.GetAsync("https://localhost:7037/api/House");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            jsonData = await responseMessage.Content.ReadAsStringAsync();
            var houses = JsonConvert.DeserializeObject<List<ResultHousesWithNamesDto>>(jsonData);

            var housesSelectList = houses.Select(u => new SelectListItem
            {
                Value = u.HouseId.ToString(),
                Text = u.Title
            }).ToList();

            ViewBag.Houses = housesSelectList;

            // Mevcut Ev Bilgilerini Getir
            responseMessage = await client.GetAsync($"https://localhost:7037/api/HouseImage/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            jsonData = await responseMessage.Content.ReadAsStringAsync();
            var house = JsonConvert.DeserializeObject<UpdateHouseImageDto>(jsonData);

            return View(house);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHouseImage(UpdateHouseImageDto updateHouseImageDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateHouseImageDto);
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateHouseImageDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            var responseMessage = await client.PutAsync("https://localhost:7037/api/HouseImage", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(updateHouseImageDto);
        }
    }
}
