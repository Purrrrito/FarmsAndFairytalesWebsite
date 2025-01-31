using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace FarmsAndFairytalesWebsite.Models
{
	public class IndoorBookedTimeSlots 
	{
		[Key]
		public int BookedTimeSlotId { get; set; }

		public DateTime Start { get; set; }

		public DateTime End { get; set; }

		public IdentityUser? Photographer { get; set; }

		public bool MilestoneShoot { get; set; }
	}
}