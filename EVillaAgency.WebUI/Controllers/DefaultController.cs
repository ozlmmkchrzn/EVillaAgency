using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
