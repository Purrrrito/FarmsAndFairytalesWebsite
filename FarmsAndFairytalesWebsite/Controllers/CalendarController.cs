using FarmsAndFairytalesWebsite.Data;
using FarmsAndFairytalesWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FarmsAndFairytalesWebsite.Controllers
{
	public class CalendarController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<IdentityUser> _userManager;
		public CalendarController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		[Authorize(Roles = "Photographer")]
		public IActionResult Indoor()
		{
			return View();
		}

		[Authorize(Roles = "Photographer")]
		public IActionResult Outdoor()
		{
			return View();
		}
	}
}
