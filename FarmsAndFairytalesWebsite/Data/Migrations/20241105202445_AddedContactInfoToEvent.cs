using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmsAndFairytalesWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedContactInfoToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactInfo",
                table: "Event",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactInfo",
                table: "Event");
        }
    }
}
