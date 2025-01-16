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
		public async Task<IActionResult> IndoorBookingConfirmation(int? id, DateTime start, DateTime end)
		{
			if (id == null)
			{
				return NotFound();
			}

			// By default EF Core does not automatically load related entitys, so we have use .include to get the related Photographer
			var @timeSlot = await _context.BookedTimeSlots
				.Include(e => e.Photographer)
				.FirstOrDefaultAsync(m => m.BookedTimeSlotId == id);
			string? currentUserId = _userManager.GetUserId(User);

			// Check to see if the user logged in is the same user that booked this slot
			if (currentUserId == null || @timeSlot?.Photographer?.Id != currentUserId)
			{
				return Forbid();
			}
			if (@timeSlot == null)
			{
				return NotFound();
			}

			return View(@timeSlot);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CancelTimeSlot(int id)
		{
			var @timeSlot = await _context.BookedTimeSlots.FindAsync(id);
			if (@timeSlot != null)
			{
				_context.BookedTimeSlots.Remove(@timeSlot);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool EventExists(int id)
		{
			return _context.BookedTimeSlots.Any(e => e.BookedTimeSlotId == id);
		}
	}
}
