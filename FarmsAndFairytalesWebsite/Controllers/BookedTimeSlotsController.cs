using FarmsAndFairytalesWebsite.Data;
using FarmsAndFairytalesWebsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FarmsAndFairytalesWebsite.Controllers
{
	public class BookedTimeSlotsController : Controller
	{
		private readonly ApplicationDbContext _context;
		public BookedTimeSlotsController(ApplicationDbContext context)
		{
			_context = context;
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
		public JsonResult CheckAndBookSlot([FromBody] BookedTimeSlots slots)
		{
			bool isBooked = _context.BookedTimeSlots.Any(b =>
				(slots.Start >= b.Start && slots.Start < b.End) ||
				(slots.End > b.Start && slots.End <= b.End)
			);

			if (!isBooked)
			{
				 Console.WriteLine("Controller" + slots);
				_context.BookedTimeSlots.Add(slots);
				_context.SaveChanges();
			}

			return Json(new { isBooked });
		}
	}
}
