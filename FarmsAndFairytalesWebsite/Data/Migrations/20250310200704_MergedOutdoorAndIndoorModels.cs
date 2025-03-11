using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmsAndFairytalesWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class MergedOutdoorAndIndoorModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_IndoorBookedTimeSlots_IndoorEventTimeSlotsIndoorBookedTimeSlotId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_OutdoorBookedTimeSlots_OutdoorEventTimeSlotsOutdoorBookedTimeSlotId",
                table: "Event");

            migrationBuilder.DropTable(
                name: "IndoorBookedTimeSlots");

            migrationBuilder.DropTable(
                name: "OutdoorBookedTimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_Event_IndoorEventTimeSlotsIndoorBookedTimeSlotId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "IndoorEventTimeSlotsIndoorBookedTimeSlotId",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "OutdoorEventTimeSlotsOutdoorBookedTimeSlotId",
                table: "Event",
                newName: "EventTimeSlotBookedTimeSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_OutdoorEventTimeSlotsOutdoorBookedTimeSlotId",
                table: "Event",
                newName: "IX_Event_EventTimeSlotBookedTimeSlotId");

            migrationBuilder.CreateTable(
                name: "BookedTimeSlots",
                columns: table => new
                {
                    BookedTimeSlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsOutdoor = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PhotographerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookedTimeSlots", x => x.BookedTimeSlotId);
                    table.ForeignKey(
                        name: "FK_BookedTimeSlots_AspNetUsers_PhotographerId",
                        column: x => x.PhotographerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookedTimeSlots_PhotographerId",
                table: "BookedTimeSlots",
                column: "PhotographerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_BookedTimeSlots_EventTimeSlotBookedTimeSlotId",
                table: "Event",
                column: "EventTimeSlotBookedTimeSlotId",
                principalTable: "BookedTimeSlots",
                principalColumn: "BookedTimeSlotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_BookedTimeSlots_EventTimeSlotBookedTimeSlotId",
                table: "Event");

            migrationBuilder.DropTable(
                name: "BookedTimeSlots");

            migrationBuilder.RenameColumn(
                name: "EventTimeSlotBookedTimeSlotId",
                table: "Event",
                newName: "OutdoorEventTimeSlotsOutdoorBookedTimeSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_EventTimeSlotBookedTimeSlotId",
                table: "Event",
                newName: "IX_Event_OutdoorEventTimeSlotsOutdoorBookedTimeSlotId");

            migrationBuilder.AddColumn<int>(
                name: "IndoorEventTimeSlotsIndoorBookedTimeSlotId",
                table: "Event",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IndoorBookedTimeSlots",
                columns: table => new
                {
                    IndoorBookedTimeSlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndoorPhotographerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndoorEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IndoorMilestoneShoot = table.Column<bool>(type: "bit", nullable: false),
                    IndoorStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndoorBookedTimeSlots", x => x.IndoorBookedTimeSlotId);
                    table.ForeignKey(
                        name: "FK_IndoorBookedTimeSlots_AspNetUsers_IndoorPhotographerId",
                        column: x => x.IndoorPhotographerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OutdoorBookedTimeSlots",
                columns: table => new
                {
                    OutdoorBookedTimeSlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OutdoorPhotographerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OutdoorBoudoirShoot = table.Column<bool>(type: "bit", nullable: false),
                    OutdoorEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OutdoorStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutdoorBookedTimeSlots", x => x.OutdoorBookedTimeSlotId);
                    table.ForeignKey(
                        name: "FK_OutdoorBookedTimeSlots_AspNetUsers_OutdoorPhotographerId",
                        column: x => x.OutdoorPhotographerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_IndoorEventTimeSlotsIndoorBookedTimeSlotId",
                table: "Event",
                column: "IndoorEventTimeSlotsIndoorBookedTimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_IndoorBookedTimeSlots_IndoorPhotographerId",
                table: "IndoorBookedTimeSlots",
                column: "IndoorPhotographerId");

            migrationBuilder.CreateIndex(
                name: "IX_OutdoorBookedTimeSlots_OutdoorPhotographerId",
                table: "OutdoorBookedTimeSlots",
                column: "OutdoorPhotographerId");

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
    }
}
