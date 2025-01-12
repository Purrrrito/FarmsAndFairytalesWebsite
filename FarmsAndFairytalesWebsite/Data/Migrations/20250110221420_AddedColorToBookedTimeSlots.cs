using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmsAndFairytalesWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedColorToBookedTimeSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "BookedTimeSlots",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "BookedTimeSlots");
        }
    }
}
