using Microsoft.AspNetCore.Mvc;

namespace CreditMe.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		

	}
}
