using EVillaAgency.WebUI.Dtos.HeatingTypeDtos;
using EVillaAgency.WebUI.Dtos.HouseDtos;
using EVillaAgency.WebUI.Dtos.HouseTypeDtos;
using EVillaAgency.WebUI.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace EVillaAgency.WebUI.Controllers
{
    public class UserAdminController : BaseController
    {

		private readonly IHttpClientFactory _httpClientFactory;
		public UserAdminController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public IActionResult _UserAdminLayout()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ListHouses(int? userId)
        {
            userId = ViewBag.UserId;
            var client = _httpClientFactory.CreateClient();

            // Evleri Getir
            var responseMessage = await client.GetAsync($"https://localhost:7037/api/House/byOwner/{userId}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View(new List<ResultHousesWithNamesDto>());
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var houses = JsonConvert.DeserializeObject<List<ResultHousesWithNamesDto>>(jsonData);
            return View(houses);
        }


        [HttpGet]
        public async Task<IActionResult> UserCreateHouse()
        {
            var client = _httpClientFactory.CreateClient();

            // House Types
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

            // Users
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
            ViewBag.Users = userSelectList;

            // Heating Types
            responseMessage = await client.GetAsync("https://localhost:7037/api/HeatingTypeApi/GetHeatingTypes");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }
            jsonData = await responseMessage.Content.ReadAsStringAsync();
            var heatingTypes = JsonConvert.DeserializeObject<List<ResultHeatingTypeDto>>(jsonData);
            var heatingTypeSelectList = heatingTypes.Select(ht => new SelectListItem
            {
                Value = ht.HeatingTypeId.ToString(),
                Text = ht.Name
            }).ToList();
            ViewBag.HeatingTypes = heatingTypeSelectList;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UserCreateHouse(CreateHouseDto createHouseDto)
        {
            createHouseDto.OwnerId = ViewBag.UserId;
            createHouseDto.DistrictId = 1;

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
        public async Task<IActionResult> UserUpdateHouse(int id)
        {
            var client = _httpClientFactory.CreateClient();

            // Mevcut evi getir
            var responseMessage = await client.GetAsync($"https://localhost:7037/api/House/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var house = JsonConvert.DeserializeObject<UpdateHouseDto>(jsonData);

            // Ev Türlerini Getir
            responseMessage = await client.GetAsync("https://localhost:7037/api/HouseType");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            jsonData = await responseMessage.Content.ReadAsStringAsync();
            var houseTypes = JsonConvert.DeserializeObject<List<ResultHouseTypeDto>>(jsonData);

            var houseTypeSelectList = houseTypes.Select(ht => new SelectListItem
            {
                Value = ht.Id.ToString(),
                Text = ht.Name
            }).ToList();

            ViewBag.HouseTypes = houseTypeSelectList;

            // Kullanıcıları Getir
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

            ViewBag.Users = userSelectList;

            // Isıtma Türlerini Getir
            responseMessage = await client.GetAsync("https://localhost:7037/api/HeatingTypeApi/GetHeatingTypes");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            jsonData = await responseMessage.Content.ReadAsStringAsync();
            var heatingTypes = JsonConvert.DeserializeObject<List<ResultHeatingTypeDto>>(jsonData);

            var heatingTypeSelectList = heatingTypes.Select(ht => new SelectListItem
            {
                Value = ht.HeatingTypeId.ToString(),
                Text = ht.Name
            }).ToList();

            ViewBag.HeatingTypes = heatingTypeSelectList;

            return View(house);
        }


        [HttpPost]
        public async Task<IActionResult> UserUpdateHouse(UpdateHouseDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.DistrictId = 1;
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        
            var responseMessage = await client.PutAsync("https://localhost:7037/api/House", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ListHouses");
            }

            // Ev Türlerini tekrar yükle
            responseMessage = await client.GetAsync("https://localhost:7037/api/HouseType");
            if (responseMessage.IsSuccessStatusCode)
            {
                jsonData = await responseMessage.Content.ReadAsStringAsync();
                var houseTypes = JsonConvert.DeserializeObject<List<ResultHouseTypeDto>>(jsonData);
                var houseTypeSelectList = houseTypes.Select(ht => new SelectListItem
                {
                    Value = ht.Id.ToString(),
                    Text = ht.Name
                }).ToList();
                ViewBag.HouseTypes = houseTypeSelectList;
            }

            // Kullanıcıları tekrar yükle
            responseMessage = await client.GetAsync("https://localhost:7037/api/AppUser");
            if (responseMessage.IsSuccessStatusCode)
            {
                jsonData = await responseMessage.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<ResultUserDto>>(jsonData);
                var userSelectList = users.Select(u => new SelectListItem
                {
                    Value = u.UserId.ToString(),
                    Text = u.Username
                }).ToList();
                ViewBag.Users = userSelectList;
            }

            // Isıtma Türlerini tekrar yükle
            responseMessage = await client.GetAsync("https://localhost:7037/api/HeatingTypeApi/GetHeatingTypes");
            if (responseMessage.IsSuccessStatusCode)
            {
                jsonData = await responseMessage.Content.ReadAsStringAsync();
                var heatingTypes = JsonConvert.DeserializeObject<List<ResultHeatingTypeDto>>(jsonData);
                var heatingTypeSelectList = heatingTypes.Select(ht => new SelectListItem
                {
                    Value = ht.HeatingTypeId.ToString(),
                    Text = ht.Name
                }).ToList();
                ViewBag.HeatingTypes = heatingTypeSelectList;
            }

            return View(model);
        }


        public async Task<IActionResult> UserDeleteHouse(int id)
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
