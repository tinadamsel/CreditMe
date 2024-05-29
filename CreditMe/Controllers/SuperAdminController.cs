using Microsoft.AspNetCore.Mvc;

namespace CreditMe.Controllers
{
	public class SuperAdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
