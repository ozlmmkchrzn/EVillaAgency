using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebUI.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            ViewBag.Username = HttpContext.Session.GetString("Username");

            return View();
        }
    }
}
