using Microsoft.AspNetCore.Mvc;

namespace FarmsAndFairytalesWebsite.Controllers
{
	public class SetsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
