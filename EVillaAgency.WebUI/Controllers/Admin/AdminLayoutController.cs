using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebUI.Controllers.Admin
{
    public class AdminLayoutController : Controller
    {

		public IActionResult _AdminLayout()
		{
			return View();
		}


		public IActionResult Index()
        {
            return View();
        }


        public IActionResult Index1()
        {
            return View();
        }
    }
}
