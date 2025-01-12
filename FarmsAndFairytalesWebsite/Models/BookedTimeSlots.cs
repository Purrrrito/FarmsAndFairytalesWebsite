using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace FarmsAndFairytalesWebsite.Models
{
	public class BookedTimeSlots 
	{
		[Key]
		public int BookedTimeSlotId { get; set; }

		public DateTime Start { get; set; }

		public DateTime End { get; set; }

		[MaxLength(7)] // Hex code length
		public string Color { get; set; } = "#FF0000"; // Default color is red
	}
}


    