using Microsoft.AspNetCore.Mvc;

namespace FarmsAndFairytalesWebsite.Controllers
{
	public class InfoController : Controller
	{
		public IActionResult Contact()
		{
			return View();
		}

		public IActionResult FAQ()
		{
			return View();
		}
	}
}
