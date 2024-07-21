using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebUI.ViewComponents.LayoutComponents
{
	public class _LayoutHeaderPartialComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
