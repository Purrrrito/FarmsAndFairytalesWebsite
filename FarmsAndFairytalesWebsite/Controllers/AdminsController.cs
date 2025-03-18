using FarmsAndFairytalesWebsite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FarmsAndFairytalesWebsite.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminsController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ApplicationDbContext _context;

		public AdminsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
		{
			_userManager = userManager;
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var unapprovedUsers = await _context.Users.Where(u => !u.IsApproved).ToListAsync();
			return View(unapprovedUsers);
		}

		[HttpPost]
		public async Task<IActionResult> ApproveUser(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null) return NotFound();

			user.IsApproved = true;
			await _userManager.UpdateAsync(user);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> DenyUser(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null) return NotFound();

			await _userManager.DeleteAsync(user);

			return RedirectToAction("Index");
		}
	}
}
