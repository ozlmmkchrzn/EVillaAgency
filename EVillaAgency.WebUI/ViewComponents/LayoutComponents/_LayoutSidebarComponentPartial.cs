using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebUI.ViewComponents.LayoutComponents
{
	public class _LayoutSidebarComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
