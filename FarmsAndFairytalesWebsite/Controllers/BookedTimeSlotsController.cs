using FarmsAndFairytalesWebsite.Data;
using FarmsAndFairytalesWebsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FarmsAndFairytalesWebsite.Controllers
{
	public class BookedTimeSlotsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<IdentityUser> _userManager;
		public BookedTimeSlotsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		[HttpGet]
		public JsonResult GetBookedTimeSlots()
		{
			var bookedTimeSlots = _context.BookedTimeSlots.Select(b => new
			{
				start = b.Start.ToString("yyyy-MM-ddTHH:mm:ss"),
				end = b.End.ToString("yyyy-MM-ddTHH:mm:ss"),
			}).ToList();

			return Json(bookedTimeSlots);
		}

		[HttpPost]
		public async Task<JsonResult> CheckAndBookSlot([FromBody] BookedTimeSlots @slots)
		{
			bool isBooked = _context.BookedTimeSlots.Any(b =>
				(@slots.Start < b.End && @slots.End > b.Start)
			);

			if (!isBooked)
			{
				var user = await _userManager.GetUserAsync(User);
				// Assigns the currently logged in photographer to the event
				@slots.Photographer = user;

				await _context.BookedTimeSlots.AddAsync(@slots);
				await _context.SaveChangesAsync();
			}

			return Json(new { isBooked, bookedTimeSlotsId = slots.BookedTimeSlotId });
		}
	}
}
