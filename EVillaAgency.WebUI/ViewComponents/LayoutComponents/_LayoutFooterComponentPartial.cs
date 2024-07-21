using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebUI.ViewComponents.LayoutComponents
{
	public class _LayoutFooterComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
