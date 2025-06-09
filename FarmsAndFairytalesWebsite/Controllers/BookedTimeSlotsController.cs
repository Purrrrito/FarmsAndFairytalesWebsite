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
		private readonly UserManager<ApplicationUser> _userManager;
		public BookedTimeSlotsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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
			bool isOutdoor = slots.IsOutdoor;

			bool isBooked = await _context.BookedTimeSlots
				.AnyAsync(b =>
					b.IsOutdoor == isOutdoor && // Only compare with same type, either indoor or outdoor bookings
					slots.StartTime < b.EndTime &&
					slots.EndTime > b.StartTime
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
				Photographer = (ApplicationUser)user,
				Type = slots.Type
			};

			_context.BookedTimeSlots.Add(newBooking);
			await _context.SaveChangesAsync();

			return Ok();
		}
	}
}
