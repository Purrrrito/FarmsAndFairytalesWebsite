using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FarmsAndFairytalesWebsite.Models
{
	public enum BookingType
	{
		General,
		Boudoir,
		Milestone
	}

	public class BookedTimeSlots
	{
		[Key]
		public int BookedTimeSlotId { get; set; }

		[Required]
		public DateTime StartTime { get; set; }

		[Required]
		public DateTime EndTime { get; set; }

		[Required]
		public bool IsOutdoor { get; set; } // True for outdoor, false for indoor

		public BookingType Type { get; set; } // General, Boudoir, Milestone

		public IdentityUser? Photographer { get; set; }
	}
}
