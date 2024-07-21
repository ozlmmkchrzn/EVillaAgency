using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebUI.Controllers.Admin
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		public IActionResult _AdminLayout()
		{
			return View();
		}

	}
}
