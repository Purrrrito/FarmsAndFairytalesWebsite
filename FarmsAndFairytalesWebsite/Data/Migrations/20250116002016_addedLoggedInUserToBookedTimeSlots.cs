using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmsAndFairytalesWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedLoggedInUserToBookedTimeSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotographerId",
                table: "BookedTimeSlots",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookedTimeSlots_PhotographerId",
                table: "BookedTimeSlots",
                column: "PhotographerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookedTimeSlots_AspNetUsers_PhotographerId",
                table: "BookedTimeSlots",
                column: "PhotographerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookedTimeSlots_AspNetUsers_PhotographerId",
                table: "BookedTimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_BookedTimeSlots_PhotographerId",
                table: "BookedTimeSlots");

            migrationBuilder.DropColumn(
                name: "PhotographerId",
                table: "BookedTimeSlots");
        }
    }
}
