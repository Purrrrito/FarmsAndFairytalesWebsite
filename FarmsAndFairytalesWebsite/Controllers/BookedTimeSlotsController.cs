﻿using FarmsAndFairytalesWebsite.Data;
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

		public JsonResult CheckSlot([FromBody] BookedTimeSlots @slots)
		{
			bool isBooked = _context.BookedTimeSlots.Any(b =>
				(@slots.Start < b.End && @slots.End > b.Start)
			);

			return Json(new { isBooked, bookedTimeSlotsId = slots.BookedTimeSlotId });
		}

		public async Task<IActionResult> BookSlot([FromBody] BookedTimeSlots @slots)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Unauthorized();
			}
			_context.BookedTimeSlots.Add(new BookedTimeSlots
			{
				Start = @slots.Start,
				End = @slots.End,
				Photographer = user
			});
			await _context.SaveChangesAsync();
			return Ok();
		}
	}
}
