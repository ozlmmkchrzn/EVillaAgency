using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebUI.ViewComponents.LayoutComponents
{
	public class _LayoutScriptComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
