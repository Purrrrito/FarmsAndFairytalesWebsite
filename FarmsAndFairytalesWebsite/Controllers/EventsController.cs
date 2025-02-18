﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FarmsAndFairytalesWebsite.Data;
using FarmsAndFairytalesWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FarmsAndFairytalesWebsite.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
		private readonly UserManager<IdentityUser> _userManager;
		public EventsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
			_userManager = userManager;
		}

        // GET: Events
        public async Task<IActionResult> Index()
        {
            return View(await _context.Event.ToListAsync());
        }

        // GET: Events/Details/*id of selected event*
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        [Authorize(Roles = "Photographer")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("EventId,DateOfEvent,EventName,Description,PhotographerHost,ContactInfo")] Event @event, string EventType)
		{
			if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                // Assigns the currently logged in photographer to the event
                @event.Photographer = user;

				// Automatically set start and end times for the whole day
				DateTime startTime = @event.DateOfEvent.Date; // Midnight (00:00)
				DateTime endTime = startTime.AddDays(1).AddTicks(-1); // 23:59:59

				if (EventType == "Indoor")
				{
					@event.IndoorEventTimeSlots = new IndoorBookedTimeSlots
					{
						IndoorStart = startTime,
						IndoorEnd = endTime,
						IndoorPhotographer = user,
						IndoorMilestoneShoot = false
					};
				}
				else if (EventType == "Outdoor")
				{
					@event.OutdoorEventTimeSlots = new OutdoorBookedTimeSlots
					{
						OutdoorStart = startTime,
						OutdoorEnd = endTime,
						OutdoorPhotographer = user,
						OutdoorBoudoirShoot = false
					};
				}

				_context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

		// GET: Events/Edit/*id of selected event*
		[Authorize(Roles = "Photographer")]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // By default EF Core does not automatically load related entitys, so we have use .include to get the related Photographer
			var @event = await _context.Event
                .Include(e => e.Photographer)
                .FirstOrDefaultAsync(e => e.EventId == id);

			if (@event == null)
            {
                return NotFound();
            }

            // Check to see if the user logged in is the same user that created the event
			string? currentUserId = _userManager.GetUserId(User);
			if (currentUserId == null || @event.Photographer?.Id != currentUserId)
			{
				return Forbid();
			}

			return View(@event);
        }

		// POST: Events/Edit/*id of selected event*
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,DateOfEvent,EventName,Description,PhotographerHost,ContactInfo")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

			if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

		// GET: Events/Delete/*id of selected event*
		[Authorize(Roles = "Photographer")]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			// By default EF Core does not automatically load related entitys, so we have use .include to get the related Photographer
			var @event = await _context.Event
				.Include(e => e.Photographer)
				.FirstOrDefaultAsync(e => e.EventId == id);

			// Check to see if the user logged in is the same user that created the event
			string? currentUserId = _userManager.GetUserId(User);
			if (currentUserId == null || @event?.Photographer?.Id != currentUserId)
			{
				return Forbid();
			}
			if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

		// POST: Events/Delete/*id of selected event*
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var @event = await _context.Event
				.Include(e => e.IndoorEventTimeSlots)
				.Include(e => e.OutdoorEventTimeSlots)
				.FirstOrDefaultAsync(e => e.EventId == id);

			if (@event != null)
			{
				// Remove associated Indoor and Outdoor Time Slots
				if (@event.IndoorEventTimeSlots != null)
				{
					_context.IndoorBookedTimeSlots.Remove(@event.IndoorEventTimeSlots);
				}

				if (@event.OutdoorEventTimeSlots != null)
				{
					_context.OutdoorBookedTimeSlots.Remove(@event.OutdoorEventTimeSlots);
				}

				// Remove the event itself
				_context.Event.Remove(@event);
				await _context.SaveChangesAsync();
			}

			return RedirectToAction(nameof(Index));
		}

		private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.EventId == id);
        }
    }
}
