using FarmsAndFairytalesWebsite.Data;
using FarmsAndFairytalesWebsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FarmsAndFairytalesWebsite.Controllers
{
	public class BookedTimeSlotsController : Controller
	{
		/// <summary>
		/// Controller for handling booked time slots.
		/// </summary>
		private readonly ApplicationDbContext _context;
		private readonly UserManager<IdentityUser> _userManager;
		public BookedTimeSlotsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		/// <summary>
		/// Retrieves all booked time slots.
		/// </summary>
		[HttpGet]
		public async Task<JsonResult> GetBookedTimeSlots(bool isOutdoor)
		{
			var bookedTimeSlots = await _context.BookedTimeSlots
				.Where(b => b.IsOutdoor == isOutdoor)
				.Select(b => new { start = b.StartTime.ToString("yyyy-MM-ddTHH:mm:ss"), end = b.EndTime.ToString("yyyy-MM-ddTHH:mm:ss") })
				.ToListAsync();

			return Json(bookedTimeSlots);
		}

		/// <summary>
		/// Checks if a requested time slot is already booked.
		/// </summary>
		[HttpPost]
		public async Task<JsonResult> CheckSlot([FromBody] BookedTimeSlots slots)
		{
			bool isOutdoor = slots.IsOutdoor; // Assuming `IsOutdoor` exists in the model

			bool isBooked = await _context.BookedTimeSlots
				.AnyAsync(b =>
					slots.StartTime < b.EndTime &&
					slots.EndTime > b.StartTime &&
					(b.IsOutdoor && isOutdoor) // Only block if both bookings are outdoor
				);

			return Json(new { isBooked, bookedTimeSlotsId = slots.BookedTimeSlotId });
		}

		/// <summary>
		/// Books a new time slot if available.
		/// </summary>
		[HttpPost]
		public async Task<IActionResult> BookSlot([FromBody] BookedTimeSlots slots)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Unauthorized();
			}

			var newBooking = new BookedTimeSlots
			{
				StartTime = slots.StartTime,
				EndTime = slots.EndTime,
				IsOutdoor = slots.IsOutdoor,
				Photographer = user,
				Type = slots.Type
			};

			_context.BookedTimeSlots.Add(newBooking);
			await _context.SaveChangesAsync();

			return Ok();
		}
	}
}
