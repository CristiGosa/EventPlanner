using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlanner.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedLocationCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "MapLatitude",
                table: "Locations",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MapLongitude",
                table: "Locations",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapLatitude",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "MapLongitude",
                table: "Locations");
        }
    }
}
