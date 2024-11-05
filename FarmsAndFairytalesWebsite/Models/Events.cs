using System.ComponentModel.DataAnnotations;

namespace FarmsAndFairytalesWebsite.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Display(Name = "Date of the Event")]
        public DateTime DateOfEvent { get; set; }

        [Display(Name = "Event's name")]
        public string EventName { get; set; }

        [StringLength(300, ErrorMessage = "The description cannot be longer than 300 characters.")]
        [Display(Name = "Event description")]
        public string Description { get; set; }

        [Display(Name = "Photographer's name")]
        public string PhotographerHost { get; set; }
    }
}
