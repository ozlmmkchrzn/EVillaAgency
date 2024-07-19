using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.EntityLayer.Concrete;
using EVillaAgency.WebUI.Dtos.HouseDtos;
using EVillaAgency.WebUI.Dtos.HouseTypeDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

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

            // Get House Types
            var responseMessage = await client.GetAsync("https://localhost:7037/api/HouseType");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return View();
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var houseTypes = JsonConvert.DeserializeObject<List<ResultHouseTypeDto>>(jsonData);

            // Create SelectListItem list for dropdown
            var houseTypeSelectList = houseTypes.Select(ht => new SelectListItem
            {
                Value = ht.Id.ToString(),
                Text = ht.Name
            }).ToList();

            ViewBag.HouseTypes = houseTypeSelectList;


            // Get Houses by HouseTypeId
            if (houseTypeId.HasValue)
            {
                responseMessage = await client.GetAsync($"https://localhost:7037/api/House/GetHousesByHouseTypeId?id={houseTypeId}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var houses = JsonConvert.DeserializeObject<List<ResultHousesWithNamesDto>>(jsonData);
                    return View(houses);
                }
            }

            return View(new List<ResultHousesWithNamesDto>());
        }
    }
}
