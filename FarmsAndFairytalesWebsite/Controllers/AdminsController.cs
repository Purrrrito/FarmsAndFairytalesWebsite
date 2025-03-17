using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmsAndFairytalesWebsite.Controllers
{
	public class AdminsController : Controller
	{
		[Authorize (Roles = "Admin")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
