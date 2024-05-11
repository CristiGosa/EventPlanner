using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlanner.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedLocationPlaceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlaceId",
                table: "Locations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Locations");
        }
    }
}
