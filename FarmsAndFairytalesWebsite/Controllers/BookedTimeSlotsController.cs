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

		// INDOOR METHODS

		/// <summary>
		/// Retrieves all booked time slots.
		/// </summary>
		[HttpGet]
		public async Task<JsonResult> GetIndoorBookedTimeSlots()
		{
			var bookedTimeSlots = await _context.IndoorBookedTimeSlots
				.Select(b => new { start = b.IndoorStart.ToString("yyyy-MM-ddTHH:mm:ss"), end = b.IndoorEnd.ToString("yyyy-MM-ddTHH:mm:ss") })
				.ToListAsync();

			return Json(bookedTimeSlots);
		}

		/// <summary>
		/// Checks if a requested time slot is already booked.
		/// </summary>
		[HttpPost]
		public async Task<JsonResult> CheckIndoorSlot([FromBody] IndoorBookedTimeSlots slots)
		{
			bool isBooked = await _context.IndoorBookedTimeSlots
				.AnyAsync(b => slots.IndoorStart < b.IndoorEnd && slots.IndoorEnd > b.IndoorStart);

			return Json(new { isBooked, bookedTimeSlotsId = slots.IndoorBookedTimeSlotId });
		}

		/// <summary>
		/// Books a new time slot if available.
		/// </summary>
		[HttpPost]
		public async Task<IActionResult> BookIndoorSlot([FromBody] IndoorBookedTimeSlots @slots)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Unauthorized();
			}

			var newBooking = new IndoorBookedTimeSlots
			{
				IndoorStart = slots.IndoorStart,
				IndoorEnd = slots.IndoorEnd,
				IndoorPhotographer = user,
				IndoorMilestoneShoot = slots.IndoorMilestoneShoot
			};

			_context.IndoorBookedTimeSlots.Add(newBooking);
			await _context.SaveChangesAsync();

			return Ok();
		}


		// OUTDOOR METHODS

		/// <summary>
		/// Retrieves all booked time slots.
		/// </summary>
		[HttpGet]
		public async Task<JsonResult> GetOutdoorBookedTimeSlots()
		{
			var bookedTimeSlots = await _context.OutdoorBookedTimeSlots
				.Select(b => new { start = b.OutdoorStart.ToString("yyyy-MM-ddTHH:mm:ss"), end = b.OutdoorEnd.ToString("yyyy-MM-ddTHH:mm:ss") })
				.ToListAsync();

			return Json(bookedTimeSlots);
		}

		/// <summary>
		/// Checks if a requested time slot is already booked.
		/// </summary>
		[HttpPost]
		public async Task<JsonResult> CheckOutdoorSlot([FromBody] OutdoorBookedTimeSlots slots)
		{
			bool isBooked = await _context.OutdoorBookedTimeSlots
				.AnyAsync(b => slots.OutdoorStart < b.OutdoorEnd && slots.OutdoorEnd > b.OutdoorStart);

			return Json(new { isBooked, bookedTimeSlotsId = slots.OutdoorBookedTimeSlotId });
		}

		/// <summary>
		/// Books a new time slot if available.
		/// </summary>
		[HttpPost]
		public async Task<IActionResult> BookOutdoorSlot([FromBody] OutdoorBookedTimeSlots @slots)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Unauthorized();
			}

			var newBooking = new OutdoorBookedTimeSlots
			{
				OutdoorStart = slots.OutdoorStart,
				OutdoorEnd = slots.OutdoorEnd,
				OutdoorPhotographer = user,
				OutdoorBoudoirShoot = slots.OutdoorBoudoirShoot
			};

			_context.OutdoorBookedTimeSlots.Add(newBooking);
			await _context.SaveChangesAsync();

			return Ok();
		}
	}
}
