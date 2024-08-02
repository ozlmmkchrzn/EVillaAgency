using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebUI.Controllers
{
    public class UserAdminController : Controller
    {

        public IActionResult _UserAdminLayout()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
