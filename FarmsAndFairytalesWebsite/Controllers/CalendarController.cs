using Microsoft.AspNetCore.Mvc;

namespace FarmsAndFairytalesWebsite.Controllers
{
	public class CalendarController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Indoor()
		{
			return View();
		}

	}
}
