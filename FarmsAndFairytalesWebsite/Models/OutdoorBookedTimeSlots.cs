using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FarmsAndFairytalesWebsite.Models
{
	/// <summary>
	/// Represents a booked time slot for outdoor photography sessions.
	/// </summary>
	public class OutdoorBookedTimeSlots
	{
		/// <summary>
		/// Unique identifier for the booked time slot.
		/// </summary>
		[Key]
		public int OutdoorBookedTimeSlotId { get; set; }

		/// <summary>
		/// Start time of the booking.
		/// </summary>
		[Required]
		public DateTime OutdoorStart { get; set; }

		/// <summary>
		/// End time of the booking.
		/// </summary>
		[Required]
		public DateTime OutdoorEnd { get; set; }

		/// <summary>
		/// The photographer who booked the time slot.
		/// </summary>
		public IdentityUser? OutdoorPhotographer { get; set; }

		/// <summary>
		/// Indicates whether the session will be a boudoir shoot.
		/// </summary>
		public bool OutdoorBoudoirShoot { get; set; }
	}
}
