using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmsAndFairytalesWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIdentityUserPhotographerToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotographerId",
                table: "Event",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Event_PhotographerId",
                table: "Event",
                column: "PhotographerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_AspNetUsers_PhotographerId",
                table: "Event",
                column: "PhotographerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_AspNetUsers_PhotographerId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_PhotographerId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "PhotographerId",
                table: "Event");
        }
    }
}
