using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DtoLayer.UserDtos;
using EVillaAgency.EntityLayer.Concrete;
using EVillaAgency.WebUI.Dtos.HouseDtos;
using EVillaAgency.WebUI.Dtos.HouseTypeDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace EVillaAgency.WebUI.Controllers.Admin
{
    public class HouseController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HouseController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        

        [HttpGet]
        public async Task<IActionResult> Index(int? houseTypeId)
        {
            var client = _httpClientFactory.CreateClient();

            // Ev Türlerini Getir
            var responseMessage = await client.GetAsync("https://localhost:7037/api/HouseType");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var houseTypes = JsonConvert.DeserializeObject<List<ResultHouseTypeDto>>(jsonData);

            // Dropdown için SelectListItem listesi oluştur
            var houseTypeSelectList = houseTypes.Select(ht => new SelectListItem
            {
                Value = ht.Id.ToString(),
                Text = ht.Name
            }).ToList();

            // "Bütün Evler" seçeneğini ekle
            houseTypeSelectList.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "Bütün Evler"
            });

            ViewBag.HouseTypes = houseTypeSelectList;

            // Evleri Getir
            responseMessage = houseTypeId.HasValue
                ? await client.GetAsync($"https://localhost:7037/api/House/GetHousesByHouseTypeId?id={houseTypeId}")
                : await client.GetAsync("https://localhost:7037/api/House");

            if (responseMessage.IsSuccessStatusCode)
            {
                jsonData = await responseMessage.Content.ReadAsStringAsync();
                var houses = JsonConvert.DeserializeObject<List<ResultHousesWithNamesDto>>(jsonData);
                return View(houses);
            }

            return View(new List<ResultHousesWithNamesDto>());
        }





        [HttpGet]
        public async Task<IActionResult> CreateHouse()
        {
            var client = _httpClientFactory.CreateClient();

            // Get House Types
            var responseMessage = await client.GetAsync("https://localhost:7037/api/HouseType");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var houseTypes = JsonConvert.DeserializeObject<List<ResultHouseTypeDto>>(jsonData);

            var houseTypeSelectList = houseTypes.Select(ht => new SelectListItem
            {
                Value = ht.Id.ToString(),
                Text = ht.Name
            }).ToList();

            ViewBag.HouseTypes = houseTypeSelectList;

            // Get Users
            responseMessage = await client.GetAsync("https://localhost:7037/api/User");
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

            ViewBag.Users = userSelectList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateHouse(CreateHouseDto createHouseDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createHouseDto);
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createHouseDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7037/api/House", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(createHouseDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateHouse(int id)
        {
            var client = _httpClientFactory.CreateClient();

            // Ev Türlerini Getir
            var responseMessage = await client.GetAsync("https://localhost:7037/api/HouseType");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var houseTypes = JsonConvert.DeserializeObject<List<ResultHouseTypeDto>>(jsonData);

            var houseTypeSelectList = houseTypes.Select(ht => new SelectListItem
            {
                Value = ht.Id.ToString(),
                Text = ht.Name
            }).ToList();

            ViewBag.HouseTypes = houseTypeSelectList;

            // Kullanıcıları Getir
            responseMessage = await client.GetAsync("https://localhost:7037/api/User");
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

            ViewBag.Users = userSelectList;

            // Mevcut Ev Bilgilerini Getir
            responseMessage = await client.GetAsync($"https://localhost:7037/api/House/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            jsonData = await responseMessage.Content.ReadAsStringAsync();
            var house = JsonConvert.DeserializeObject<UpdateHouseDto>(jsonData);

            return View(house);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHouse(UpdateHouseDto updateHouseDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateHouseDto);
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateHouseDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            var responseMessage = await client.PutAsync("https://localhost:7037/api/House", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(updateHouseDto);
        }

        public async Task<IActionResult> DeleteHouse(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7037/api/House/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
