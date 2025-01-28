using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FarmsAndFairytalesWebsite.Models;

namespace FarmsAndFairytalesWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FarmsAndFairytalesWebsite.Models.Event> Event { get; set; } = default!;
        public DbSet<FarmsAndFairytalesWebsite.Models.IndoorBookedTimeSlots> BookedTimeSlots { get; set; } = default!;
    }
}
