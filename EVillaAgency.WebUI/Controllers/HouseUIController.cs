using EVillaAgency.DataAccessLayer.Context;
using EVillaAgency.EntityLayer.Concrete;
using EVillaAgency.WebUI.Dtos.HouseDtos;
using EVillaAgency.WebUI.Dtos.HouseTypeDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using X.PagedList;
using X.PagedList.Extensions;

namespace EVillaAgency.WebUI.Controllers
{
    public class HouseUIController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HouseUIController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

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
                : await client.GetAsync("https://localhost:7037/api/House/GetHousesWithNames");

            if (responseMessage.IsSuccessStatusCode)
            {
                jsonData = await responseMessage.Content.ReadAsStringAsync();
                var houses = JsonConvert.DeserializeObject<List<ResultHousesWithNamesDto>>(jsonData);
                return View(houses);
            }

            return View(new List<ResultHousesWithNamesDto>());
        }
        
    }
}
