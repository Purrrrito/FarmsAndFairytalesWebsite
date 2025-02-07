using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmsAndFairytalesWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNamingOfMilestoneShoot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IndoorMilestoneCompleted",
                table: "IndoorBookedTimeSlots",
                newName: "IndoorMilestoneShoot");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IndoorMilestoneShoot",
                table: "IndoorBookedTimeSlots",
                newName: "IndoorMilestoneCompleted");
        }
    }
}
