﻿using Microsoft.AspNetCore.Mvc;

namespace EVillaAgency.WebUI.ViewComponents.LayoutComponents
{
	public class _LayoutNavbarComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
