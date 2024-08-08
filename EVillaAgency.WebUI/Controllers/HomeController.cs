
using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.EntityLayer.Concrete;
using EVillaAgency.WebUI.Dtos.FavoriteDtos;
using EVillaAgency.WebUI.Dtos.HouseDtos;
using EVillaAgency.WebUI.Dtos.IdentityDtos;
using EVillaAgency.WebUI.Dtos.UserDtos;
using EVillaAgency.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class HomeController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(IHttpClientFactory httpClientFactory, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAppUserService appUserService)
        {
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
            _signInManager = signInManager;
            _appUserService = appUserService;
        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var houseViewModel = new IndexHouseFavoriteModel();

            // Get Last 6 Houses
            var responseMessage = await client.GetAsync("https://localhost:7037/api/House/GetLast6ActiveHouses");
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
                var houseDetail = JsonConvert.DeserializeObject<ResultHousesWithNamesDto>(jsonData);
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
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new AppUser()
            {
                Name = dto.Name,
                Email = dto.Email,
                UserName = dto.UserName,
                PhoneNumber = dto.PhoneNumber,
                ImageUrl = dto.ImageUrl,
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }

        }

        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(dto.UserName);

                if (user != null)
                {
                    // Kullanıcının ID ve Adını oturuma kaydet
                    HttpContext.Session.SetInt32("Id", user.Id);
                    HttpContext.Session.SetString("Name", user.Name);
                    return RedirectToAction("Index");
                }
            }
            // Başarısız giriş
            ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
            return View();
        }

        // GET: /Account/Logout
        [HttpGet]
        public IActionResult Logout()
        {
            // Session'daki tüm verileri temizleyin
            HttpContext.Session.Clear();

            // Kullanıcıyı giriş sayfasına yönlendirin
            return RedirectToAction("Index", "Home");
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
