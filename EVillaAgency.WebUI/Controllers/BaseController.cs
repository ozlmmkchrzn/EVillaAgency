using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EVillaAgency.WebUI.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            ViewBag.Id = HttpContext.Session.GetInt32("Id");
            ViewBag.Name = HttpContext.Session.GetString("Name");
        }
    }
}
