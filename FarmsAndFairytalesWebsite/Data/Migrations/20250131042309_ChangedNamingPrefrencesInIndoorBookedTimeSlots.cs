using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmsAndFairytalesWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNamingPrefrencesInIndoorBookedTimeSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookedTimeSlots_AspNetUsers_PhotographerId",
                table: "BookedTimeSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookedTimeSlots",
                table: "BookedTimeSlots");

            migrationBuilder.RenameTable(
                name: "BookedTimeSlots",
                newName: "IndoorBookedTimeSlots");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "IndoorBookedTimeSlots",
                newName: "IndoorStart");

            migrationBuilder.RenameColumn(
                name: "PhotographerId",
                table: "IndoorBookedTimeSlots",
                newName: "IndoorPhotographerId");

            migrationBuilder.RenameColumn(
                name: "MilestoneShoot",
                table: "IndoorBookedTimeSlots",
                newName: "IndoorMilestoneCompleted");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "IndoorBookedTimeSlots",
                newName: "IndoorEnd");

            migrationBuilder.RenameColumn(
                name: "BookedTimeSlotId",
                table: "IndoorBookedTimeSlots",
                newName: "IndoorBookedTimeSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_BookedTimeSlots_PhotographerId",
                table: "IndoorBookedTimeSlots",
                newName: "IX_IndoorBookedTimeSlots_IndoorPhotographerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndoorBookedTimeSlots",
                table: "IndoorBookedTimeSlots",
                column: "IndoorBookedTimeSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_IndoorBookedTimeSlots_AspNetUsers_IndoorPhotographerId",
                table: "IndoorBookedTimeSlots",
                column: "IndoorPhotographerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndoorBookedTimeSlots_AspNetUsers_IndoorPhotographerId",
                table: "IndoorBookedTimeSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndoorBookedTimeSlots",
                table: "IndoorBookedTimeSlots");

            migrationBuilder.RenameTable(
                name: "IndoorBookedTimeSlots",
                newName: "BookedTimeSlots");

            migrationBuilder.RenameColumn(
                name: "IndoorStart",
                table: "BookedTimeSlots",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "IndoorPhotographerId",
                table: "BookedTimeSlots",
                newName: "PhotographerId");

            migrationBuilder.RenameColumn(
                name: "IndoorMilestoneCompleted",
                table: "BookedTimeSlots",
                newName: "MilestoneShoot");

            migrationBuilder.RenameColumn(
                name: "IndoorEnd",
                table: "BookedTimeSlots",
                newName: "End");

            migrationBuilder.RenameColumn(
                name: "IndoorBookedTimeSlotId",
                table: "BookedTimeSlots",
                newName: "BookedTimeSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_IndoorBookedTimeSlots_IndoorPhotographerId",
                table: "BookedTimeSlots",
                newName: "IX_BookedTimeSlots_PhotographerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookedTimeSlots",
                table: "BookedTimeSlots",
                column: "BookedTimeSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookedTimeSlots_AspNetUsers_PhotographerId",
                table: "BookedTimeSlots",
                column: "PhotographerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
