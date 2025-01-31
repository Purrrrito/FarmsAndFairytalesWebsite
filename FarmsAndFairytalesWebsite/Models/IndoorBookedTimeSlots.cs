using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FarmsAndFairytalesWebsite.Models
{
	/// <summary>
	/// Represents a booked time slot for indoor photography sessions.
	/// </summary>
	public class IndoorBookedTimeSlots
	{
		/// <summary>
		/// Unique identifier for the booked time slot.
		/// </summary>
		[Key]
		public int IndoorBookedTimeSlotId { get; set; }

		/// <summary>
		/// Start time of the booking.
		/// </summary>
		[Required]
		public DateTime IndoorStart { get; set; }

		/// <summary>
		/// End time of the booking.
		/// </summary>
		[Required]
		public DateTime IndoorEnd { get; set; }

		/// <summary>
		/// The photographer who booked the time slot.
		/// </summary>
		public IdentityUser? IndoorPhotographer { get; set; }

		/// <summary>
		/// Indicates whether the milestone has been completed during the session.
		/// </summary>
		public bool IndoorMilestoneCompleted { get; set; }
	}
}
