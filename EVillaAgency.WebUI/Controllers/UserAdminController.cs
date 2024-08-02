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
        public async Task<IActionResult> UserCreateHouse()
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
        public async Task<IActionResult> UserCreateHouse(CreateHouseDto createHouseDto)
        {
            createHouseDto.OwnerId = ViewBag.UserId;

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



    }
}
