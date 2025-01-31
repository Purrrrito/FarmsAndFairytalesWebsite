using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace FarmsAndFairytalesWebsite.Models
{
	public class IndoorBookedTimeSlots 
	{
		[Key]
		public int IndoorBookedTimeSlotId { get; set; }

		public DateTime IndoorStart { get; set; }

		public DateTime IndoorEnd { get; set; }

		public IdentityUser? IndoorPhotographer { get; set; }

		public bool IndoorMilestoneCompleted { get; set; }
	}
}