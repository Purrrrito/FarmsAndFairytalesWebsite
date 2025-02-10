using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmsAndFairytalesWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedOutdoorBookedTimeSlotsName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OutdoorBookedTimeSlots",
                columns: table => new
                {
                    OutdoorBookedTimeSlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OutdoorStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OutdoorEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OutdoorPhotographerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OutdoorBoudoirShoot = table.Column<bool>(type: "bit", nullable: false)
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
                name: "IX_OutdoorBookedTimeSlots_OutdoorPhotographerId",
                table: "OutdoorBookedTimeSlots",
                column: "OutdoorPhotographerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutdoorBookedTimeSlots");
        }
    }
}
