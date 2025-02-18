using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmsAndFairytalesWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class IndoorOrOutdoorAddedToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IndoorEventTimeSlotsIndoorBookedTimeSlotId",
                table: "Event",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OutdoorEventTimeSlotsOutdoorBookedTimeSlotId",
                table: "Event",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Event_IndoorEventTimeSlotsIndoorBookedTimeSlotId",
                table: "Event",
                column: "IndoorEventTimeSlotsIndoorBookedTimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_OutdoorEventTimeSlotsOutdoorBookedTimeSlotId",
                table: "Event",
                column: "OutdoorEventTimeSlotsOutdoorBookedTimeSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_IndoorBookedTimeSlots_IndoorEventTimeSlotsIndoorBookedTimeSlotId",
                table: "Event",
                column: "IndoorEventTimeSlotsIndoorBookedTimeSlotId",
                principalTable: "IndoorBookedTimeSlots",
                principalColumn: "IndoorBookedTimeSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_OutdoorBookedTimeSlots_OutdoorEventTimeSlotsOutdoorBookedTimeSlotId",
                table: "Event",
                column: "OutdoorEventTimeSlotsOutdoorBookedTimeSlotId",
                principalTable: "OutdoorBookedTimeSlots",
                principalColumn: "OutdoorBookedTimeSlotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_IndoorBookedTimeSlots_IndoorEventTimeSlotsIndoorBookedTimeSlotId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_OutdoorBookedTimeSlots_OutdoorEventTimeSlotsOutdoorBookedTimeSlotId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_IndoorEventTimeSlotsIndoorBookedTimeSlotId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_OutdoorEventTimeSlotsOutdoorBookedTimeSlotId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "IndoorEventTimeSlotsIndoorBookedTimeSlotId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "OutdoorEventTimeSlotsOutdoorBookedTimeSlotId",
                table: "Event");
        }
    }
}
