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
			var bookedTimeSlots = _context.IndoorBookedTimeSlots.Select(b => new
			{
				start = b.IndoorStart.ToString("yyyy-MM-ddTHH:mm:ss"),
				end = b.IndoorEnd.ToString("yyyy-MM-ddTHH:mm:ss"),
			}).ToList();

			return Json(bookedTimeSlots);
		}

		public JsonResult CheckSlot([FromBody] IndoorBookedTimeSlots @slots)
		{
			bool isBooked = _context.IndoorBookedTimeSlots.Any(b =>
				(@slots.IndoorStart < b.IndoorEnd && @slots.IndoorEnd > b.IndoorStart)
			);

			return Json(new { isBooked, bookedTimeSlotsId = slots.IndoorBookedTimeSlotId });
		}

		public async Task<IActionResult> BookSlot([FromBody] IndoorBookedTimeSlots @slots)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Unauthorized();
			}
			_context.IndoorBookedTimeSlots.Add(new IndoorBookedTimeSlots
			{
				IndoorStart = @slots.IndoorStart,
				IndoorEnd = @slots.IndoorEnd,
				IndoorPhotographer = user,
				IndoorMilestoneCompleted = slots.IndoorMilestoneCompleted
			});
			await _context.SaveChangesAsync();
			return Ok();
		}
	}
}
